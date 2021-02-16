### Version 3.4.0 *(February 16, 2021)*

##### Features
* Added the ability to update the App version within AMP.
* Added support for landscape format layouts.
* Added automatic retrying of API calls if timeout occurs.
* Added Amp.getLoginRequirement() to return the current setting value.
* Added Amp.getAllowLicenseKeys() to return the current setting value.
* Added AZURE environment configuration.

##### Bugs Fixed/Improvements
* Converted AMP to use Unified Logging and implemented relevant logging levels

##### Upgrading from AMP 3.3 to AMP 3.4
1. Add "Associated Domains" to your app’s entitlements to allow using saved passwords from the GEOINT App Store site:
        a. Go into your app’s entitlements file.
        b. Click the plus button to add a new row to "Entitlements File".
        c. Select "Associated Domains" for the "Key" value
        d. Change the "Type" to "Array" if not already
        e. Click the disclosure triangle to expand out the array for the Associated Domains
        f. Click plus to add a row to the array
        g. Enter "webcredentials:apps.nga.mil" for the "Value" in the array
    
2. Add "LSApplicationQueriesSchemes" to your app’s plist to allow opening the GEOINT App Store app directly from your app:
        a. Go into your app’s plist file
        b. Click the plus button to add a new row to "Information Property List"
        c. Change the "Type" to "Array" if not already
        d. Click the disclosure triangle to expand out the array for the LSApplicationQueriesSchemes
        e. Click plus to add a row to the array
        f. Enter "mil.dod.nga.GEOINT.Applications.Store" for the "Value" in the array


----------------------------------------------------------------
### Version 3.3.0 *(July 2, 2020)*

##### Build
* Build 100 is the version released for Xcode 11/Swift 5.0 

##### Bugs Fixed/Improvements
* Removed checkProvisioningProfile method because it was unnecessary
* Wait for response to checkProvisioningProfile message before proceeding with AMP calls
* Force portrait viewing of AMP screens
* Adjusted openApp call to wait for location manager before calling API 
* Fixed issues with TESTING configuration
* Removed the ability to permanently toggle password visibility.  Now visiblity requires a touch and hold.


----------------------------------------------------------------
### Version 3.2.0 *(June 1, 2020)*

##### Build
* Build 83 is the version released for Xcode 11/Swift 5.0 (Preferred Build)
* Build 90 is the version for Xcode 10.1/Swift 4.2 (June 3 Build)

##### Features
* Converted codebase to Swift.
* New designs for authentication screens.
* Added support for forced logout via API.
* Added support for controlling authorization types displayed via API.
* Added ability to get usage counter value.
* Added ability to use app offline even if subscription is expired.

##### Bugs Fixed/Improvements
* Enhanced dark mode support.
* Fixed bugs with testing state.
* Removed "Never" option as AMP login requirement.
* Added button to view GEOINT App Store if mission message is configured on openApp.
* Added permanent dismissal of individual mission messages.
* Fixed license key pasting and redemption when token times out.

##### Upgrading from AMP 3.x to AMP 3.2
The NONE option was removed from the AmpLoginRequirement.  Also, the default login requirement was changed to ALWAYS if not explicitly set.  After updating the project reference to the new AMP.XCFramework or AMP.Framework file, you will have to make the following code changes:

###### _Swift_
Change `AMP.` to `Amp.` on the following methods:
 `setAllowLicenseKeys, setUsagesBeforeRatingsPrompt, setLoginRequirement, start, isNetworkAvailable, getVersionNumber, displayRatingsPrompt, getLoginId, getDeviceId, oauthStart`

Remember to include OAuth return handler in *AppDelegate.swift* if you have not already done so:
                
        func application(_ app: UIApplication, open url: URL, options: [UIApplication.OpenURLOptionsKey : Any] = [:]) -> Bool {
            return Amp.oauthStart(url)
        }


###### _Objective-C_
Change `#import <AMP/AMP.h>` to `#import "AMP/AMP-Swift.h"`

Change `[AMP ...` to `[Amp ...` on the following methods:
`setAllowLicenseKeys, setUsagesBeforeRatingsPrompt, setLoginRequirement, startWith, isNetworkAvailable, getVersionNumber, displayRatingsPrompt, getLoginId, getDeviceId, oauthStart`

Change `[AMP startWithWindow:self.window ...` to `[Amp startWith:self.window ...`

Change `ONCE` to `AmpLoginRequirementONCE`

Change `ALWAYS` to `AmpLoginRequirementALWAYS`

Change `TESTING` to `AmpEnvironmentTESTING`

Change `NGA_IGAPP` to `AmpEnvironmentNGA_IGAPP`

Change `NGA_STAGE` to `AmpEnvironmentNGA_STAGE`

Change `NGA_PRODUCTION` to `AmpEnvironmentNGA_PRODUCTION`

Remember to include OAuth return handler in *AppDelegate.m* if you have not already done so:

        - (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options {
            return [Amp oauthStart:url];
        }



----------------------------------------------------------------
### Version 3.1.0 *(November 19, 2019)*

##### Bugs Fixed/Improvements
This version of AMP uses the code from AMP 3, but downgrades the Swift version to v4 and uses the old lipo script to create a framework file to support Xamarin and Xcode v10 building


----------------------------------------------------------------
### Version 3.0.15 *(November 14, 2019)*

##### Features
This version of AMP is built as an Xcframework and supports building and publishing apps in Xcode v11 and iOS 13. This version of AMP uses the V3 APIs and adds the following features:
* OAuth Support 
* License key authentication 
* Touch ID 
* Adding reviews on demand 
* Dark mode support


----------------------------------------------------------------
### Version 2.2 *(March 6, 2019)*

##### Bugs Fixed/Improvements
* Added support for Xamarin apps


----------------------------------------------------------------
### Version 2.1  *(November 26, 2018)*


----------------------------------------------------------------
### Version 2.0  *(August 27, 2018)*


----------------------------------------------------------------
### Version 1.4.1  *(August 23, 2018)*


----------------------------------------------------------------
### Version 1.0.2  *(May 3, 2018)*

