/**
 * Modal window extension
 */

(function($)
{
	/**
	 * Opens a new modal window
	 * @param object options an object with any of the following options
	 * @return object the jQuery object of the new window
	 */
	$.modal = function(options)
	{
		var settings = $.extend({}, $.modal.defaults, options),
			root = getModalDiv(),
			
			// Vars for resizeFunc and moveFunc
			winX = 0,
			winY = 0,
			contentWidth = 0,
			contentHeight = 0,
			mouseX = 0,
			mouseY = 0,
			resized;
		
		// Get contents
		var content = '';
		var contentObj;
		if (settings.content)
		{
			if (typeof(settings.content) == 'string')
			{
				content = settings.content;
			}
			else
			{
				contentObj = settings.content.clone(true).show();
			}
		}
		else
		{
			// No content
			content = '';
		}
		
		// Title
		var titleClass = settings.title ? '' : ' no-title';
		var title = settings.title ? '<h1>'+settings.title+'</h1>' : '';
		
		// Content size
		var sizeParts = new Array();
		sizeParts.push('min-width:'+settings.minWidth+'px;');
		sizeParts.push('min-height:'+settings.minHeight+'px;');
		if (settings.width)
		{
			sizeParts.push('width:'+settings.width+'px; ');
		}
		if (settings.height)
		{
			sizeParts.push('height:'+settings.height+'px; ');
		}
		if (settings.maxWidth)
		{
			sizeParts.push('max-width:'+settings.maxWidth+'px; ');
		}
		if (settings.maxHeight)
		{
			sizeParts.push('max-height:'+settings.maxHeight+'px; ');
		}
		var contentStyle = (sizeParts.length > 0) ? ' style="'+sizeParts.join(' ')+'"' : '';
		
		// Borders
		var borderOpen = settings.border ? '"><div class="block-content'+titleClass : titleClass;
		var borderClose = settings.border ? '></div' : '';
		
		// Scrolling
		var scrollClass = settings.scrolling ? ' modal-scroll' : '';
		
		// Insert window
		var win = $('<div class="modal-window block-border'+borderOpen+'">'+title+'<div class="modal-content'+scrollClass+'"'+contentStyle+'>'+content+'</div></div'+borderClose+'>').appendTo(root);
		var contentDiv = win.find('.modal-content');
		if (contentObj)
		{
			contentObj.appendTo(contentDiv);
		}
		
		// If resizable
		if (settings.resizable && settings.border)
		{
			// Custom function (to use correct var scope)
			var resizeFunc = function(event)
			{
					// Mouse offset
				var offsetX = event.pageX-mouseX,
					offsetY = event.pageY-mouseY,
				
					// New size
					newWidth = Math.max(settings.minWidth, contentWidth+(resized.width*offsetX)),
					newHeight = Math.max(settings.minHeight, contentHeight+(resized.height*offsetY)),
					
					// Position correction
					correctX = 0,
					correctY = 0;
				
				// If max sizes are defined
				if (settings.maxWidth && newWidth > settings.maxWidth)
				{
					correctX = newWidth-settings.maxWidth;
					newWidth = settings.maxWidth;
				}
				if (settings.maxHeight && newHeight > settings.maxHeight)
				{
					correctY = newHeight-settings.maxHeight;
					newHeight = settings.maxHeight;
				}
				
				contentDiv.css({
					width: newWidth+'px',
					height: newHeight+'px'
				});
				win.css({
					left: (winX+(resized.left*(offsetX+correctX)))+'px',
					top: (winY+(resized.top*(offsetY+correctY)))+'px'
				});
			};
			
			// Create resize handlers
			$('<div class="modal-resize-tl"></div>').appendTo(win).data('modal-resize', {
				top: 1, left: 1,
				height: -1, width: -1
				
			}).add(
				$('<div class="modal-resize-t"></div>').appendTo(win).data('modal-resize', {
					top: 1, left: 0,
					height: -1, width: 0
				})
			).add(
				$('<div class="modal-resize-tr"></div>').appendTo(win).data('modal-resize', {
					top: 1, left: 0,
					height: -1, width: 1
				})
			).add(
				$('<div class="modal-resize-r"></div>').appendTo(win).data('modal-resize', {
					top: 0, left: 0,
					height: 0, width: 1
				})
			).add(
				$('<div class="modal-resize-br"></div>').appendTo(win).data('modal-resize', {
					top: 0, left: 0,
					height: 1, width: 1
				})
			).add(
				$('<div class="modal-resize-b"></div>').appendTo(win).data('modal-resize', {
					top: 0, left: 0,
					height: 1, width: 0
				})
			).add(
				$('<div class="modal-resize-bl"></div>').appendTo(win).data('modal-resize', {
					top: 0, left: 1,
					height: 1, width: -1
				})
			).add(
				$('<div class="modal-resize-l"></div>').appendTo(win).data('modal-resize', {
					top: 0, left: 1,
					height: 0, width: -1
				})
			).mousedown(function(event)
			{
				// Detect positions
				contentWidth = contentDiv.width();
				contentHeight = contentDiv.height();
				var position = win.position();
				winX = position.left;
				winY = position.top;
				mouseX = event.pageX;
				mouseY = event.pageY;
				resized = $(this).data('modal-resize');
				
				// Prevent text selection
				document.onselectstart = function () { return false; };
				
				$(document).bind('mousemove', resizeFunc);
				
			})
			root.mouseup(function()
			{
				$(document).unbind('mousemove', resizeFunc);
				
				// Restore text selection
				document.onselectstart = null;
			});
		}
		
		// Put in front
		win.mousedown(function()
		{
			$(this).putModalOnFront();
		});
		
		// If movable
		if (settings.draggable && title)
		{
			// Custom functions (to use correct var scope)
			var moveFunc = function(event)
			{
				// Window and document sizes
				var width = win.outerWidth(),
					height = win.outerHeight();
				
				// New position
				win.css({
					left: Math.max(0, Math.min(winX+(event.pageX-mouseX), $(root).width()-width))+'px',
					top: Math.max(0, Math.min(winY+(event.pageY-mouseY), $(root).height()-height))+'px'
				});
			};
			
			// Listeners
			win.find('h1:first').mousedown(function(event)
			{
				// Detect positions
				var position = win.position();
				winX = position.left;
				winY = position.top;
				mouseX = event.pageX;
				mouseY = event.pageY;
				
				// Prevent text selection
				document.onselectstart = function () { return false; };
				
				$(document).bind('mousemove', moveFunc);
				
			})
			root.mouseup(function()
			{
				$(document).unbind('mousemove', moveFunc);
				
				// Restore text selection
				document.onselectstart = null;
			});

		}
		
		// Close button
		if (settings.closeButton)
		{
			$('<ul class="action-tabs right"><li><a href="#" title="Close window"><img src="images/icons/fugue/cross-circle.png" width="16" height="16"></a></li></ul>')
				.prependTo(win)
				.find('a').click(function(event)
				{
					event.preventDefault();
					$(this).closest('.modal-window').closeModal();
				});
		}
		
		// Bottom buttons
		var buttonsFooter = false;
		$.each(settings.buttons, function(key, value)
		{
			// Button zone
			if (!buttonsFooter)
			{
				buttonsFooter = $('<div class="block-footer align-'+settings.buttonsAlign+'"></div>').insertAfter(contentDiv);
			}
			else
			{
				// Spacing
				buttonsFooter.append('&nbsp;');
			}
			
			$('<button type="button">'+key+'</button>').appendTo(buttonsFooter).click(function(event)
			{																			   
				value.call(this, $(this).closest('.modal-window'), event);
			});
		});
		
		// Close function
		if (settings.onClose)
		{
			win.bind('closeModal', settings.onClose);
		}
		
		// Apply template setup
		win.applyTemplateSetup();
		
		// Effect
		if (!root.is(':visible'))
		{
			win.hide();
			root.fadeIn('normal', function()
			{
				win.show().centerModal();
			});
		}
		else
		{
			win.centerModal();
		}
		
		// Store as current
		$.modal.current = win;
		$.modal.all = root.children('.modal-window');
				
		// Callback
		if (settings.onOpen)
		{
			settings.onOpen.call(win.get(0));
		}
		
		// If content url
		if (settings.url)
		{
			win.loadModalContent(settings.url, settings);
		}
		
		return win;
	};
	
	/**
	 * Shortcut to the current window
	 * @var jQuery|boolean
	 */
	$.modal.current = false;
	
	/**
	 * jQuery selection of all opened modal windows
	 * @var jQuery
	 */
	$.modal.all = $();
	
	/**
	 * Wraps the selected elements content in a new modal window
	 * @param object options same as $.modal()
	 * @return jQuery the new windows
	 */
	$.fn.modal = function(options)
	{
		var modals = $();
		
		this.each(function()
		{
			modals.add($.modal($.extend({}, $.modal.defaults, {content: $(this).clone(true).show()})));
		});
		
		return modals;
	};
	
	/**
	 * Use this method to retrieve the content div in the modal window
	 */
	$.fn.getModalContentBlock = function()
	{
		return this.find('.modal-content');
	}
	
	/**
	 * Use this method to retrieve the modal window from any element within it
	 */
	$.fn.getModalWindow = function()
	{
		return this.closest('.modal-window');
	}
	
	/**
	 * Set window content
	 * @param string|jQuery content the content to put: HTML or a jQuery object
	 * @param boolean resize use true to resize window to fit content (height only)
	 */
	$.fn.setModalContent = function(content, resize)
	{
		this.each(function()
		{
			var contentBlock = $(this).getModalContentBlock();
			
			// Set content
			if (typeof(content) == 'string')
			{
				contentBlock.html(content);
			}
			else
			{
				content.clone(true).show().appendTo(contentBlock);
			}
			contentBlock.applyTemplateSetup();
			
			// Resizing
			if (resize)
			{
				contentBlock.setModalContentSize(true, false);
			}
		});
		
		return this;
	}
	
	/**
	 * Set window content-block size
	 * @param int|boolean width the width to set, true to keep current or false for fluid width
	 * @param int|boolean height the height to set, true to keep current or false for fluid height
	 */
	$.fn.setModalContentSize = function(width, height)
	{
		this.each(function()
		{
			var contentBlock = $(this).getModalContentBlock();
			
			// Resizing
			if (width !== true)
			{
				contentBlock.css('width', width ? width+'px' : '');
			}
			if (height !== true)
			{
				contentBlock.css('height', height ? height+'px' : '');
			}
		});
		
		return this;
	}
	
	/**
	 * Load AJAX content
	 * @param string url the content url
	 * @param object options an object with any of the following options:
	 * 		- string loadingMessage any message to display while loading (may contain HTML), or leave empty to keep current content
	 * 		- string|object data a map or string that is sent to the server with the request (same as jQuery.load())
	 * 		- function complete a callback function that is executed when the request completes. (same as jQuery.load())
	 * 		- boolean resize use true to resize window on loading message and when content is loaded. To define separately, use options below:
	 * 		- boolean resizeOnMessage use true to resize window on loading message
	 * 		- boolean resizeOnLoad use true to resize window when content is loaded
	 */
	$.fn.loadModalContent = function(url, options)
	{
		var settings = $.extend({
			loadingMessage: '',
			data: {},
			complete: function(responseText, textStatus, XMLHttpRequest) {},
			resize: true,
			resizeOnMessage: false,
			resizeOnLoad: false
		}, options)
		
		this.each(function()
		{
			var win = $(this),
				contentBlock = win.getModalContentBlock();
			
			// If loading message
			if (settings.loadingMessage)
			{
				win.setModalContent('<div class="modal-loading">'+settings.loadingMessage+'</div>', (settings.resize || settings.resizeOnMessage));
			}
			
			contentBlock.load(url, settings.data, function(responseText, textStatus, XMLHttpRequest)
			{
				// Template functions
				contentBlock.applyTemplateSetup();
				
				if (settings.resize || settings.resizeOnLoad)
				{
					contentBlock.setModalContentSize(true, false);
				}
				
				// Callback
				settings.complete.call(this, responseText, textStatus, XMLHttpRequest);
			});
		});
		
		return this;
	}
	
	/**
	 * Set modal title
	 * @param string newTitle the new title (may contain HTML), or an empty string to remove the title
	 */
	$.fn.setModalTitle = function(newTitle)
	{
		this.each(function()
		{
			var win = $(this),
				title = $(this).find('h1'),
				contentBlock = win.hasClass('block-content') ? win : win.children('.block-content:first');
			
			if (newTitle.length > 0)
			{
				if (title.length == 0)
				{
					contentBlock.removeClass('no-title');
					title = $('<h1>'+newTitle+'</h1>').prependTo(contentBlock);
				}
				
				title.html(newTitle);
			}
			else if (title.length > 0)
			{
				title.remove();
				contentBlock.addClass('no-title');
			}
		});
		
		return this;
	}
	
	/**
	 * Center the window
	 * @param boolean animate true to animate the window movement
	 */
	$.fn.centerModal = function(animate)
	{
		var root = getModalDiv(),
			rootW = root.width()/2,
			rootH = root.height()/2;
		
		this.each(function()
		{
			var win = $(this),
				winW = Math.round(win.outerWidth()/2),
				winH = Math.round(win.outerHeight()/2);
			
			win[animate ? 'animate' : 'css']({
				left: (rootW-winW)+'px',
				top: (rootH-winH)+'px'
			});
		});
		
		return this;
	};
	
	/**
	 * Put modal on front
	 */
	$.fn.putModalOnFront = function()
	{
		if ($.modal.all.length > 1)
		{
			var root = getModalDiv();
			this.each(function()
			{
				if ($(this).next('.modal-window').length > 0)
				{
					$(this).detach().appendTo(root);
				}
			});
		}
		
		return this;
	};
	
	/**
	 * Closes the window
	 */
	$.fn.closeModal = function()
	{
		this.each(function()
		{
			var event = $.Event('closeModal'),
				win = $(this);
			
			// Events on close
			win.trigger(event);
			if (!event.isDefaultPrevented())
			{
				win.remove();
				
				// Modal root element
				var root = getModalDiv();
				$.modal.all = root.children('.modal-window');
				if ($.modal.all.length == 0)
				{
					$.modal.current = false;
					root.fadeOut('normal');
				}
				else
				{
					// Refresh current
					$.modal.current = $.modal.all.last();
				}
			}
		});
		
		return this;
	};
	
	/**
	 * New modal window options
	 */
	$.modal.defaults = {
		/**
		 * Content of the window: HTML or jQuery object
		 * @var string|jQuery
		 */
		content: false,
		
		/**
		 * Url for loading content
		 * @var string|boolean
		 */
		url: false,
		
		/**
		 * Title of the window, or false for none
		 * @var string|boolean
		 */
		title: false,
		
		/**
		 * Add glass-like border to the window (required to enable resizing)
		 * @var boolean
		 */
		border: true,
		
		/**
		 * Enable window moving (only work if title is defined)
		 * @var boolean
		 */
		draggable: true,
		
		/**
		 * Enable window resizing (only work if border is true)
		 * @var boolean
		 */
		resizable: true,
		
		/**
		 * If  true, enable content vertical scrollbar if content is higher than 'height' (or 'maxHeight' if 'height' is undefined)
		 * @var boolean
		 */
		scrolling: true,
		
		/**
		 * Wether or not to display the close window button
		 * @var boolean
		 */
		closeButton: true,
		
		/**
		 * Map of bottom buttons, with text as key and function on click as value
		 * Ex: {'Close' : function(win) { win.closeModal(); } }
		 * @var object
		 */
		buttons: {},
		
		/**
		 * Alignement of buttons ('left', 'center' or 'right')
		 * @var string
		 */
		buttonsAlign: 'right',
		
		/**
		 * Function called when opening window
		 * @var function
		 */
		onOpen: false,
		
		/**
		 * Function called when closing window. It may return false or call event.preventDefault() to prevent closing
		 * @var function
		 */
		onClose: false,
		
		/**
		 * Minimum content height
		 * @var int
		 */
		minHeight: 40,
		
		/**
		 * Minimum content width
		 * @var int
		 */
		minWidth: 200,
		
		/**
		 * Maximum content width, or false for no limit
		 * @var int|boolean
		 */
		maxHeight: false,
		
		/**
		 * Maximum content height, or false for no limit
		 * @var int|boolean
		 */
		maxWidth: false,
		
		/**
		 * Initial content height, or false for fluid size
		 * @var int|boolean
		 */
		height: false,
		
		/**
		 * Initial content width, or false for fluid size
		 * @var int|boolean
		 */
		width: 450,
		
		/**
		 * If AJAX load only - loading message, or false for none (can be HTML)
		 * @var string|boolean
		 */
		loadingMessage: 'Loading...',
		
		/**
		 * If AJAX load only - data a map or string that is sent to the server with the request (same as jQuery.load())
		 * @var string|object
		 */
		data: {},
		
		/**
		 * If AJAX load only - a callback function that is executed when the request completes. (same as jQuery.load())
		 * @var function
		 */
		complete: function(responseText, textStatus, XMLHttpRequest) {},
		
		/**
		 * If AJAX load only - true to resize window on loading message and when content is loaded. To define separately, use options below.
		 * @var boolean
		 */
		resize: true,
		
		/**
		 * If AJAX load only - use true to resize window on loading message
		 * @var boolean
		 */
		resizeOnMessage: false,
		
		/**
		 * If AJAX load only - use true to resize window when content is loaded
		 * @var boolean
		 */
		resizeOnLoad: false
	};
	
	/**
	 * Return the modal windows root div
	 */
	function getModalDiv()
	{
		var modal = $('#modal');
		if (modal.length == 0)
		{
			$(document.body).append('<div id="modal"></div>');
			modal = $('#modal').hide();
		}
		
		return modal;
	};

})(jQuery);