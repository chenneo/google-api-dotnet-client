List of released versions:


# Version 1.9.1 #
Blogpost: http://google-api-dotnet-client.blogspot.com/2014/12/announcing-release-of-191.html
```
Google .NET Client Library
Stable Release '1.9.1'
Monday, December 29, 2014
===========================================

Changes:
  1030: Tools\Google.Apis.Release - Do not clean the generated directory, it should contains .NET doc
  1029: NuGet package should refer to the release notes as the project url
  1028: Improve ComputeCredential error while trying to request a new access token
    https://codereview.appspot.com/186470043/
  1027: Issue 503: TokenResponse.IsExpired returns true at one minute after token expiration
    https://codereview.appspot.com/184430043/
  1026: Supports Compute Credentials
    https://codereview.appspot.com/181560043
  1025: Chnage version to 1.9.1 and output xml for new WP8.1 projects
  1024: Issue 471: Support WP 8.1 projects
    https://codereview.appspot.com/176220043/
  1023: Issue 330 - Operation Could Destabilize Runtime Google.Api.Services.BaseClientService
    https://codereview.appspot.com/110190043/
  1022: Issue 482: GoogleWebAuthorizationBroker.AuthorizeAsync - browser will not self-close
    https://codereview.appspot.com/162630043/
```

# Version 1.9.0 #
Blogpost: http://google-api-dotnet-client.blogspot.com/2014/09/announcing-release-of-190.html
```
Google .NET Client Library
Stable Release '1.9.0'
Tuesday, September 30, 2014
===========================================

Changes:
  1020: Issue 471: Support Windows 8.1 application
    (NOTE: There is not a full solution for WP8.1 and there is a problem
    in building the project using Google.Apis.Release tool)
    https://codereview.appspot.com/135720043/
  1019: Issue 475: Pressing the back-button on WP crashes the application
    https://codereview.appspot.com/117500043/
  1018: Issue 471: Support Windows Phone 8.1 - Upgrade JSON.NET to 6.0.4
    and update Portable projects to profile 328
  1017: Use profile 136 for now (everything compiles again)
    Need to investigate how can we support 
    portable-net40+sl50+win+wpa81+wp80 (profile 328) for Newtonsoft.Json and Zlib.Portable.
    Both those packages are missing some configuration for profile 328.
  1016: Update projects to support profile 328 (for universal apps)
  1015: Issue 478: Update NuGet dependencies
    https://codereview.appspot.com/111210044/
```

# Version 1.8.2 #
```
Google .NET Client Library
Stable Release '1.8.2'
Monday, May 26, 2014
===========================================

Changes:
  1011: Issue 452: Fixing a incompatible change that was committed before (FileDataStore)
  https://codereview.appspot.com/100490046/
  1010: Issue 464: Can not send HTTP request when setting an invalid etag
  https://codereview.appspot.com/96320045/
  1009: Issue 463: Add support in token revocation
  https://codereview.appspot.com/94340043/
  1008: Issue 462: Improve FileDataStore impl
  https://codereview.appspot.com/97380043/
  1007: Imrpove NuGet Publisher logs
  1006: Issue 455: Translate api does not work in POST mode (bug in MaxUrlLengthInterceptor)
  https://codereview.appspot.com/87170043/

```

# Version 1.8.1 #
```
Google .NET Client Library
Stable Release '1.8.1'
Monday, March 17, 2014
===========================================

!NO CODE CHANGES FROM RC!
Developers guide was improved significantely:
https://developers.google.com/api-client-library/dotnet/

```

# Version 1.8.0 (RC) #
```
Google .NET Client Library
Stable Release '1.8.0-rc'
Monday, February 17, 2014
===========================================

Changes:
  996-997: WP Auth bug fix
  995: Improve comments for doxygen proess (Step 2)
  994: Improve comments for doxygen (step 1)
  992-993: Improve comment on AuthActionFilter
  991: Fix comments
  990: Issue 362: Add a resume method to media upload
  https://codereview.appspot.com/61660043
  989: Release tool should support rc
  988: Fix comment (and\or ==> and \ or)
  987: Issue 422: Back btton doesnt work on WP auth login
  Issue 431: WebAuthenticationBrokerUserControl.OnBrowserNavigationFailed throws ArgumentNullException when no network connectivity
  https://codereview.appspot.com/46440043/
  986: Issue 436: Add Utility methods to parse DateTime to string and stirng to DateTime
  https://codereview.appspot.com/46920043/
  985: Issue 432: Batch request with null callback throws exception
  https://codereview.appspot.com/46460043/

```

# Version 1.7 (beta) #
```
Google .NET Client Library
Stable Release '1.7.0-beta'
Wednesday, December 18, 2013
===========================================

Changes:
  981: DateTime bug when server gets "2013-12-17T23:26:42Z" and not "updated=2013-12-17T23:26:42.000Z"
  message=Invalid value for: Invalid format: "2013-12-17T23:26:42Z" is malformed at "Z"
  980: Issue 428: Fix a bug in using a DateTime as a query parameter
  979: Issue 401: Malformed HTTP request based on not honoring the Uri Template Spec (http://tools.ietf.org/html/rfc6570)
  978: Issue 425: Set HttpStatusCode on GoogleApiException when available
  977: Issue 420: RequestAccessTokenAsync and RefreshAccessTokenAsync should be public
  976: Issue 60: Support Batch Request
  975: Call ConfigureAwait on every call to await (when applicable)
  973: Issue 407: Split Google.Apis to Google.Apis.Core and Google.Apis
  969: Issue 404: Remove obsolete code (GoogleApis.Authentication)
```

# Version 1.6 (beta) #
```
Google .NET Client Library
Stable Release '1.6.0-beta'
Wednesday, October 23, 2013
===========================================

Changes:
  965: Change the nuspec descritpions
  964: Fix a bug on creating core nuget packages
  963 and 962: Remove all content of ThirdParty library except DotNetOpenAuth
  961: Upgrade Newtonsoft.Json to 5.0.8 and add IClientServiceRequest interface (not generic)
  958, 959 and 960: Rename MVC NuGet package from MVC4 to MVC
  957: Fix a small bug in the release process
  956: Issue 351: Reimplement OAuth2 library (Step 7): Change the release process to support new packages
    https://codereview.appspot.com/13251043/
  955: Issue 351: Reimplement OAuth2 (Step 5): WP support
    https://codereview.appspot.com/14295045/
  954: Issue 351: Reimplement OAuth2 (Step 6): WinRT Support
    https://codereview.appspot.com/14433060/
  953: Issue 361: MediaDownloader cant download drive export list
    https://codereview.appspot.com/13480044/
  952: Issue 351: Reimplement OAuth2 (Step 4): ServiceAccount and MVC
    https://codereview.appspot.com/14341043/
  951: Issue 351: Reimplement OAuth2 library (Step 3 - Tests, UserCredential and Flows)
    https://codereview.appspot.com/13972043/
  950: Issue 146: Pass override HTTP header when request URI too long
    https://code.google.com/p/google-api-dotnet-client/issues/detail?id=146
    https://codereview.appspot.com/13096044/
  949: Issue 383: ExecuteAsync creates unnecessary additional Task
    https://codereview.appspot.com/14229044/
  948: Issue 377: New build tool releasing a new version
    https://codereview.appspot.com/12767046
  947: Issue 351: Reimplement OAuth2 - Step 2 (Auth PCL - only data types)
    https://codereview.appspot.com/13632059/
  946: Issue 148: Extension method should be internal (and not public)
  945: Merge
  944: Add NuGet.exe to Tootls\.nuget folder
  943: Issue 351: Reimplement OAuth2 library (Step 1)
    https://codereview.appspot.com/13412046/
  942: Change the directory of Google.Apis.NuGet.Publisher project
  941: merge
  940: Rename IMediaDownloaded file to IMediaDownloader
  939: Issue 376: Generate NuGet publisher to publish Google.Apis packages
    https://codereview.appspot.com/12662047/

```

# Version 1.5 (beta) #
```
Google .NET Client Library
Stable Release '1.5.0-beta'
Monday, August 19, 2013
===========================================

Changes:
  913: Issue 369: Change deafult behavior of a HTTP request (Number Tries = 3, BackOff of 503 by default)
  https://codereview.appspot.com/12566043/
  912: Add documenation to DotNet4 project
  911: Change in .hgignore add Google.Apis.*xml
  910: Issues 373 (Execute bug), 374 (Remove Tests.Utility assembly) and 375 (Clean warnings)
  https://codereview.appspot.com/12772043/
  909: Update .hgignore file
  908: Issue 360: Fix a bug in ResumableUpload when media size is unknown
  https://codereview.appspot.com/11347044/
  907: Issue 325: Remove discovery and codegen parts from the library
  https://codereview.appspot.com/12020043/
  906: Issue 368: Update NuGet.exe
  904: Update Microsoft.Http.Client.LICENSE.rtf
```

# Version 1.4 (beta) #
```
Google .NET Client Library
Stable Release '1.4.0-beta'
Monday, June 24, 2013
===========================================

Changes:
  901: Issue 322: Use canonical name for service class when possible
  900: Issue 338: Support media downloader
  899: Issue 334: Include Google.Apis.FullProfile as part of our release
  897: Issue 320: change Google.Apis to be a PCL assembly
  896: Issue 321: Support CancellationToken on service and media requests
  895: Issue 329 - Exponential back-off
  894: Remove ResourcePath from a service request
  893: NuGet packages error on Windows8
  892: Update Tools and add add dlls and licenses in ThirdParty library
  891: Add NuGet.exe to .nuget folder
  890: Issues 320,324 and 260 - Upgrade .NET 4.0, Create Google.Api.Http namesapce and support NuGet
  889: Fix ResumableUpload Uri
  888: Issues 310 and 311: ResumableUpload - support server errors and stream with unknown size
  887: Issue 308: set ResumableUpload url to /upload + resource-uri
  886: Issue 304: ServiceGenerator should use rootUrl key from the discovery doc
```

# Version 1.3 (beta) #
```
Google .NET Client Library
Stable Release '1.3.0-beta'
Monday, March 18, 2013
===========================================
  882: Add Google.Apis.Services namesapce
  881: Issue 303: ServiceGenerator does not work with readonly url files
  880: Issue 300: Bug in ResumableUpload url
  879: Merge
  878: Issue 293: Include clr version (Unit Test)
  877: Issues 293 and 295: Include clr version and api version in UserAgent header
  876: Issue 292: Improve the build process
  875: Issue 277: DiscoveryDocument should be removed from each generated API
  874: Issues: 193 (specify alt parameter on ServiceRequest) and 249 (omit default values from query)
```

# Version 1.2 (beta) #
```
Google .NET Client Library
Stable Release '20111109-1.2.4507-beta'
Wednesday, May 4, 2012
===========================================
 - Resumable media upload supported in base and generated API layers.
 - Refactored parameter parsing and URL construction (see HttpWebRequestBuilder).
 - Refactored authenticator interface.
 - Alpha support for VisualBasic codegen.
 - Fix constraint violation problem with missing refresh-tokens.
 - Code generation fixed for some services that were failing to generate.
 - NUnit dll reference no longer requried for apis dll.
```

# Version 1.1 (beta) #
<a href='Hidden comment: GENERATED_STABLE_BEGIN'></a>
```
Google .NET Client Library
Stable Release '20111109-1.1.4329-beta'
Wednesday, November 09, 2011
===========================================

Changes:
  728: Added tag 20111109-1.1.4329-beta for changeset adfc9ae26f49
  727: Look up parameter information in Service as well as Method when building request.
  726: Filter out hand coded common parameters.
  725: Update build release program to include new Google.Tests.Utility DLL.
  724: Updated assembly versions to prepare for 1.1 release.
  723: Fix syntax error caused by special characters in namespace.
  722: Merged issue_145_remove_nunit_ref
  719: Merged with .orig change.
  717: Merge with Issue 155
  716: Merge issue 164, support common parameters.
  709: merge in branch issue_169_licences as per codereview 5323073
  703: Merge.
  699: Merge fix for wiki pages with empty titles.
  698: Merge Issue #159.
  697: Merge.
  695: Merge issue #153.
  692: Merge with issue_159_jsonreader_should_be_static


```
<a href='Hidden comment: GENERATED_STABLE_END'></a>

# Version 1.0.0 (beta) #
```
Google .NET Client Library
Stable Release '20110831-1.0.0-beta'
Wednesday, August 31, 2011
===========================================

This is the beta release of the Google .NET Client Library. 

Major features which are included in this build:
 - Support for all Discovery Google APIs
 - Generation of strongly typed client libraries
 - OAuth2 using DotNetOpenAuth
```


# Version 0.9.7 (pre-beta) #
```
Google .NET Client Library
Stable Release '20110830-0.9.7'
Tuesday, August 30, 2011
===========================================
This is a pre-beta release. The library is still in alpha, but we should release the Beta soon.
You should be able to use this release with all APIs in the Google Discovery.

Changes:
  668: Refactored the change for issue #130, and removed the .hg folder from the generated .zip files.
  667: Added a workaround for the framework bug causing #130.
  666: Added support for the "Downloads.wiki"-page in the BuildRelease tool.
  665: Fixed style issues with the new ServiceGenerator project.
  664: Merged changes from branch issue_113_silverlight.
  660: Merged changes from branch issue_129_core_servicegenerator.
```