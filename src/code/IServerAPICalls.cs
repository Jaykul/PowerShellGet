// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.PowerShell.PowerShellGet.UtilClasses;
using NuGet.Versioning;
using System.Net.Http;
using System.Runtime.ExceptionServices;

public interface IServerAPICalls
{
    #region Methods
    /// <summary>
    /// Find method which allows for searching for all packages from a repository and returns latest version for each.
    /// Examples: Search -Repository PSGallery
    /// API call: 
    /// - Include prerelease: http://www.powershellgallery.com/api/v2/Search()?$filter=IsAbsoluteLatestVersion&includePrerelease=true
    /// </summary>
    string[] FindAll(bool includePrerelease, ResourceType type, out ExceptionDispatchInfo edi);

    /// <summary>
    /// Find method which allows for searching for packages with tag from a repository and returns latest version for each.
    /// Examples: Search -Tag "JSON" -Repository PSGallery
    /// API call: 
    /// - Include prerelease: http://www.powershellgallery.com/api/v2/Search()?$filter=IsAbsoluteLatestVersion&searchTerm='tag:JSON'&includePrerelease=true
    /// </summary>
    string[] FindTag(string tag, bool includePrerelease, ResourceType _type, out ExceptionDispatchInfo edi);
  
    /// <summary>
    /// Find method which allows for searching for single name and returns latest version.
    /// Name: no wildcard support
    /// Examples: Search "PowerShellGet"
    /// API call: 
    /// - No prerelease: http://www.powershellgallery.com/api/v2/FindPackagesById()?id='PowerShellGet'
    /// - Include prerelease: http://www.powershellgallery.com/api/v2/FindPackagesById()?id='PowerShellGet'
    /// Implementation Note: Need to filter further for latest version (prerelease or non-prerelease dependening on user preference)
    /// </summary>
    string FindName(string packageName, bool includePrerelease, ResourceType type, out ExceptionDispatchInfo edi);

    string FindNameWithTag(string packageName, string[] tags, bool includePrerelease, ResourceType type, out ExceptionDispatchInfo edi);

    /// <summary>
    /// Find method which allows for searching for single name with version range.
    /// Name: no wildcard support
    /// Version: supports wildcards
    /// Examples: Search "PowerShellGet" "[3.0.0.0, 5.0.0.0]"
    ///           Search "PowerShellGet" "3.*"
    /// API Call: http://www.powershellgallery.com/api/v2/FindPackagesById()?id='PowerShellGet'
    /// Implementation note: Returns all versions, including prerelease ones. Later (in the API client side) we'll do filtering on the versions to satisfy what user provided.
    /// </summary>
    string[] FindNameGlobbing(string packageName, bool includePrerelease, ResourceType type, out ExceptionDispatchInfo edi);

    string[] FindNameGlobbingWithTag(string packageName, string[] tags, bool includePrerelease, ResourceType type, out ExceptionDispatchInfo edi);

    /// <summary>
    /// Find method which allows for searching for single name with specific version.
    /// Name: no wildcard support
    /// Version: no wildcard support
    /// Examples: Search "PowerShellGet" "2.2.5"
    /// API call: http://www.powershellgallery.com/api/v2/Packages(Id='PowerShellGet', Version='2.2.5')
    /// </summary>
    string[] FindVersionGlobbing(string packageName, VersionRange versionRange, bool includePrerelease, ResourceType type, bool getOnlyLatest, out ExceptionDispatchInfo edi);

    // <summary>
    /// Find method which allows for searching for single name with specific version.
    /// Name: no wildcard support
    /// Version: no wildcard support
    /// Examples: Search "PowerShellGet" "2.2.5"
    /// API call: http://www.powershellgallery.com/api/v2/Packages(Id='PowerShellGet', Version='2.2.5')
    /// </summary>
    string FindVersion(string packageName, string version, ResourceType type, out ExceptionDispatchInfo edi);

    string FindVersionWithTag(string packageName, string version, string[] tags, ResourceType type, out ExceptionDispatchInfo edi);
    

    /// <summary>
    /// Installs specific package.
    /// Name: no wildcard support.
    /// Examples: Install "PowerShellGet"
    /// Implementation Note: if prerelease: call IFindPSResource.FindName()
    ///                      if not prerelease: https://www.powershellgallery.com/api/v2/package/Id (Returns latest stable)
    /// </summary>
    HttpContent InstallName(string packageName, bool includePrerelease, out ExceptionDispatchInfo edi);

    /// <summary>
    /// Installs package with specific name and version.
    /// Name: no wildcard support.
    /// Version: no wildcard support.
    /// Examples: Install "PowerShellGet" -Version "3.0.0.0"
    ///           Install "PowerShellGet" -Version "3.0.0-beta16"
    /// API Call: https://www.powershellgallery.com/api/v2/package/Id/version (version can be prerelease)
    /// </summary>    
    HttpContent InstallVersion(string packageName, string version, out ExceptionDispatchInfo edi);

    #endregion
}
