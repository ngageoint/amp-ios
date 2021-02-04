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
using ObjCRuntime;

/// <summary>
/// This namespace includes proxy classes for wrapping AMP.framework Swift library.
/// </summary>
namespace AMPBinding
{
	/// <summary>
	/// This determines the environment AMP should use.
	/// </summary>
	[Native]
	public enum AmpEnvironment : long
	{
		/// <summary>
		/// This environment will simulate network calls, but will not actually connect to the GEOINT App Store's servers.
		/// </summary>
		Testing = 0,
		/// <summary>
		///  This is the NGA staging environment for the GEOINT App Store.
		/// </summary>
		NgaStage = 1,
		/// <summary>
		/// This is the IGAPP testing environment for the GEOINT App Store.
		/// </summary>
		NgaIgapp = 2,
		/// <summary>
		/// This is the production environment for the GEOINT App Store.
		/// </summary>
		NgaProduction = 3,
		/// <summary>
		/// This is a CTI testing environment for GEOINT App Store developers.
		/// </summary>
		CtiDmz = 4,
        /// <summary>
        /// This is the AZURE testing environment for GEOINT App Store developers.
        /// </summary>
        AzureTest = 5
	}

	/// <summary>
	/// This determines how often a user must login.
	/// </summary>
	[Native]
	public enum AmpLoginRequirement : long
	{
		/// <summary>
		/// This is the default value. Authentication is only required one time.
		/// </summary>
		Once = 0,
		/// <summary>
		/// Authentication is required every time the application is launched.
		/// </summary>
		Always = 1
	}

	/// <summary>
	/// This tells you if the user has a rating for your product in the GEOINT App Store.
	/// </summary>
	[Native]
	public enum AmpRatingsStatus : long
	{
		/// <summary>
		/// The user has no rating for your product.
		/// </summary>
		None = 0,
		/// <summary>
		/// The user has a rating for this version of your product.
		/// </summary>
		New = 1,
		/// <summary>
		/// The user has a rating for a different version of this product.
		/// </summary>
		Old = 2
	}

	/// <summary>
	/// This is the result of a user's action in a ratings prompt.
	/// </summary>
	[Native]
	public enum AmpRatingsResult : long
	{
		/// <summary>
		/// The user has successfully submitted a rating to the GEOINT App Store.
		/// </summary>
		Submitted = 0,
		/// <summary>
		/// The user has dismissed the ratings prompt without leaving a rating.
		/// </summary>
		Dismissed = 1,
		/// <summary>
		/// The user never wants to see this ratings prompt again.
		/// </summary>
		NeverDisplayAgain = 2
	}
}
