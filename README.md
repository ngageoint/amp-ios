# GEOINT App Store AIMS Mobile Platform (AMP) for iOS

## Overview

The GEOINT App Store AIMS Mobile Platform (AMP) for iOS is an XCFramework package used to enable management of installs, 
subscription licenses, and other metrics about the [IGAPP](https://igapp.com/) program products available on the 
[GEOINT App Store (GAS)](https://apps.nga.mil) website. Adding AMP is a requirement of [IGAPP](https://igapp.com/) applications. 
This document will explain the process of adding AMP to an iOS application. 

Here is a summary of AMP’s capabilities:

- Subscription Service - Specify a time period for when your app can be used. Users can renew subscriptions in-app.
- Usage Management - Allows admins of GEOINT App Store to remotely lock down an application for a specific user, app, version, etc.
- Metrics - Enables GEOINT App Store admins to have metrics on app usage, including time and location data.
- Device ID - Provides access to a device ID that will match the device ID that the native GEOINT App Store app uses.
- Login ID - Provides access to a login ID that is assigned to a user, regardless of device.
- In-App Reviews - Allows users to write a review for your product from within the app.  This review will be posted in the GEOINT App 
Store.
- Provisioning Profile Check - Monitors for provisioning profile expiration and alerts the user when the certificate used to sign your 
app is about to expire

## Adding AMP to your project

The easiest way to add AMP is through Swift Package Manager if using XCode 12 or later:  
1.  Open your project, navigate to 'File' > 'Swift Packages' > 'Add Package Dependency...'.  
2.  Select your project and click 'Next' (if required).  
3.  Enter this repository URL and tap 'Next'
4.  Choose the version you want to use (ideally the latest version) and click 'Next'
5.  Choose the target and click 'Finish'
6.  Continue to the '[Implementing AMP in iOS](#implementing-amp-in-ios)' section of this document

If not using XCode 12 or SPM:
1.  Download the XCframework file from this repo 
2.  Copy the AMP framework into your project by dragging the file into the "Embedded Binaries" section of your target. Be sure to select 
"Copy items if needed" and select your target when adding the file.
3.  Continue to the '[Implementing AMP in iOS](#implementing-amp-in-ios)' section of this document

## Upgrading from AMP 3.1 and below to AMP 3.2 or greater

The NONE option was removed from the AmpLoginRequirement.  Also, the default login requirement was changed to ALWAYS if not explicitly 
set.  After updating the project reference to the new AMP.XCFramework or AMP.Framework file, you will have to make the code changes 
listed below.  It is also a good idea to read through all the steps and verify you did not forget anything the first time, as the 
directions have become more verbose between versions.  This includes documenting OAuth integration and Biometric authentication.

###### _Swift Changes_
1.  Change `AMP.` to `Amp.` on the following methods: `setAllowLicenseKeys, setUsagesBeforeRatingsPrompt, setLoginRequirement, startWith, 
isNetworkAvailable, getVersionNumber, displayRatingsPrompt, getLoginId, getDeviceId, oauthStart`
2.  Remember to include OAuth return handler in *AppDelegate.swift* if you have not already done so. See the 
'[OAuth completion block](#oauth-completion-block)' section of this document.

###### _Objective-C Changes_
1.  Change `#import <AMP/AMP.h>` to `#import "AMP/AMP-Swift.h"`
2.  Change `[AMP ...` to `[Amp ...` on the following methods: `setAllowLicenseKeys, setUsagesBeforeRatingsPrompt, setLoginRequirement, 
start, isNetworkAvailable, getVersionNumber, displayRatingsPrompt, getLoginId, getDeviceId, oauthStart`
3.  Change `[AMP startWithWindow:self.window ...` to `[Amp startWith:self.window ...`
4.  Change `ONCE` to `AmpLoginRequirementONCE`
5.  Change `ALWAYS` to `AmpLoginRequirementALWAYS`
6.  Change `TESTING` to `AmpEnvironmentTESTING`
7.  Change `NGA_IGAPP` to `AmpEnvironmentNGA_IGAPP`
8.  Change `NGA_STAGE` to `AmpEnvironmentNGA_STAGE`
9.  Change `NGA_PRODUCTION` to `AmpEnvironmentNGA_PRODUCTION`
10. Remember to include OAuth return handler in *AppDelegate.m* if you have not already done so. See the 
'[OAuth completion block](#oauth-completion-block)' section of this document.

###### _Xamarin Changes_
1.  Replace the ApiDefinitions.cs and Structs.cs files with the new versions
2.  Change `AMP.` to `Amp.` on the following methods: `SetAllowLicenseKeys, SetUsagesBeforeRatingsPrompt, SetLoginRequirement, StartWith, 
IsNetworkAvailable, GetVersionNumber, DisplayRatingsPrompt, GetLoginId, GetDeviceId, OauthStart`
3.  Remember to include OAuth return handler in *AppDelegate.cs* if you have not already done so. See the 
'[OAuth completion block](#oauth-completion-block)' section of this document.
4.  Change "AMP.RatingsStatus" to "Amp.GetRatingsStatus"

## Implementing AMP in iOS

1.  In the Signing & Capabilities tab of your target, enable Keychain Sharing and add "AMPGroup" to your list of groups.
3.  In the Info tab of your target, add the URL Type of "$(PRODUCT_BUNDLE_IDENTIFIER)"
4.  In the Build Settings tab of your target, set the Enable Bitcode setting to "No".
5.  If using a _SceneDelegate_, skip to next step, otherwise, go to your _AppDelegate_ class and add the following to your 
applicationDidFinishLaunchingWithOptions override. You will also need import the AMP namespace: 
        
    a.	For Objective-C, add "#import <AMP/AMP-Swift.h>"".  
    
    b.	For Swift, add "import AMP".
    
    c.	For Xamarin "using AMPBinding".  Also, for Xamarin you will have to include the AMPBinding project that is included in the AMP zip.  
More details in the '[Implementing AMP in Xamarin for iOS](#implementing-amp-in-xamarin-for-ios)' section of this document

        // For Objective-C Projects:
        //////////////////////////////////////////////////////////////////
        
        #import "AMP/AMP-Swift.h"
        ...
        - (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
            self.window = [[UIWindow alloc] initWithFrame:UIScreen.mainScreen.bounds];
            NSString *signature = @"test";
            [Amp startWith:self.window environment:AmpEnvironmentTESTING signature:signature completion:^{
                // Your segue will go here. See step 7 for more details.
                ...
            }];
        }


        // For Swift Projects:
        //////////////////////////////////////////////////////////////////
        
        import AMP
        ...
        func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
            window = UIWindow(frame: UIScreen.main.bounds)
            let signature = "test"
            Amp.start(with: window, environment: .TESTING, signature: signature) {
                // Your segue will go here. See step 7 for more details.
                ...
            }
        }
        
        
        // For Xamarin Projects:
        //////////////////////////////////////////////////////////////////
        
        using AMPBinding;
        ...
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var signature = "test";
            Amp.StartWithWindow(Window, AmpEnvironment.Testing, signature, () => {
                // Your segue will go here. See step 7 for more details.
                ...
            });
            return true;
        }
        
6.  If using a _SceneDelegate_, go to your _SceneDelegate_ class and add the following to your scene function.
        
        // For Objective-C Projects:
        //////////////////////////////////////////////////////////////////////////
        
        #import "AMP/AMP-Swift.h"
        ...
        - (void)scene:(UIScene *)scene willConnectToSession:(UISceneSession *)session options:(UISceneConnectionOptions *)connectionOptions {
        	self.window = [[UIWindow alloc] initWithWindowScene:(UIWindowScene *)scene];
        	self.window.rootViewController = [[UIViewController alloc] init]; // This placeholder VC will be replaced by AMP VC
        	[self.window makeKeyAndVisible];
        
        	// Start AMP
        	NSString *signature = @"test";
        	[Amp startWith:self.window environment:AmpEnvironmentTESTING signature:signature completion:^{
            		// Your segue will go here. See step 7 for more details.
        		...
        	}];
        }


        // For Swift Projects:
        //////////////////////////////////////////////////////////////////
        
        import AMP
        ...
        func scene(_ scene: UIScene, willConnectTo session: UISceneSession, options connectionOptions: UIScene.ConnectionOptions) {
            guard let windowScene = (scene as? UIWindowScene) else { return }
            
            self.window = UIWindow(windowScene: windowScene)
            self.window?.rootViewController = UIViewController(nibName: nil, bundle: nil) // This placeholder VC will be replaced by AMP VC
            self.window?.makeKeyAndVisible()
            
            // Start AMP
            let signature = "test"
            Amp.start(with: self.window, environment: environment, signature: signature) {
                // Your segue will go here. See step 7 for more details.
                ...
            }
        }
        

7.  Fill in the start method’s completion block with a segue to your initial view controller. Below are several examples of how this can be accomplished in 
different scenarios. These are only examples, and you should feel free to segue to your view controller however you wish as long as it takes place within 
this block. Note that if you are using Cordova, React, or Xamarin there are example code blocks in their respective instructions. 

        // For Objective-C Projects:
        //////////////////////////////////////////////////////////////////
        
        // If your VC is listed in Storyboard as initial VC
        UIStoryboard *storyboard = [UIStoryboard storyboardWithName:@"Main" bundle:NSBundle.mainBundle];
        ViewController *viewController = [storyboard instantiateInitialViewController];
        [self.window setRootViewController:viewController];
        
        
        // For Swift Projects with SceneDelegate:
        //////////////////////////////////////////////////////////////////
        
        // Create the SwiftUI view that provides the window contents.
        // Use a UIHostingController as window root view controller.
        self.window?.rootViewController = UIHostingController(rootView: ContentView())
        self.window?.makeKeyAndVisible()
        
        
        // For Swift Projects with AppDelegate:
        //////////////////////////////////////////////////////////////////
        
        // If you've created your first VC programmatically
        let vc = ViewController()
        self.window?.rootViewController = vc

        // If your VC is listed in Storyboard as initial VC
        let storyboard = UIStoryboard(name: "Main", bundle: Bundle.main)
        let vc: ViewController = storyboard.instantiateInitialViewController() as! ViewController
        self.window?.rootViewController = vc

        // If your VC is listed in Storyboard
        let storyboard = UIStoryboard(name: "Main", bundle: Bundle.main)
        let vc = storyboard.instantiateViewController(withIdentifier: "Starting VC")
        self.window?.rootViewController = vc

        // If your initial VC needs a nav controller
        let storyboard = UIStoryboard(name: "Main", bundle: Bundle.main)
        let vc = storyboard.instantiateViewController(withIdentifier: "Starting VC")
        let navigationVC = UINavigationController(rootViewController: vc)
        self.window?.rootViewController = navigationVC

        // If your navigation controller is already in Storyboard
        let storyboard = UIStoryboard(name: "Main", bundle: Bundle.main)
        let navVC = storyboard.instantiateViewController(withIdentifier: "Nav VC")
        
        
        // For Xamarin Projects:
        //////////////////////////////////////////////////////////////////
        
        var storyboard = UIStoryboard.FromName("Main", null);
        var viewController = storyboard.InstantiateInitialViewController();
        Window.RootViewController = viewController;
        
        
        // For Cordova Projects:
        //////////////////////////////////////////////////////////////////
        
        self.viewController = [[MainViewController alloc] init];
        [super application:application didFinishLaunchingWithOptions:launchOptions];

        
8.  Allow OAuth redirection back into App handler.

        // For Objective-C Projects:
        //////////////////////////////////////////////////////////////////
        - (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options {
            return [Amp oauthStart:url];
        }
        

        // For Swift Projects:
        //////////////////////////////////////////////////////////////////
        func application(_ app: UIApplication, open url: URL, options: [UIApplication.OpenURLOptionsKey : Any] = [:]) -> Bool {
            return Amp.oauthStart(url)
        }
        
        
        // For Xamarin Projects:
        //////////////////////////////////////////////////////////////////
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options) {
            return Amp.OauthStart(url) || base.OpenUrl(app, url, options);
        }

9.  Disable third party keyboards. This is for security reasons, so that a user's username and password cannot be recorded. Add this method if it is 
not already in your _AppDelegate_.

        // For Objective-C Projects:
        //////////////////////////////////////////////////////////////////
        - (BOOL)application:(UIApplication *)application shouldAllowExtensionPointIdentifier:(UIApplicationExtensionPointIdentifier)extensionPointIdentifier {
            return ![extensionPointIdentifier isEqualToString: UIApplicationKeyboardExtensionPointIdentifier];
        }


        // For Swift Projects:
        //////////////////////////////////////////////////////////////////
        func application(_ application: UIApplication, shouldAllowExtensionPointIdentifier extensionPointIdentifier: UIApplication.ExtensionPointIdentifier) -> Bool {
            return extensionPointIdentifier != UIApplication.ExtensionPointIdentifier.keyboard
        }

10.  Modify your info.plist:  
    
    a.  Set the name of your app.  Add a new key named "Bundle display name" with a value of the name of your application
    
    b.  Enable location services.  Add a new key named "Privacy - Location When In Use Usage Description" with a value of "GEOINT App Store will access 
your location when opening this application."  If you already have a value here, you should keep your existing message
    
    c.  Enable biometric authentication.  Add a new key named "Privacy - Face ID Usage Description" with a value of "Use your stored credentials to log 
in to the GEOINT App Store."  If you already have a value here, you should keep your existing message.

11.	Run your application on a device, and you should see a login screen. As long as you used the TESTING environment in the start method, you should see a 
"TESTING" label at the bottom of the screen. This testing environment will simulate network behavior, but will not actually make any network calls. Feel free 
to test your app with it. Tap the Login button to test a successful login, and verify that the initial view controller you specified is displayed.


## Additional Cordova Instructions
You will only need to follow these instructions if you are using Cordova to develop your application. 

There is a bug in Cordova which does not generate the entitlements plist correctly. Please use the following steps to verify your entitlements. 
1.    Verify an entitlements plist exists in your Workspace. The file should end in the extension ".entitlements" or ".plist". 
2.    If no entitlements plist exists, add a new plist to your project. Add the Keychain Sharing group to this plist, and you may need to add other entitlements 
your project needs here as well. An entitlements plist with the Keychain Sharing group will look like this: 

            <xml version="1.0" encoding="UTF-8"?>
            <!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/ PropertyList-1.0.dtd">
            <plist version="1.0"> 
              <dict>
                <key>keychain-access-groups</key>
                <array>
                  <string>$(AppIdentifierPrefix)AMPGroup</string>
                </array>
              </dict> 
            </plist>
        
3.    Go to your project's Build Settings and search for "Code Signing Entitlements". Add the path to your entitlements plist here. 


## Implementing AMP in Xamarin for iOS

Adding the Binding Library
1.    Copy the "AMP.framework" file into your project folder on the file system.
2.    Right click your solution and select "Add" > "Add New Project". 
3.    Choose "iOS" > "Library" > "Bindings Library" as your template.
4.    Give the new project the name "AMPBinding".
5.    Open the "ApiDefinition.cs" and "Structs.cs" files in the AMPBinding project, and replace the entire contents of the files with the content from the included AMP files.
6.    Right click the "Native References" folder and select "Add Native Reference".
7.    Select the "AMP.framework" file from your project folder and click "Open".

Using AMP in Your Project
1.    In the Solution drawer, select your project and select "Project" > "Edit References".
2.    On the Projects tab, check the box next to "AMPBinding" and click "OK".
3.    Open the _"AppDelegate.cs"_ file in your project.
4.    Add the using statement "using AMPBinding;".
5.    Update the contents in FinishedLaunching to include a call to the AMP.StartWithWindow method
6.    Update the contents of OpenUrl to include a call to the AMP.OauthStart method
 

## AMP Options 

### Start Method Completion Block

The Start method is used to authenticate a user with the GEOINT App Store and activate the application. If you would like to ensure a block of code is called 
after AMP has finished with its Start method, you can pass a completion block into the Start method. Note that using the Start method with a completion 
handler should replace your existing call to Start.

        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        NSString *signature = @"exampleSignature";
        [Amp startWith:self.window environment:AmpEnvironmentNGA_PRODUCTION signature:signature completion:^{
            // Place your completion code here
            ...
        }];


        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        let signature: String = "exampleSignature"
        Amp.start(with: window, environment: .NGA_PRODUCTION, signature: signature) {
            // Place your completion code here
            ...
        }
        
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        var signature = "exampleSignature";
        Amp.StartWithWindow(Window, AmpEnvironment.NgaProduction, signature, () => {
            // Place your completion code here
            ...
        }
        
### OAuth Completion Block
        
This method allows for OAuth using GEOAxIS.  The AMP UI will show a "Login with PKI" button that will open a web browser to GEOAxIS to allow PKI authentication.  
This will redirect back into the AMP app and a handler needs added to process the redirect result.  This is that handler: 
        
        // For Objective-C Projects:
        //////////////////////////////////////////////////////////////////
        - (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options {
            return [Amp oauthStart:url];
        }
    
    
        // For Swift Projects:
        //////////////////////////////////////////////////////////////////
        func application(_ app: UIApplication, open url: URL, options: [UIApplication.OpenURLOptionsKey : Any] = [:]) -> Bool {
            return Amp.oauthStart(url)
        }
        
        
        // For Xamarin Projects:
        //////////////////////////////////////////////////////////////////
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options) {
            return Amp.OauthStart(url) || base.OpenUrl(app, url, options);
        }

### Device ID

This method will return a device ID associated with the device the user has installed the app on. This device ID will match what the GEOINT App Store uses to 
identify the device.

        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        NSString *deviceId = [Amp getDeviceId];

        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        let deviceId: String? = Amp.getDeviceId()
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        var deviceId = Amp.GetDeviceId;

### Login ID

This method will return a login ID unique to the user who activates the application, and this login ID remains the same across devices for this user. Note that this 
method will only return a value after the Start method has finished running. Otherwise it returns an empty string. For this reason, you may want to access login ID 
in the completion handler for the Start method.

        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        NSString *loginId = [Amp getLoginId];

        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        let loginId: String? = Amp.getLoginId()
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        var loginId = Amp.GetLoginId;
        
### Usage Count
        
This method will return a count of the number of usages of your AMP app.  This usage count is used to prompt a ratings dialogue.
        
        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        NSInteger usageCount = [Amp getUsageCount];
        
        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        let usageCount: Int = Amp.getUsageCount()
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        var usageCount = Amp.GetUsageCount;
        
### AMP Version
        
This method will return the version number of the AMP framework.
        
        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        NSString *ampVersion = [Amp getVersionNumber];
        
        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        let ampVersion: String = Amp.getVersionNumber()
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        var ampVersion = Amp.GetVersionNumber;

### In-App Reviews

This method allows you to specify a number of usages (any integer greater than zero) of your application before a review prompt is displayed. This review will appear in 
the GEOINT App Store for your product. Regardless of if you implement this option, users still have the ability to add a review in the GEOINT App Store.

The user has the options of leaving a review, dismissing and never showing the review prompt again, or dismissing the review prompt this time (it will not appear again 
until the user has completed the same amount of usages again). Note that this method must be called prior to calling the start method.

        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        [Amp setUsagesBeforeRatingsPrompt:5];
        [Amp startWith ...

        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        Amp.setUsagesBeforeRatingsPrompt(5)
        Amp.start ...
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        Amp.SetUsagesBeforeRatingsPrompt(5);
        Amp.StartWithWindow ...

### Network Availability

This method will return true if the GEOINT App Store is available on the network.

        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        BOOL networkAvailable = [Amp isNetworkAvailable];

        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        let networkAvailable: Bool = Amp.isNetworkAvailable()
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        var networkAvailable = Amp.IsNetworkAvailable;

## Production Instructions for Admin Use

Note: Vendors do not need to follow these steps. These instructions are for an admin to complete the implementation of AMP. Verify that all steps of the vendor 
instructions have been completed before completing these production instructions.

1.	Set the login requirement if needed. This must be set prior to the Amp start method being called. By default, AMP will only require the user to authenticate 
once to use the application, and an internet connection is required. You should continue to the next step if login requirement ONCE is what you need (since it is 
the default, you do not need to add this code before the Start call).

    If the user should authenticate every time the application is relaunched, set the login requirement to ALWAYS.

    **ALWAYS Authenticate Examples**
    
        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        [Amp setLoginRequirement:AmpLoginRequirementALWAYS];
        [Amp startWith ...

        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        Amp.setLoginRequirement(.ALWAYS)
        Amp.start ...
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        Amp.SetLoginRequirement(AmpLoginRequirement.Always);
        Amp.StartWithWindow ...

2.	Use the following parameters to update the Start method:

    **Environment** – Choose the GEOINT App Store website you are building this application for.
    
        -------------------------------------------------------------------------------------------
        | URL                  | Objective-C Variable          | Swift Variable | Xamarin Variable |
        -------------------------------------------------------------------------------------------
        | apps-stage.nga.mil   | AmpEnvironmentNGA_STAGE       | NGA_STAGE      | NgaStage         |
        | apps-igapp.nga.mil   | AmpEnvironmentNGA_IGAPP       | NGA_IGAPP      | NgaIgapp         |
        | app.nga.mil          | AmpEnvironmentNGA_PRODUCTION  | NGA_PRODUCTION | NgaProduction    |
        -------------------------------------------------------------------------------------------
    
    **Signature** – A string of characters located in the Metrics tab on a product's GEOINT App Store page. The signature is unique to each version of a product.
    
    **Completion** – The vendor may have provided a completion block with the Start method. You should retain their completion block if there is one.
    
    **Start With Completion Examples**
    
        // For Objective-C Projects
        //////////////////////////////////////////////////////////////////
        NSString *signature = @"exampleSignature";
        [Amp startWith:self.window environment:AmpEnvironmentNGA_PRODUCTION signature:signature completion:^{
            // Vendor’s completion code here
            ...
        }];
        
        
        // For Swift Projects
        //////////////////////////////////////////////////////////////////
        let signature: String = "exampleSignature"
        Amp.start(with: window, environment: .NGA_PRODUCTION, signature: signature) {
            // Vendor’s completion code here
            ...
        }
        
        
        // For Xamarin Projects
        //////////////////////////////////////////////////////////////////
        var signature = "exampleSignature";
        Amp.StartWithWindow(Window, AmpEnvironment.NgaProduction, signature, () => {
            // Place your completion code here
            ...
        }


## Contact

Website: [apps.nga.mil](https://apps.nga.mil)

Government Team: [geoint.appstoreteam@nga.mil](mailto:geoint.appstoreteam@nga.mil)

Development Team: [gasdevteam@feditc.com](mailto:gasdevteam@feditc.com)
