/**
 * Template JS for standard pages
 */

(function($)
{
	// Standard template setup
	$.fn.addTemplateSetup(function()
	{
		// Mini menu
		this.find('.mini-menu').css({opacity:0}).parent().hover(function()
		{
			$(this).children('.mini-menu').stop(true).animate({opacity:1});
			
		}, function()
		{
			$(this).children('.mini-menu').css('display', 'block').stop(true).animate({opacity:0}, {'complete': function() { $(this).css('display', ''); }});
			
		});
		
		// CSS Menu improvement
		this.find('.menu, .menu li:has(ul)').hover(function()
		{
			$(this).openDropDownMenu();
			
		}, function()
		{
			// Remove in case of future window resizing
			$(this).children('ul').removeClass('reverted');
		});
		
		// Scroll top button
		$('a[href="#top"]').click(function(event)
		{
			event.preventDefault();
			$('html, body').animate({scrollTop:0});
		});
	});
	
	// Close buttons
	$('.close-bt').live('click', function()
	{
		$(this).parent().fadeAndRemove();
	});
	
	// Document initial setup
	$(document).ready(function()
	{
		// Notifications blocks
		var notifications = $('<ul id="notifications"></ul>').appendTo(document.body);
		var notificationsTop = parseInt(notifications.css('top'));
		
		// If it is a standard page
		if (!$(document.body).hasClass('special-page'))
		{
			// Main nav - click style
			$('nav > ul > li').click(function(event) {
				// If not already active and has sub-menu
				if (!$(this).hasClass('current') && $(this).find('ul li').length > 0)
				{
					$(this).addClass('current').siblings().removeClass('current');
					$('nav > ul > li').refreshTip();
					event.preventDefault();
				}
			}).tip({
				stickIfCurrent: true,
				offset: -3
			});
			
			// Main nav - hover style
			/*$('nav > ul > li').hover(function() {
				$(this).addClass('current').siblings().removeClass('current');
				$('nav > ul > li').refreshTip();
			}, function() {}).tip({
				stickIfCurrent: true,
				offset: -3
			});*/
			
			// Advanced search field
			if ($.fn.advancedSearchField)
			{
				$('#s').advancedSearchField();
			}
			
			// Status bar buttons : drop-downs fade In/Out
			function convertDropLists()
			{
				$(this).find('.result-block .small-files-list').accessibleList({moreAfter:false});
				
				// Run only once
				$(this).unbind('mouseenter', convertDropLists);
			}
			$('#status-infos li:has(.result-block)').hover(function()
			{
				$(this).find('.result-block').stop(true).css('display', 'none').fadeIn('normal', function()
				{
					$(this).css('opacity', '');	
				});
				
			}, function()
			{
				$(this).find('.result-block').stop(true).css('display', 'block').fadeOut('normal', function()
				{
					$(this).css('opacity', '');	
				});
				
			}).bind('mouseenter', convertDropLists);
			
			// Fixed control bar
			var controlBar = $('#control-bar');
			if (controlBar.length > 0)
			{
				var cbPlaceHolder = controlBar.after('<div id="cb-place-holder" style="height:'+controlBar.outerHeight()+'px"></div>').next();
				
				// Effect
				controlBar.hover(function()
				{
					if ($(this).hasClass('fixed'))
					{
						$(this).stop(true).fadeTo('fast', 1);
					}
					
				}, function()
				{
					if ($(this).hasClass('fixed'))
					{
						$(this).stop(true).fadeTo('slow', 0.5);
					}
				});
				
				// Listener
				$(window).scroll(function()
				{
					// Check top position
					var controlBarPos = controlBar.hasClass('fixed') ? cbPlaceHolder.offset().top : controlBar.offset().top;
					
					if ($(window).scrollTop() > controlBarPos)
					{
						if (!controlBar.hasClass('fixed'))
						{
							cbPlaceHolder.height(controlBar.outerHeight()).show();
							controlBar.addClass('fixed').stop(true).fadeTo('slow', 0.5);
							
							// Notifications
							$('#notifications').animate({'top': controlBar.outerHeight()+notificationsTop});
						}
					}
					else
					{
						if (controlBar.hasClass('fixed'))
						{
							cbPlaceHolder.hide();
							controlBar.removeClass('fixed').stop(true).fadeTo('fast', 1, function()
							{
								// Required for IE
								$(this).css('filter', '');
							});
							
							// Notifications
							$('#notifications').animate({'top': notificationsTop});
						}
					}
				}).trigger('scroll');
			}
		}
		
	});
	
	/**
	 * Internal function to open drop-down menus, required for context menu
	 */
	$.fn.openDropDownMenu = function()
	{
		var ul = this.children('ul');
		
		// Position check
		if (ul.offset().left+ul.outerWidth()-$(window).scrollLeft() > $(window).width())
		{
			ul.addClass('reverted');
		}
		
		// Effect - IE < 9 uses filter for opacity, cutting out sub-menus
		if (!$.browser.msie || $.browser.version > 8)
		{
			ul.stop(true).css({opacity:0}).animate({opacity:1});
		}
	};
	
})(jQuery);


/**
 * Display a notification. If the page is not yet ready, delay the notification until it is ready.
 * @var string message a text or html message to display
 * @var object options an object with any options for the message - optional
 * 		- closeButton: true to add a close button to the message (default: true)
 * 		- autoClose: true to close message after (closeDelay) ms (default: true)
 * 		- closeDelay: delay before message close (default: 8000)
 */
var notify = function(message, options)
{
	var block = jQuery('#notifications');
	
	// If ready
	if (block.length > 0)
	{
		var settings = jQuery.extend({}, notify.defaults, options);
		
		// Append message
		var closeButton = settings.closeButton ? '<span class="close-bt"></span>' : '';
		var element = jQuery('#notifications').append('<li>'+message+closeButton+'</li>').children(':last-child');
		
		// Effect
		element.expand();
		
		// If closing
		if (settings.autoClose)
		{
			// Timer
			var timeoutId = setTimeout(function() { element.fadeAndRemove(); }, settings.closeDelay);
			
			// Prevent closing when hover
			element.hover(function()
			{
				clearTimeout(timeoutId);
				
			}, function()
			{
				timeoutId = setTimeout(function() { element.fadeAndRemove(); }, settings.closeDelay);
			});
		}
	}
	else
	{
		// Not ready, delay action
		setTimeout(function() { notify(message, options); }, 40);
	}
};

// Defaults values for the notify method
notify.defaults = {
	closeButton: true,			// Add a close button to the message
	autoClose: true,			// Message will close after (closeDelay) ms
	closeDelay: 8000			// Delay before message closes
};