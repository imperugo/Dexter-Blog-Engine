#region Disclaimer/Info

// ////////////////////////////////////////////////////////////////////////////////////////////////
// File:			TrackingConfigurationBinder.cs
// Website:		http://dexterblogengine.com/
// Authors:		http://dexterblogengine.com/aboutus
// Created:		2013/04/01
// Last edit:	2013/04/01
// License:		New BSD License (BSD)
// For updated news and information please visit http://dexterblogengine.com/
// Dexter is hosted to Github at https://github.com/imperugo/Dexter-Blog-Engine
// For any question contact info@dexterblogengine.com
// ////////////////////////////////////////////////////////////////////////////////////////////////

#endregion

namespace Dexter.Host.Areas.Dxt_Admin.Binders
{
	public class TrackingConfigurationBinder
	{
		#region Constructors and Destructors

		public TrackingConfigurationBinder()
		{
			this.EnablePingBackReceive = true;
			this.EnablePingBackSend = true;
			this.EnableReferrerTracking = true;
			this.EnableTrackBackReceive = true;
			this.EnableTrackBackSend = true;
		}

		#endregion

		#region Public Properties

		public bool EnablePingBackReceive { get; set; }

		public bool EnablePingBackSend { get; set; }

		public bool EnableReferrerTracking { get; set; }

		public bool EnableTrackBackReceive { get; set; }

		public bool EnableTrackBackSend { get; set; }

		#endregion
	}
}