//////////////////////////////////////////////////////////////////////////
//                                                                      //
//  NGA GEOINT App Store - Application Management Platform (AMP)        //
//                                                                      //
//////////////////////////////////////////////////////////////////////////
//                                                                      //
//  For questions related to the usage of this library, please contact  //
//  the GEOINT App Store Team.                                          //
//                                                                      //
//  Website:    https://apps.nga.mil                                    //
//  Email:      geoint.appstoreteam@nga.mil                             //
//                                                                      //
//  Updated:    2021-02-03                                              //
//  Version:    3.4                                                     //
//                                                                      //
//  Copyright © 2019-2021 NGA. All rights reserved.                     //
//                                                                      //
//////////////////////////////////////////////////////////////////////////

using System;
using Foundation;
using UIKit;

/// <summary>
/// This namespace includes proxy classes for wrapping AMP.framework Swift library.
/// </summary>
namespace AMPBinding
{
    /// <summary>
    /// AMP is the application management platform used for applications distributed
    /// by the GEOINT App Store.
    /// </summary>
    // @interface Amp : NSObject
    [BaseType(typeof(NSObject))]
    interface Amp
    {
        #region -- Vendor Methods --

        /// <summary>
        /// Starts AMP's authentication and validates application.
        /// This method will authenticate a user with the GEOINT App Store.
        /// It will also verify that the user is allowed to use the application and
        /// manage things like ratings prompts and subscription renewals.
        /// </summary>
        /// <param name="window">An initialized UIWindow created in your UIApplicationDelegate.</param>
        /// <param name="environment">This determines which server AMP should use. Use
        /// the "TESTING" environment to simulate server calls.</param>
        /// <param name="signature">This string validates your app with the selected server.
        /// Use any non-nil string for testing purposes.</param>
        /// <param name="completion">This completion block will be called after AMP has
        /// finished its processes. Use this block to segue into whichever View Controller
        /// you wish to start the application with.</param>
        // +(void)startWithWindow:(UIWindow *)window environment:(AmpEnvironment)environment signature:(NSString *)signature completion:(AmpCompletion)completion;
        // +(void)startWith:(UIWindow * _Nullable)window environment:(enum AmpEnvironment)environment signature:(id)signature completion:(void (^ _Nonnull)(void))completion;
        [Static]
        [Export("startWith:environment:signature:completion:")]
        void StartWithWindow([NullAllowed] UIWindow window, AmpEnvironment environment, string signature, Action completion);

        /// <summary>
        /// This enables AMP to use GEOAxIS Website OAuth for authentication. This must be placed in your AppDelegate's application:openUrl:options: override.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        // +(BOOL)oauthStart:(NSURL *)url;
        // +(id)oauthStart:(id)url __attribute__((warn_unused_result));
        [Static]
        [Export("oauthStart:")]
        bool OauthStart(NSUrl url);

        /// <summary>
        /// This will tell you if the user has a rating for your product.
        /// Return: This will be NONE if the user has no ratings for this product, NEW if
        /// they have a rating for this version, and OLD if they have a rating for a different
        /// version of this product.
        /// </summary>
        // +(AmpRatingsStatus)getRatingsStatus;
        // +(enum AmpRatingsStatus)getRatingsStatus __attribute__((warn_unused_result));
        [Static]
        [Export("getRatingsStatus")]
        //[Verify(MethodToProperty)]
        AmpRatingsStatus GetRatingsStatus { get; }

        /// <summary>
        /// Display a ratings prompt.
        /// You will be responsible for handling and remembering the user's action.
        /// 
        /// The ratings prompt allows the user to leave a
        /// rating/review for your product which will appear in the
        /// GEOINT App Store for your product.You should handle the resulting
        /// AmpRatingsResult accordingly.
        ///
        /// Please note that we provide a getRatingsStatus method which will tell you if
        /// the user has a rating for your product. If they do, we will tell you whether the
        /// rating is for the currently running version of the product or for an older version.
        ///
        /// If the user submits a rating, we suggest not prompting them again unless you have
        /// released a major update to your application.
        ///
        /// If the user dismisses the prompt, you may want to prompt them on next use
        /// or wait a certain amount of usages before prompting again.
        ///
        /// If the user chooses the "Never Show Again" option,
        /// we expect you to remember this and never prompt the user again (unless you
        /// have released a major update to your application).
        /// </summary>
        /// <param name="completion">This completion block is called after the user has finished
        /// using the ratings prompt.The completion block will be passed the ratings result.</param>
        // +(void)displayRatingsPromptWithCompletion:(void (^)(AmpRatingsResult))completion;
        // +(void)displayRatingsPrompt:(void (^ _Nonnull)(enum AmpRatingsResult))completion;
        [Static]
        [Export("displayRatingsPrompt:")]
        void DisplayRatingsPromptWithCompletion(Action<AmpRatingsResult> completion);

        /// <summary>
        /// Set the number of times a user should use your app before being
        /// prompted for a rating/review. We will handle the user's action.
        ///
        /// We will display the ratings prompt for you.The user must open your
        /// app for the set amount of usages you provide here, and they will be prompted
        /// for a rating on the next relaunch.We keep track of these usages for you, and
        /// we will handle whatever action the user takes.We will not display a ratings
        /// prompt to the user if they already have a rating in our App Store for your product.
        /// </summary>
        /// <param name="usages">The number of times a user must use the application before a ratings prompt.</param>
        // +(void)setUsagesBeforeRatingsPrompt:(int)usages;
        // +(void)setUsagesBeforeRatingsPrompt:(id)usages;
        [Static]
        [Export("setUsagesBeforeRatingsPrompt:")]
        void SetUsagesBeforeRatingsPrompt(int usages);

        /// <summary>
        /// Get the device ID that the GEOINT App Store has associated with the device.
        /// </summary>
        // +(NSString *)getDeviceId;
        // +(id)getDeviceId __attribute__((warn_unused_result));
        [Static]
        [Export("getDeviceId")]
        //[Verify(MethodToProperty)]
        string GetDeviceId { get; }

        /// <summary>
        /// Get the login ID that the GEOINT App Store has associated with the user.
        /// </summary>
        // +(NSString *)getLoginId;
        // +(id)getLoginId __attribute__((warn_unused_result));
        [Static]
        [Export("getLoginId")]
        //[Verify(MethodToProperty)]
        string GetLoginId { get; }

        // +(id)getUsageCount __attribute__((warn_unused_result));
        [Static]
        [Export("getUsageCount")]
        //[Verify(MethodToProperty)]
        int GetUsageCount { get; }

        /// <summary>
        /// Get the version number of AMP
        /// </summary>
        // +(NSString *)getVersionNumber;
        // +(id)getVersionNumber __attribute__((warn_unused_result));
        [Static]
        [Export("getVersionNumber")]
        //[Verify(MethodToProperty)]
        string GetVersionNumber { get; }

        /// <summary>
        /// Checks if the device is online vs offline
        /// </summary>
        // +(id)isNetworkAvailable __attribute__((warn_unused_result));
        [Static]
        [Export("isNetworkAvailable")]
        //[Verify(MethodToProperty)]
        bool IsNetworkAvailable { get; }

        #endregion

        #region -- Environment Variables -- ** FOR ADMIN USE ONLY **

        /// <summary>
        /// Determines how often a user should be required to login.
        /// </summary>
        /// <param name="loginRequirement">How often a user should be required to login.</param>
        // +(void)setLoginRequirement:(enum AmpLoginRequirement)loginRequirement;
        [Static]
        [Export("setLoginRequirement:")]
        void SetLoginRequirement(AmpLoginRequirement loginRequirement);

        /// <summary>
        /// Should users be able to use a License Key (provided by Device Officers) instead of a GAS account.
        /// </summary>
        /// <param name="shouldAllowLicenseKeys">Should users be able to use a License Key (provided by Device Officers) instead of a GAS account.</param>
        // +(void)setAllowLicenseKeys:(BOOL)shouldAllowLicenseKeys;
        [Static]
        [Export("setAllowLicenseKeys:")]
        void SetAllowLicenseKeys(bool shouldAllowLicenseKeys);

        #endregion
    }
}
