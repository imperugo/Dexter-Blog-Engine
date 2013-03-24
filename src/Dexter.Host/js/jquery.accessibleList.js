/**
 * The accessibleList plugin provides two transformations to improve lists display:
 * - truncate list to a given length, and add a 'more' button to reveal the whole list
 * - paginate long lists so they are easier to read
 * Each effect can be used without the other, or simultaneously
 */

(function($)
{
	/**
	 * Add controls to a list
	 */
	$.fn.accessibleList = function(options)
	{
		var settings = $.extend({}, $.fn.accessibleList.defaults, options);
		var inited = false;
		
		this.each(function(i)
		{
			var list = $(this);
			var lines = list.children();
			var listNode = list;
			var currentPage;
			var pagination = false;
			
			// Detect lag in rendering (IE...)
			if (list.height() == 0)
			{
				setTimeout(function() { list.accessibleList(options); }, 20);
				return;
			}
			
			// Setup
			list.css('overflow', 'hidden').addClass('relative');
			var listHeight = list.height();
			
			if (settings.pageSize && lines.length > settings.pageSize)
			{
				var nbPages = Math.max(1, Math.ceil(lines.length/settings.pageSize));
				currentPage = Math.max(1, Math.min(nbPages, settings.startPage));
				
				// Setup pages
				var width = list.width();
				list.width(width);
				var linePage = 0;
				var lineRow = 0;
				var height = 0;
				listHeight = 0;
				lines.addClass('absolute').css({
					width: (width-parseInt(lines.css('padding-left'))-parseInt(lines.css('padding-right')))+'px',
					overflow: 'hidden',
					textOverflow: 'ellipsis'
				}).each(function(i)
				{
					var line = $(this);
					line.css('top', height+'px').css('left', (linePage*100)+'%');
					height += line.outerHeight();
					
					++lineRow;
					if (lineRow == settings.pageSize)
					{
						// Detect listHeight
						if (height > listHeight)
						{
							listHeight = height;	
						}
						
						++linePage;
						lineRow = 0;
						height = 0;
					}
					else
					{
						height += parseInt(line.css('margin-bottom'));
					}
					
				}).attr('scrollLeft', (currentPage-1)*width);
				list.height(listHeight);
				
				// Create pagination
				pagination = listNode.after('<ul class="small-pagination"></ul>').next();
				for (number = 0; number < nbPages; ++number)
				{
					pagination.append(' <li><a href="#" title="Page '+(number+1)+'">'+(number+1)+'</a></li>');
				}
				var links = pagination.find('a');
				links.click(function(event)
				{
					// Stop link action
					event.preventDefault();
					var element = $(this);
					
					// Page number
					currentPage = parseInt(element.text());
					if (isNaN(currentPage))
					{
						currentPage = 1;
					}
					
					// Show page
					var scrollVal = (currentPage-1)*width;
					if (inited && settings.animate)
					{
						list.animate({scrollLeft: scrollVal});
					}
					else
					{
						list.attr('scrollLeft', scrollVal);
					}
					
					// Style
					element.parent().addClass('current').siblings().removeClass('current');
					if (currentPage == 1)
					{
						pagination.find('li.prev').css('visibility', 'hidden');
					}
					else
					{
						pagination.find('li.prev').css('visibility', 'visible');
					}
					if (currentPage == nbPages)
					{
						pagination.find('li.next').css('visibility', 'hidden');
					}
					else
					{
						pagination.find('li.next').css('visibility', 'visible');
					}
					
					// Callback
					if (settings.after)
					{
						settings.after.call(list.get(0));
					}
				});
				
				// Prev / next buttons
				pagination.prepend('<li class="prev"><a href="#">Prev</a></li>').find('li.prev a').click(function()
				{
					pagination.find('li.current').prev().not('.prev').children('a').trigger('click');
				});
				pagination.append(' <li class="next"><a href="#">Next</a></li>').find('li.next a').click(function()
				{
					pagination.find('li.current').next().not('.next').children('a').trigger('click');
				});
				
				// First update
				links.eq(currentPage-1).trigger('click');
				
				// Prepare for next condition
				listNode = pagination;
			}
			
			if (settings.moreAfter && lines.length > settings.moreAfter)
			{
				var expanded = true;
				
				var more = $('<a href="#" class="search-less">'+settings.lessText+'</a>').insertAfter(listNode).click(function(event)
				{
					// Stop link action
					event.preventDefault();
					
					// Detect mode
					if (!expanded)
					{
						if (inited && settings.animate)
						{
							list.animate({'height':listHeight});
						}
						else
						{
							list.css({'height':listHeight});
						}
						
						// Pagination
						if (pagination)
						{
							if (inited && settings.animate)
							{
								pagination.expand();
							}
							else
							{
								pagination.show();
							}
						}
						
						// More button
						more.removeClass('search-more').addClass('search-less').text(settings.lessText);
						expanded = true;
					}
					else
					{
						// Gather visible elements
						if (settings.pageSize && lines.length > settings.pageSize)
						{
							var visibleLines = lines;
							var rangeStart = (currentPage-1)*settings.pageSize;
							if (rangeStart > 0)
							{
								visibleLines = visibleLines.filter(':gt('+(rangeStart-1)+')');
							}
							visibleLines = visibleLines.filter(':lt('+settings.moreAfter+')');
						}
						else
						{
							var visibleLines = lines.filter(':lt('+settings.moreAfter+')');
						}
						
						// Calculate visible height
						var visibleHeight = 0;
						var visibleCount = visibleLines.length;
						visibleLines.each(function(i)
						{
							visibleHeight += $(this).outerHeight();
							if (i < visibleCount-1)
							{
								visibleHeight += parseInt($(this).css('margin-bottom'));
							}
						});
						
						if (inited && settings.animate)
						{
							list.animate({'height': visibleHeight});
						}
						else
						{
							list.css({'height': visibleHeight});
						}
						
						// Pagination
						if (pagination)
						{
							if (inited && settings.animate)
							{
								pagination.fold();
							}
							else
							{
								pagination.hide();
							}
						}
						
						// More button
						more.removeClass('search-less').addClass('search-more').text(settings.moreText);
						expanded = false;
					}
					
					// Callback
					if (settings.after)
					{
						settings.after.call(list.get(0));
					}
				});
				
				if (!settings.openedOnStart)
				{
					more.trigger('click');
				}
			}
		});
		
		// List ready
		inited = true;
		
		return this;
	};
	
	$.fn.accessibleList.defaults = {
		/**
		 * Max number of visible lines in each list above 'more' button (0 for no masking)
		 * @var int
		 */
		moreAfter: 2,
		
		/**
		 * Number of visible matches per page (0 for no pagination)
		 * @var int
		 */
		pageSize: 7,
		
		/**
		 * Number of page displayed on startup
		 * @var int
		 */
		startPage: 1,
		
		/**
		 * Tell wether the list should be expanded on startup (ignored if moreAfter is on 0)
		 * @var boolean
		 */
		openedOnStart: false,
		
		/**
		 * Enable animation on list expand/page change
		 * @var boolean
		 */
		animate: true,
		
		/**
		 * Text for the 'more' button
		 * @var string
		 */
		moreText: 'More',
		
		/**
		 * Text for the 'less' button
		 * @var string
		 */
		lessText: 'Less',
		
		/**
		 * Callback function after collapsing/expading/changing page. 'this' is the target list
		 * @var function|boolean
		 */
		after: false
	};

})(jQuery);