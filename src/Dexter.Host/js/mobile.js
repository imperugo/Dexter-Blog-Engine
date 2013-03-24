/**
 * Template JS for mobile pages
 */

(function($)
{
	$(document).ready(function()
	{
		// Menu
		$('#menu > a').click(function(event)
		{
			event.preventDefault();
			$(this).parent().toggleClass('active');
		});
		
		// Menus with sub-menus
		$('#menu ul li:has(ul)').addClass('with-subs').children('a').click(function(event)
		{
			// Stop link
			event.preventDefault();
			
			// Show sub-menu
			var li = $(this).parent();
			li.addClass('active').siblings().removeClass('active');
			
			// Scroll
			var menu = $('#menu > ul');
			menu.stop(true).animate({scrollLeft:li.children('ul:first').offset().left+menu.scrollLeft()});
		})
		.siblings('ul').prepend('<li class="back"><a href="#">Back</a></li>')
		.find('li.back > a').click(function(event)
		{
			// Stop link
			event.preventDefault();
			
			// Scroll
			var li = $(this).parent().parent().parent();
			var menu = $('#menu > ul');
			var scrollVal = (li.parent().parent().attr('id') == 'menu') ? li.parent().offset().left : li.parent().offset().left+menu.scrollLeft();
			menu.stop(true).animate({scrollLeft:scrollVal}, {
				complete: function()
				{
					// Close sub-menu
					li.removeClass('active');
				}
			});
		});
	});

})(jQuery);