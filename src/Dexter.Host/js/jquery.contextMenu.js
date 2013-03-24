/**
 * Enables the context menu for elements bound to the 'contextMenu' event
 */

(function($)
{
	/*
	 * Enable context menu
	 */
	document.oncontextmenu = function(event)
	{
		var e = window.event || event;
		var target = $(e.target || e.srcElement);
		var list = [];
		target.trigger('contextMenu', [list]);
		
		// If some menu elements added
		if (list.length > 0)
		{
			// Mouse position
			var posx = 0;
			var posy = 0;
			if (e.pageX || e.pageY)
			{
				posx = e.pageX;
				posy = e.pageY;
			}
			else if (e.clientX || e.clientY)
			{
				posx = e.clientX+document.body.scrollLeft+document.documentElement.scrollLeft;
				posy = e.clientY+document.body.scrollTop+document.documentElement.scrollTop;
			}
			
			$('#contextMenu').html(buildMenuLevel(list)).css({
				top: posy+'px',
				left: posx+'px'
			}).show().openDropDownMenu();
			
			// Listener
			$(document).bind('click', closeContextMenu);
			
			// Prevent browser menu
			return false;
		}
	};
	
	/*
	 * Simple functions for closing menu
	 */
	function closeContextMenu()
	{
		$('#contextMenu').empty().hide();
		removeBinding();
	};
	function removeBinding()
	{
		$(document).unbind('click', closeContextMenu);
	};
	
	// Insert menu element
	$(document).ready(function()
	{
		$(document.body).append('<div id="contextMenu" class="menu"></div>');
	});
	
	/**
	 * Builds a level of the menu (recursive function)
	 * @param array list the menu elements list
	 */
	function buildMenuLevel(list)
	{
		var html = '<ul>';
		var defaults = {
			text: 'Link',
			alt: '',
			link: '',
			subs: [],
			icon: ''
		};
		
		for (var element in list)
		{
			// If separation
			if (typeof(list[element]) != 'object')
			{
				html += '<li class="sep"></li>';
			}
			else
			{
				var el = $.extend({}, defaults, list[element]);
				var alt = (el.alt.length > 0) ? ' title="'+el.alt+'"' : '';
				var icon = (el.icon.length > 0) ? ' class="icon_'+el.icon+'"' : '';
				if (el.link.length > 0)
				{
					var opener = 'a href="'+el.link+'"';
					var closer = 'a';
				}
				else
				{
					var opener = 'span';
					var closer = 'span';
				}
				
				// Opening
				html += '<li'+icon+'><'+opener+alt+'>'+el.text+'</'+closer+'>';
				
				// If sub menus
				if (typeof(el.subs) == 'object' && el.subs.length > 0)
				{
					html += buildMenuLevel(el.subs);
				}
				
				// Close
				html += '</li>';
			}
		}
		
		return html+'</ul>';
	};

})(jQuery);