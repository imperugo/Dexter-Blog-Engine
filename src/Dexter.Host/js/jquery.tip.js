/**
 * Add tip to any element
 */

(function($)
{
	/**
	 * Adds tip on selected elements
	 * @param object options an object with any of the following options
	 * 		- content: Content of the tip (may be HTML) or false for auto detect (default: false)
	 * 		- onHover: Show tip only on hover (default: true)
	 * 		- reverseHover: Hide tip on hover in, show on hover out (default: false)
	 * 		- stickIfCurrent: Enable permanent tip on elements with the class 'current' (default: false)
	 * 		- currentClass: Name of the 'current' class (default: 'current')
	 * 		- offset: Offset of the tip from the element (default: 4)
	 * 		- position: Position of the tip relative to the element (default: 'top')
	 * 		- animationOffset: Offset for the animation (pixels) (default: 4)
	 * 		- delay: Delay before tip shows up on hover (milliseconds) (default: 0)
	 */
	$.fn.tip = function(options)
	{
		var settings = $.extend({}, $.fn.tip.defaults, options);
		
		// Mode
		if (settings.onHover)
		{
			// Detect current elements
			if (settings.stickIfCurrent)
			{
				this.filter('.'+settings.currentClass).each(function(i)
				{
					$(this).createTip(settings);
				});
			}
			
			// Effect
			if (settings.reverseHover)
			{
				$(this).createTip(settings);
				
				this.hover(function()
				{
					if (!settings.stickIfCurrent || !$(this).hasClass(settings.currentClass))
					{
						$(this).hideTip();
					}
					
				}, function()
				{
					$(this).showTip(settings);
				});
			}
			else
			{
				this.hover(function()
				{
					$(this).showTip(settings);
					
				}, function()
				{
					if (!settings.stickIfCurrent || !$(this).hasClass(settings.currentClass))
					{
						$(this).hideTip();
					}
				});
			}
		}
		else
		{
			this.createTip(settings);
		}
		
		return this;
	};
	
	$.fn.tip.defaults = {
		/**
		 * Content of the tip (may be HTML) or false for auto detect
		 * @var string|boolean
		 */
		content: false,
		
		/**
		 * Show tip only on hover
		 * @var boolean
		 */
		onHover: true,
		
		/**
		 * Hide tip on hover in, show on hover out
		 * @var boolean
		 */
		reverseHover: false,
		
		/**
		 * Enable permanent tip on elements with the class 'current'
		 * @var boolean
		 */
		stickIfCurrent: false,
		
		/**
		 * Name of the 'current' class
		 * @var boolean
		 */
		currentClass: 'current',
		
		/**
		 * Offset of the tip from the element (pixels)
		 * @var int
		 */
		offset: 4,
		
		/**
		 * Position of the tip relative to the element
		 * @var string
		 */
		position: 'top',
		
		/**
		 * Offset for the animation (pixels)
		 * @var int
		 */
		animationOffset: 4,
		
		/**
		 * Delay before tip shows up on hover (milliseconds)
		 * @var int
		 */
		delay: 0
	};
	
	/**
	 * Add tip (if not exist) and show it with effect
	 * @param object options same as the tip() method
	 */
	$.fn.showTip = function(options)
	{
		var settings = $.extend({}, $.fn.tip.defaults, options);
		
		this.each(function(i)
		{
			var element = $(this);
			var oldIE = ($.browser.msie && $.browser.version < 9);
			
			// If tip does not already exist (if current), create it
			var tip = element.data('tip');
			if (!tip)
			{
				element.createTip(settings, oldIE ? false : true);
				tip = element.data('tip');
			}
			else if (settings.content !== element.data('settings').content)
			{
				element.updateTipContent(options.content);
			}
			
			// Animation
			if (!oldIE)	// IE6-8 filters do not allow correct animation (the arrow is truncated)
			{
				var position = getTipPosition(element, tip, settings, false);
				tip.stop(true).delay(settings.delay).animate({
					opacity: 1,
					top: position.top,
					left: position.left
				}, 'fast');
			}
		});
		
		return this;
	};
	
	/**
	 * Hide then remove tip
	 */
	$.fn.hideTip = function()
	{
		this.each(function(i)
		{
			var element = $(this);
			var tip = element.data('tip');
			if (tip)
			{
				var settings = element.data('settings');
				
				if ($.browser.msie && $.browser.version < 9)
				{
					// IE8 and lower filters do not allow correct animation (the arrow is truncated)
					tip.children('.arrow').remove();
					this.title = tip.html();
					element.data('tip', false);
					tip.remove();
				}
				else
				{
					// Hiding is not relative to the parent element, to prevent weird behaviour if parent is moved or removed
					var position = getFinalPosition(tip, settings);
					var offset = tip.offset();
					
					switch (position)
					{
						case 'right':
							offset.left += settings.animationOffset+settings.offset;
							break;
						
						case 'bottom':
							offset.top += settings.animationOffset+settings.offset;
							break;
						
						case 'left':
							offset.left -= settings.animationOffset+settings.offset;
							break;
						
						default:
							offset.top -= settings.animationOffset+settings.offset;
							break;
					}
					
					tip.animate({
						opacity: 0,
						top: offset.top,
						left: offset.left
					}, {
						complete: function()
						{
							// Restore node
							var tip = $(this);
							var node = tip.data('node');
							if (node)
							{
								tip.children('.arrow').remove();
								node.attr('title', tip.html());
								node.data('tip', false);
							}
							
							// Remove tip
							tip.remove();
						}
					});
				}
			}
		});
		
		return this;
	};
	
	/**
	 * Create tip bubble
	 * @param object settings the options object given to tip()
	 * @param boolean hide indicate whether to hide the tip after creating it or not (default : false)
	 */
	$.fn.createTip = function(settings, hide)
	{
		this.each(function(i)
		{
			var element = $(this);
			var tips = getTipDiv();
			
			// Insertion
			tips.append('<div></div>');
			var tip = tips.children(':last-child');
			
			// Position class
			if (settings.position == 'right' || element.hasClass('tip-right') || element.parent().hasClass('children-tip-right'))
			{
				tip.addClass('tip-right');
			}
			else if (settings.position == 'bottom' || element.hasClass('tip-bottom') || element.parent().hasClass('children-tip-bottom'))
			{
				tip.addClass('tip-bottom');
			}
			else if (settings.position == 'left' || element.hasClass('tip-left') || element.parent().hasClass('children-tip-left'))
			{
				tip.addClass('tip-left');
			}
			
			// Cross references
			tip.data('node', element);
			element.data('tip', tip);
			element.data('settings', settings);
			
			// Content
			element.updateTipContent(settings.content, hide);
			
			// Effect
			if (hide)
			{
				tip.css({opacity:0});	
			}
		});
		
		return this;
	};
	
	/**
	 * Update tip content
	 * @param mixed content any content (text or HTML) for the tip, of false for automatic detection
	 * @param boolean hide optional, compatibility with createTip()
	 */
	$.fn.updateTipContent = function(content, hide)
	{
		this.each(function(i)
		{
			var element = $(this);
			var tip = element.data('tip');
			var settings = element.data('settings');
			
			// If auto tip content
			if (!content)
			{
				if (this.title && this.title.length > 0)
				{
					var finalContent = this.title;
					this.title = '';
				}
				else
				{
					var subTitle = element.find('[title]:first');
					if (subTitle.length > 0)
					{
						var finalContent = subTitle.attr('title');
						subTitle.attr('title', '');
					}
					else
					{
						var finalContent = element.text();
					}
				}
			}
			else
			{
				var finalContent = content;
			}
			
			// If empty
			if (!finalContent || $.trim(finalContent).length == 0)
			{
				finalContent = '<em>No tip</em>';
			}
			
			// Insert
			tip.html(finalContent+'<span class="arrow"><span></span></span>');
			
			// Position
			tip.stop(true, true);
			var position = getTipPosition(element, tip, settings, hide);
			tip.offset(position);
		});
		
		return this;
	};
	
	/**
	 * Call this function to refresh tips when using the stickIfCurrent option 
	 * and the 'current' element has changed
	 */
	$.fn.refreshTip = function()
	{
		this.each(function(i)
		{
			var settings = $(this).data('settings');
			if (settings && settings.stickIfCurrent)
			{
				var element = $(this);
				if (element.hasClass(settings.currentClass))
				{
					element.showTip(settings);
				}
				else
				{
					element.hideTip(settings);
				}
			}
		});
		
		return this;
	};
	
	/**
	 * Detect final position for the tip
	 * @param jQuery tip the tip element
	 * @param Object settings the tip options
	 * @return string the final position
	 */
	function getFinalPosition(tip, settings)
	{
		var position = settings.position;
		if (tip.hasClass('tip-right'))
		{
			position = 'right';
		}
		else if (tip.hasClass('tip-bottom'))
		{
			position = 'bottom';
		}
		else if (tip.hasClass('tip-left'))
		{
			position = 'left';
		}
		
		return position;
	}
	
	/**
	 * Get tip position, relative to the element
	 * @param jQuery element the element on which the the tip show
	 * @param jQuery tip the tip element
	 * @param Object settings the tip options
	 * @param boolean animStart tells wether the tip should be positioned at the start of the animation or not
	 * @return Object an object with two values : 'top' and 'left'
	 */
	function getTipPosition(element, tip, settings, animStart)
	{
		var offset = element.offset();
		var position = getFinalPosition(tip, settings);
		
		switch (position)
		{
			case 'right':
				return {
					top: Math.round(offset.top+(element.outerHeight()/2)-(tip.outerHeight()/2)),
					left: Math.round(offset.left+element.outerWidth()+(animStart ? settings.animationOffset+settings.offset : settings.offset))
				};
				break;
			
			case 'bottom':
				return {
					top: Math.round(offset.top+element.outerHeight()+(animStart ? settings.animationOffset+settings.offset : settings.offset)),
					left: Math.round(offset.left+(element.outerWidth()/2)-(tip.outerWidth()/2))
				};
				break;
			
			case 'left':
				return {
					top: Math.round(offset.top+(element.outerHeight()/2)-(tip.outerHeight()/2)),
					left: Math.round(offset.left-tip.outerWidth()-(animStart ? settings.animationOffset+settings.offset : settings.offset))
				};
				break;
			
			default:
				return {
					top: Math.round(offset.top-tip.outerHeight()-(animStart ? settings.animationOffset+settings.offset : settings.offset)),
					left: Math.round(offset.left+(element.outerWidth()/2)-(tip.outerWidth()/2))
				};
				break;
		}
	}
	
	// If template common functions loaded
	if ($.fn.addTemplateSetup)
	{
		$.fn.addTemplateSetup(function()
		{
			this.find('.with-tip, .with-children-tip > *').tip();
		});
	}
	else
	{
		// Default behaviour
		$(document).ready(function()
		{
			$('.with-tip, .with-children-tip > *').tip();
		});
	}
	
	/**
	 * Return the tips div, or create it if it does not exist
	 */
	function getTipDiv()
	{
		var tips = $('#tips');
		if (tips.length == 0)
		{
			$(document.body).append('<div id="tips"></div>');
			tips = $('#tips');
		}
		
		return tips;
	}
	
	// Handle viewport resizing
	$(window).resize(function()
	{
		getTipDiv().children().each(function(i)
		{
			// Init
			var tip = $(this);
			var element = tip.data('node');
			var settings = element.data('settings');
			var isCurrent = settings.stickIfCurrent && element.hasClass(settings.currentClass);
			
			// Position
			var animate = (settings.onHover && !isCurrent);
			tip.stop(true, true);
			var position = getTipPosition(element, tip, settings, animate);
			tip.offset(position);
		});
	});

})(jQuery);