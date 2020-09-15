/*:
 # AMP

 ## Overview

 The GEOINT App Store AIMS Mobile Platform (AMP) for iOS is an XCFramework package used to enable management of installs,
 subscription licenses, and other metrics about the [IGAPP](https://igapp.com/) program products available on the [GEOINT
 App Store (GAS)](https://apps.nga.mil) website. Adding AMP is a requirement of [IGAPP](https://igapp.com/) applications.
 This document will explain the process of adding AMP to an iOS application.


 ## Module

 One module is vended by this package: AMP

 */
import AMP
/*:

 ## Adding AMP to your project

 The easiest way to add AMP is through Swift Package Manager if using XCode 12 or later:
 1.  Open your project, navigate to 'File' > 'Swift Packages' > 'Add Package Dependency...'.
 2.  Select your project and click 'Next' (if required).
 3.  Enter this repository URL and tap 'Next'
 4.  Choose the version you want to use (ideally the latest version) and click 'Next'
 5.  Choose the target and click 'Finish'


 ## Implementing AMP in iOS

 1.  In the Signing & Capabilities tab of your target, enable Keychain Sharing and add "AMPGroup" to your list of groups.
 2.  In the Info tab of your target, add the URL Type of "$(PRODUCT_BUNDLE_IDENTIFIER)"
 3.  In the Build Settings tab of your target, set the Enable Bitcode setting to "No".
 4.  If using a _SceneDelegate_, skip to next step, otherwise, go to your _AppDelegate_ class and add the following to your
 applicationDidFinishLaunchingWithOptions override.

 */

import UIKit
import AMP

class AppDelegate: UIResponder, UIApplicationDelegate {
    var window: UIWindow?

    // Add in AppDelegate.swift
    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        window = UIWindow(frame: UIScreen.main.bounds)
        let signature = "test"
        Amp.start(with: window, environment: .TESTING, signature: signature) {
            // TODO - Your segue will go here. See step 7 for more details.
        }
        return true
    }
}

/*:
 6. If using a _SceneDelegate_, go to your _SceneDelegate_ class and add the following to your scene function.
 */

class SceneDelegate: UIResponder, UIWindowSceneDelegate {
    var window: UIWindow?

    // Add in SceneDelegate.swift
    func scene(_ scene: UIScene, willConnectTo session: UISceneSession, options connectionOptions: UIScene.ConnectionOptions) {
        guard let windowScene = (scene as? UIWindowScene) else { return }

        self.window = UIWindow(windowScene: windowScene)
        self.window?.rootViewController = UIViewController(nibName: nil, bundle: nil) // This placeholder VC will be replaced by AMP VC
        self.window?.makeKeyAndVisible()

        // Start AMP
        let signature = "test"
        Amp.start(with: self.window, environment: .TESTING, signature: signature) {
            // TODO - Your segue will go here. See step 7 for more details.

        }
    }
}

/*:
 7.  Fill in the start method’s completion block with a segue to your initial view controller. Below are several examples
 of how this can be accomplished in different scenarios. These are only examples, and you should feel free to segue to your
 view controller however you wish as long as it takes place within this block.
 */

//////////////////////////////////////////////////////////////////
// START playground specific code
// The following is just here for the playground to compile. This is already defined in the AppDelegate or SceneDelegate
import SwiftUI
var window: UIWindow?
struct ContentView: View { var body: some View { VStack {} } }
class ViewController: UIViewController { }
// END playground specific code
//////////////////////////////////////////////////////////////////


// For Swift Projects with SceneDelegate:
//////////////////////////////////////////////////////////////////

// Create the SwiftUI view that provides the window contents.
// Use a UIHostingController as window root view controller.
window?.rootViewController = UIHostingController(rootView: ContentView())
window?.makeKeyAndVisible()


// For Swift Projects with AppDelegate:
//////////////////////////////////////////////////////////////////

// Example A: If you've created your first VC programmatically
let vc_a = ViewController()
window?.rootViewController = vc_a

// Example B: If your VC is listed in Storyboard as initial VC
let storyboard_b = UIStoryboard(name: "Main", bundle: Bundle.main)
let vc_b: ViewController = storyboard_b.instantiateInitialViewController() as! ViewController
window?.rootViewController = vc_b

// Example C: If your VC is listed in Storyboard
let storyboard_c = UIStoryboard(name: "Main", bundle: Bundle.main)
let vc_c = storyboard_c.instantiateViewController(withIdentifier: "Starting VC")
window?.rootViewController = vc_c

// Example D: If your initial VC needs a nav controller
let storyboard_d = UIStoryboard(name: "Main", bundle: Bundle.main)
let vc_d = storyboard_d.instantiateViewController(withIdentifier: "Starting VC")
let navigationVC_d = UINavigationController(rootViewController: vc_d)
window?.rootViewController = navigationVC_d

// Example E: If your navigation controller is already in Storyboard
let storyboard_e = UIStoryboard(name: "Main", bundle: Bundle.main)
let navVC_e = storyboard_e.instantiateViewController(withIdentifier: "Nav VC")

/*:
 8.  Allow OAuth redirection back into App handler.  This method allows for OAuth using GEOAxIS.  The AMP UI will show a "Login with PKI" button that will open a
 web browser to GEOAxIS to allow PKI authentication.  This will redirect back into the AMP app and a handler needs added to process the  redirect result.  This is that handler:
 */

func application(_ app: UIApplication, open url: URL, options: [UIApplication.OpenURLOptionsKey : Any] = [:]) -> Bool {
    return Amp.oauthStart(url)
}

/*:
 9.  Disable third party keyboards. This is for security reasons, so that a user's username and password cannot be recorded. Add this method if it is
 not already in your _AppDelegate_.
 */

func application(_ application: UIApplication, shouldAllowExtensionPointIdentifier extensionPointIdentifier: UIApplication.ExtensionPointIdentifier) -> Bool {
    return extensionPointIdentifier != UIApplication.ExtensionPointIdentifier.keyboard
}

/*:
10.  Modify your info.plist:
        1.  Set the name of your app.  Add a new key named "Bundle display name" with a value of the name of your application
        2.  Enable location services.  Add a new key named "Privacy - Location When In Use Usage Description" with a value
 of "GEOINT App Store will access your location when opening this application."  If you already have a value here, you should
 keep your existing message.
        3.  Enable biometric authentication.  Add a new key named "Privacy - Face ID Usage Description" with a value of "Use
 your stored credentials to log in to the GEOINT App Store."  If you already have a value here, you should keep your existing
 message.

11.  Run your application on a device, and you should see a login screen. As long as you used the TESTING environment in the
 start method, you should see a "TESTING" label at the bottom of the screen. This testing environment will simulate network
 behavior, but will not actually make any network calls. Feel free to test your app with it. Tap the Login button to test a
 successful login, and verify that the initial view controller you specified is displayed.


## AMP Options

### Start Method Completion Block

The Start method is used to authenticate a user with the GEOINT App Store and activate the application. If you would like to
ensure a block of code is called after AMP has finished with its Start method, you can pass a completion block into the Start
method. Note that using the Start method with a completion handler should replace your existing call to Start.

*/

let signature: String = "exampleSignature"
Amp.start(with: window, environment: .NGA_PRODUCTION, signature: signature) {
    // TODO - Place your completion code here
}

/*:
 ### Device ID

 This method will return a device ID associated with the device the user has installed the app on. This device ID will match
 what the GEOINT App Store uses to identify the device.

 */

let deviceId: String? = Amp.getDeviceId()

/*:
 ### Login ID

 This method will return a login ID unique to the user who activates the application, and this login ID remains the same
 across devices for this user. Note that this method will only return a value after the Start method has finished running.
 Otherwise it returns an empty string. For this reason, you may want to access login ID in the completion handler for the
 Start method.

 */

let loginId: String? = Amp.getLoginId()

/*:
 ### Usage Count

 This method will return a count of the number of usages of your AMP app.  This usage count is used to prompt a ratings dialogue.

 */

let usageCount: Int = Amp.getUsageCount()

/*:
 ### AMP Version

 This method will return the version number of the AMP framework.

 */

let ampVersion: String = Amp.getVersionNumber()

/*:
 ### In-App Reviews

 This method allows you to specify a number of usages (any integer greater than zero) of your application before a review prompt is displayed. This review will appear in
 the GEOINT App Store for your product. Regardless of if you implement this option, users still have the ability to add a review in the GEOINT App Store.

 The user has the options of leaving a review, dismissing and never showing the review prompt again, or dismissing the review prompt this time (it will not appear again
 until the user has completed the same amount of usages again). Note that this method must be called prior to calling the start method.

 */

Amp.setUsagesBeforeRatingsPrompt(5)
// Then call Amp.start ...

/*:
 ### Network Availability

 This method will return true if the GEOINT App Store is available on the network.

 */

let networkAvailable: Bool = Amp.isNetworkAvailable()

