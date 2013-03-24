/**
 * Older browsers detection
 */
 
(function($)
{
	// Change these values to fit your needs
	if (
		($.browser.msie && parseFloat($.browser.version) < 7) ||			// IE 6 and lower
		($.browser.mozilla && parseFloat($.browser.version) < 1.9) ||		// Firefox 2 and lower
		($.browser.opera && parseFloat($.browser.version) < 9) ||			// Opera 8 and lower
		($.browser.webkit && parseInt($.browser.version) < 400)				// Older Chrome and Safari
	) {
		// If no cookie has been set
		if (getCookie('forceAccess') !== 'yes')
		{
			// If coming back from the old browsers page
			if (window.location.search.indexOf('forceAccess=yes') > -1)
			{
				// Mark for future tests
				setCookie('forceAccess', 'yes');
			}
			else
			{
				document.location.href = 'old-browsers.html?redirect='+escape(document.location.href);
			}
		}
	}
	
	/**
	 * Get cookie params
	 * @return object an object with every params in the cookie
	 */
	function getCookieParams()
	{
		var parts = document.cookie.split(/; */g);
		var params = {};
		
		for (var i = 0; i < parts.length; ++i)
		{
			var part = parts[i];
			if (part)
			{
				var equal = part.indexOf('=');
				if (equal > -1)
				{
					var param = part.substr(0, equal);
					var value = unescape(part.substring(equal+1));
					params[param] = value;
				}
			}
		}
		
		return params;
	}
	
	/**
	 * Get a cookie value
	 * @param string name the cookie name
	 * @return string the value, or null if not defined
	 */
	function getCookie(name)
	{
		var params = getCookieParams();
		return params[name] || null;
	}
	
	/**
	 * Write a cookie value
	 * @param string name the cookie name
	 * @param string value the value
	 * @param int days number of days for cookie life
	 * @return void
	 */
	function setCookie(name, value, days)
	{
		var params = getCookieParams();
		
		params[name] = value;
		
		if (days)
		{
			var date = new Date();
			date.setTime(date.getTime()+(days*24*60*60*1000));
			params.expires = date.toGMTString();
		}
		
		var cookie = [];
		for (var thevar in params)
		{
			cookie.push(thevar+'='+escape(params[thevar]));
		}
		document.cookie = cookie.join('; ');
	}
	
})(jQuery);