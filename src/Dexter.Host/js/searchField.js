/**
 * Template plugin for searchField
 */

(function($)
{
	/**
	 * Timeout for ajax requests
	 * @var int
	 */
	var ajaxTimeout;
	
	/**
	 * Timeout for search block close
	 * @var int
	 */
	var closeTimeout;
	
	/**
	 * Last request timer
	 * @var int
	 */
	var lastRequest;
	
	/**
	 * Setup the search field
	 * @param object options an object with any of the options defined below in the function's defaults
	 */
	$.fn.advancedSearchField = function(options)
	{
		var settings = $.extend({}, $.fn.advancedSearchField.defaults, options);
		
		// Setup
		this.before('<input type="hidden" name="search-last" id="search-last" value="">').focus(function() {
			
			// Stop closing
			if (closeTimeout)
			{
				clearTimeout(closeTimeout);
			}
			
			// Add event listener
			$(this).bind('keyup', updateSearch);
			updateSearch();
			
		}).blur(function() {
			
			// Remove event listener
			$(this).unbind('keyup', updateSearch);
			
			// Timeout for result hiding - needed for search block interactions
			closeTimeout = setTimeout(closeSearch, 500);
			
		}).after('<div id="search-result" class="result-block"><span class="arrow"><span></span></span><div id="server-search">'+settings.messageLoading+'</div><p id="search-info" class="result-info">-</p></div>').next().hide();
		
		/**
		 * Hide search result block
		 * @return void
		 */
		function closeSearch()
		{
			// Hide elements
			$('#s').hideTip();
			$('#search-result').fadeOut();
		};
		
		/**
		 * Update the search field results block
		 * @return void
		 */
		function updateSearch()
		{
			// Elements
			var result = $('#search-result');
			
			// Current search
			var s = $.trim($('#s').val());
			
			// Parsing
			if (s.length == 0)
			{
				// Tip
				$('#s').showTip({
					content: settings.tipFocus
				});
				
				result.fadeOut();
			}
			else if (s.length < settings.minSearchLength)
			{
				// Tip
				$('#s').showTip({
					content: settings.tipTooShort
				});
				
				result.fadeOut();
			}
			else
			{
				var last = $('#search-last');
				var lastS = last.val();
				var info = $('#search-info');
				
				// Hide tip
				$('#s').hideTip();
				
				// Show search results block
				result.fadeIn();
				
				// Detect changes
				if (lastS != s)
				{
					// Store search
					$('#search-last').val(s);
					
					// Stop previous request timeout
					if (ajaxTimeout)
					{
						clearTimeout(ajaxTimeout);
					}
					
					// Empty block
					result.children().not('.arrow, #server-search, #search-info').remove();
					
					// If search within nav
					if (settings.enableNavSearch)
					{
						result.children('.arrow:first').after('<h2>'+settings.titleTemplateResult+'</h2>'+searchInNav(s)+'<hr>');
						
						// If hiding too long matches list
						if ($.fn.accessibleList && (settings.moreButtonAfter > 0 || settings.matchesPerPage > 0))
						{
							result.children('ul:first').accessibleList({
								'moreAfter': settings.moreButtonAfter,
								'pageSize': settings.matchesPerPage,
								'after': function()
								{
									// Restore focus
									$('#s').focus();
								}
							});
						}
					}
					
					// Message
					$('#search-info').addClass('loading').text(settings.messageLoading);
					
					// Ajax call
					var date = new Date();
					if (!lastRequest || lastRequest < date.getTime()-noUpdateDelay)
					{
						var delay = settings.firstRequestDelay;
					}
					else
					{
						var delay = settings.nextRequestDelay;
					}
					ajaxTimeout = setTimeout(sendRequest, delay);
				}
			}	
		};
		
		/**
		 * Search within the main nav
		 * @param string s the search string
		 * @return void
		 */
		function searchInNav(s)
		{
			// Split keywords
			var keywords = s.toLowerCase().split(/\s+/);
			var nbKeywords = keywords.length;
			
			// Search links
			var links = $('nav a');
			var matches = [];
			links.each(function(i)
			{
				var text = $(this).text().toLowerCase();
				var textMatch = true;
				for (var i = 0; i < nbKeywords; ++i)
				{
					if (text.indexOf(keywords[i]) == -1)
					{
						textMatch = false;
						break;
					}
				}
				
				if (textMatch)
				{
					// All keywords found
					matches.push(this);
				}
			});
			
			// Build results list
			var nbMatches = matches.length;
			if (nbMatches > 0)
			{
				var output = '<p class="results-count"><strong>'+nbMatches+'</strong> match'+((nbMatches > 1) ? 'es' : '')+'</p>';
				output += '<ul class="small-files-list icon-html">';
				
				for (var m = 0; m < nbMatches; ++m)
				{
					// Text with highlighted keywords
					var link = $(matches[m]);
					var text = link.text();
					var path = [text];
					for (var i = 0; i < nbKeywords; ++i)
					{
						text = text.replace(new RegExp('('+keywords[i]+')', 'gi'), '<strong>$1</strong>');
					}
					
					// Path
					var parent = link;
					while ((parent = parent.parent().parent().prev('a')) && parent.length > 0)
					{
						path.push(parent.text());
					}
					
					output += '<li><a href="'+matches[m].href+'">'+text+'<br><small>'+path.reverse().join(' > ')+'</small></a></li>';
				}
				
				return output+'</ul>';
			}
			else
			{
				return '<p class="results-count">'+settings.messageNoMatches+'</p>';
			}
		};
		
		/**
		 * Send search request to server
		 */
		function sendRequest()
		{
			// Search url
			var url = $('#s').parents('form:first').attr('action');
			if (!url || url == '')
			{
				// Page url without hash
				url = document.location.href.match(/^([^#]+)/)[1];
			}
			
			var date = new Date();
			var timer = date.getTime();
			$('#server-search').load(url, {
				's': $('#search-last').val(),
				'timer': timer
			}, function(responseText, textStatus, XMLHttpRequest)
			{
				if (textStatus == 'success' || textStatus == 'notmodified')
				{
					$('#search-info').removeClass('loading').html(settings.messageSearchDone);
				}
				else
				{
					$('#server-search').html('<p class="error-message">'+settings.messageErrorFull+'</p>');
					$('#search-info').removeClass('loading').html(settings.messageError);
				}
			});
		};
		
		return this;
	};
	
	// Function's default configuration
	$.fn.advancedSearchField.defaults = {
		/**
		 * Minimum search string length
		 * @var int
		 */
		minSearchLength: 2,
		
		/**
		 * Max number of visible matches in each list above 'more' button (0 for no masking)
		 * @var int
		 */
		moreButtonAfter: 3,
		
		/**
		 * Number of visible matches per page (0 for no pagination)
		 * @var int
		 */
		matchesPerPage: 5,
		
		/**
		 * Delay before first request in ms (avoid multiple request while user types)
		 * @var int
		 */
		firstRequestDelay: 250,
		
		/**
		 * Minimum delay between search requests in ms (reduces server load)
		 * @var int
		 */
		nextRequestDelay: 750,
		
		/**
		 * Minimum delay upon which the plugin considers the user has stopped typing long enough 
		 * to start the next request with firstRequestDelay delay, thus responding faster
		 * @var int
		 */
		noUpdateDelay: 3000,
		
		/**
		 * Enable search within navigation
		 * @var boolean
		 */
		enableNavSearch: true,
		
		/**
		 * Message when user focuses the search field
		 * @var string
		 */
		tipFocus: 'Enter your search',
		
		/**
		 * Message is search is too short
		 * @var string
		 */
		tipTooShort: 'Enter at least 2 chars',
		
		/**
		 * Message while loading
		 * @var string
		 */
		messageLoading: 'Loading results...',
		
		/**
		 * Message when no matches found
		 * @var string
		 */
		messageNoMatches: 'No matches',
		
		/**
		 * Message when the search request is done
		 * @var string
		 */
		messageSearchDone: 'Not found? <a href="#">Try advanced search &raquo;</a>',
		
		/**
		 * Full message if an error occurs while loading results
		 * @var string
		 */
		messageErrorFull: 'Error while loading results, please try again',
		
		/**
		 * Status message if an error occurs while loading results
		 * @var string
		 */
		messageError: 'Error while loading',
		
		/**
		 * Title of the result section showing template nav items
		 * @var string
		 */
		titleTemplateResult: 'Admin pages'
	};

})(jQuery);