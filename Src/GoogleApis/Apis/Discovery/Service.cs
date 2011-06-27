/*
Copyright 2010 Google Inc

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Json;
using Google.Apis.JSON;
using Google.Apis.Requests;
using Google.Apis.Testing;
using Google.Apis.Util;
using Google.Apis.Discovery.Schema;
using log4net;
using Newtonsoft.Json;

namespace Google.Apis.Discovery
{

    #region BaseService

    /// <summary>
    /// Represents the basic implementation of a service
    /// </summary>
    public abstract class BaseService : IService
    {
        /// <summary>
        /// Name of the method used for serialization (object -> json)
        /// </summary>
        public const string SerializationMethodName = "SerializeRequest";

        /// <summary>
        /// Name of the method used for deserialization (json -> object)
        /// </summary>
        public const string DeserializationMethodName = "DeserializeResponse";

        /// <summary>
        /// Name of the property defining the ISerializer
        /// </summary>
        public const string SerializerPropertyName = "Serializer";

        private static readonly ILog logger = LogManager.GetLogger(typeof(BaseService));

        protected internal readonly JsonDictionary information;
        private Dictionary<string, IResource> resources;
        private IDictionary<String, ISchema> schemas;
        private ISerializer serializer;

        internal BaseService(string version, string name, JsonDictionary values, BaseFactoryParameters param) : this()
        {
            version.ThrowIfNull("version");
            name.ThrowIfNull("name");
            values.ThrowIfNull("values");
            param.ThrowIfNull("param");

            // Set required properties
            Version = version;
            Name = name;
            information = values;

            // Set optional properties
            Id = values.GetValueAsNull("id") as string;
            Labels = values.GetValueAsStringListOrEmpty("labels").ToList().AsReadOnly();
            Features = values.GetValueAsStringListOrEmpty("features").ToList().AsReadOnly();
            DocumentationLink = values.GetValueAsNull("documentationLink") as string;
            Protocol = values.GetValueAsNull("protocol") as string;
            Description = values.GetValueAsNull("description") as string;
            Title = values.GetValueAsNull("title") as string;

            // Determine the Server URL and (optional) Base Path
            param.ServerUrl.ThrowIfNull("param.ServerUrl");
            ServerUrl = param.ServerUrl;
            BasePath = param.BasePath;
        }

        private BaseService()
        {
            GZipEnabled = true;
            Serializer = new NewtonsoftJsonSerializer();
        }

        protected string ServerUrl { get; set; }
        protected string BasePath { get; set; }

        #region IService Members

        public string Name { get; private set; }
        public string Version { get; private set; }
        public string Description { get; private set; }
        public string Title { get; private set; }

        public string Id { get; private set; }
        public IList<string> Labels { get; private set; }
        public IList<string> Features { get; private set; }
        public string DocumentationLink { get; private set; }
        public string Protocol { get; private set; }

        public abstract DiscoveryVersion DiscoveryVersion { get; }

        public Uri BaseUri
        {
            get
            {
                if (ServerUrl.EndsWith("/") && BasePath.StartsWith("/"))
                {
                    return new Uri(ServerUrl.Substring(0, ServerUrl.Length - 1) + BasePath);
                }
                else if (ServerUrl.EndsWith("/") == false && BasePath.StartsWith("/") == false)
                {
                    return new Uri(ServerUrl + "/" + BasePath);
                }
                else
                {
                    return new Uri(ServerUrl + BasePath);
                }
            }
        }

        public Uri RpcUri
        {
            get { return new Uri(information[ServiceFactory.RpcUrl] as string); }
        }

        public IDictionary<string, IResource> Resources
        {
            get
            {
                if (resources == null)
                {
                    JsonDictionary js = information.GetValueAsNull(ServiceFactory.Resources) as JsonDictionary;
                    if (js != null)
                    {
                        resources = new Dictionary<string, IResource>();
                        foreach (KeyValuePair<string, object> kvp in js)
                        {
                            IResource r = CreateResource(kvp);
                            resources.Add(kvp.Key, r);
                        }
                    }
                    else
                    {
                        resources = new Dictionary<string, IResource>();
                    }
                }
                return resources;
            }
        }

        public virtual IDictionary<string, ISchema> Schemas
        {
            get
            {
                if (schemas != null)
                {
                    return schemas;
                }

                logger.DebugFormat("Fetching Schemas for service {0}", Name);
                var js = information[ServiceFactory.Schemas] as JsonDictionary;
                if (js != null)
                {
                    schemas = ParseSchemas(js);
                }
                else
                {
                    schemas = new Dictionary<string, ISchema>(0).AsReadOnly();
                }

                return schemas;
            }
        }

        public ISerializer Serializer
        {
            get { return serializer; }
            set
            {
                value.ThrowIfNull("value");
                serializer = value;
            }
        }

        /// <summary>
        /// Creates a Request Object based on the HTTP Method Type.
        /// </summary>
        public IRequest CreateRequest(string resource, string methodName)
        {
            var method = GetResource(this, resource).Methods[methodName];
            var request = Request.CreateRequest(this, method);

            return request;
        }

        /// <summary>
        /// Retrieves a resource using the full resource name.
        /// Example:
        ///     TopResource.SubResource will retrieve the SubResource which can be found under the TopResource.
        /// </summary>
        [VisibleForTestOnly]
        internal static IResource GetResource(IResourceContainer container, string fullResourceName)
        {
            fullResourceName.ThrowIfNull("fullResourceName");

            string[] split = fullResourceName.Split(new[] { '.' }, 2);
            string topResourceName = split[0];
            IResource topResource = container.Resources[topResourceName];

            if (split.Length <= 1)
            {
                // This is the resource we are looking for.
                return topResource;
            }
            
            // Retrieve the top resource, and re-run this method on it.
            string fullSubresourceName = split[1];
            return GetResource(topResource, fullSubresourceName);
        }

        public string SerializeRequest(object obj)
        {
            if (HasFeature(Discovery.Features.LegacyDataResponse))
            {
                // Legacy path
                var request = new StandardResponse<object> { Data = obj };
                return Serializer.Serialize(request);
            }

            // New v1.0 path
            return Serializer.Serialize(obj);
        }

        public T DeserializeResponse<T>(Stream input)
        {
            // Read in the entire content
            string text;

            using (var reader = new StreamReader(input))
            {
                text = reader.ReadToEnd();
            }

            // Check if there was an error returned. The error node is returned in both paths
            // Deserialize the stream based upon the format of the stream
            if (HasFeature(Discovery.Features.LegacyDataResponse))
            {
                // Legacy path (deprecated!)
                StandardResponse<T> response = null;
                try
                {
                    response = Serializer.Deserialize<StandardResponse<T>>(text);
                }
                catch(JsonReaderException ex)
                {
                    throw new GoogleApiException(this, "Failed to parse response from server as json ["+text+"]", ex);
                }

                if (response.Error != null)
                {
                    throw new GoogleApiException(this, "Server error - " + response.Error);
                }
            
                if (response.Data == null)
                {
                    throw new GoogleApiException(this, "The response could not be deserialized.");
                }

                return response.Data;
            }

            // New path: Deserialize the object directly
            T result = default(T);
            try
            {
                result = Serializer.Deserialize<T>(text);
            }
            catch (JsonReaderException ex)
            {
                throw new GoogleApiException(this, "Failed to parse response from server as json [" + text + "]", ex);
            }

            // If this schema/object provides an error container, check it
            if (result is IResponse)
            {
                var response = (IResponse) result;
                if (response.Error != null)
                {
                    throw new GoogleApiException(this, "Server error - " + response.Error);
                }
            }

            return result;
        }

        public bool HasFeature(Features feature)
        {
            return Features.Contains(feature.GetStringValue());
        }

        public bool GZipEnabled { get; set; }

        #endregion

        internal static IDictionary<string, ISchema> ParseSchemas(JsonDictionary js)
        {
            js.ThrowIfNull("values");

            var working = new Dictionary<string, ISchema>();

            var resolver = new FutureJsonSchemaResolver();
            foreach (KeyValuePair<string, object> kvp in js)
            {
                logger.DebugFormat("Found schema {0}", kvp.Key);
                var serilizer = new JsonSerializer();
                var textWriter = new StringWriter();
                serilizer.Serialize(textWriter, kvp.Value);
                string result = textWriter.ToString();
                ISchema schema = new SchemaImpl(kvp.Key, result, resolver);
                working.Add(schema.Name, schema);
            }

            resolver.ResolveAndVerify();

            return working.AsReadOnly();
        }

        /// <summary>
        /// Creates a version specific resource containing 
        /// out of the specified KeyValuePair
        /// </summary>
        /// <param name="kvp"></param>
        /// <returns></returns>
        public virtual IResource CreateResource(KeyValuePair<string, object> kvp)
        {
            return new ResourceV1_0(kvp);
        }
    }

    #endregion

    #region Service V1.0

    /// <summary>
    /// Represents a Service as defined in Discovery V1.0
    /// </summary>
    internal class ServiceV1_0 : BaseService
    {
        private const string BasePathField = "basePath";

        /// <summary>
        /// Creates a v1.0 service
        /// </summary>
        public ServiceV1_0(string version, string name, FactoryParameterV1_0 param, JsonDictionary values)
            : base(version, name, values, param)
        {
            // If no BasePath has been set, then retrieve it from the json document
            if (BasePath.IsNullOrEmpty())
            {
                BasePath = information.GetMandatoryValue<string>(BasePathField);
            }
        }

        public override DiscoveryVersion DiscoveryVersion
        {
            get { return DiscoveryVersion.Version_1_0; }
        }
    }

    #endregion

    #region Service V0.3

    /// <summary>
    /// Represents a Service as defined in Discovery V0.3
    /// </summary>
    internal class ServiceV0_3 : BaseService
    {
        private const string RestBasePathField = "restBasePath";

        /// <summary>
        /// Creates a v0.3 service
        /// </summary>
        public ServiceV0_3(string version, string name, FactoryParameterV0_3 param, JsonDictionary values)
            : base(version, name, values, param)
        {
            // If no BasePath has been set, then retrieve it from the json document
            if (BasePath.IsNullOrEmpty())
            {
                BasePath = information.GetMandatoryValue<string>(RestBasePathField);
            }
        }

        public override DiscoveryVersion DiscoveryVersion
        {
            get { return DiscoveryVersion.Version_0_3; }
        }

        public override IResource CreateResource(KeyValuePair<string, object> kvp)
        {
            return new ResourceV0_3(kvp);
        }
    }

    #endregion
}