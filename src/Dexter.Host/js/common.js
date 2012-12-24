/**
 * Template JS for standard pages
 */

(function($) {
	/**
	 * List of functions required to enable template effects/hacks
	 * @var array
	 */
	var templateSetup = new Array();

	/**
	 * Add a new template function
	 * @param function func the function to be called on a jQuery object
	 * @param boolean prioritary set to true to call the function before all others (optional)
	 * @return void
	 */
	$.fn.addTemplateSetup = function(func, prioritary) {
		if (prioritary) {
			templateSetup.unshift(func);
		} else {
			templateSetup.push(func);
		}
	};

	/**
	 * Call every template function over a jQuery object (for instance : $('body').applyTemplateSetup())
	 * @return void
	 */
	$.fn.applyTemplateSetup = function() {
		var max = templateSetup.length;
		for (var i = 0; i < max; ++i) {
			templateSetup[i].apply(this);
		}

		return this;
	};

	// Common (mobile and standard) template setup
	$.fn.addTemplateSetup(function() {
		// Collapsible fieldsets
		this.find('fieldset legend > a, .fieldset .legend > a').click(function(event) {
			$(this).toggleFieldsetOpen();
			event.preventDefault();
		});
		this.find('fieldset.collapse, .fieldset.collapse').toggleFieldsetOpen().removeClass('collapse');

		// Equalize tab content-blocks heights
		this.find('.tabs.same-height, .side-tabs.same-height, .mini-tabs.same-height, .controls-tabs.same-height').equalizeTabContentHeight();

		// Update tabs
		this.find('.js-tabs').updateTabs();

		// Input switches
		this.find('input[type=radio].switch, input[type=checkbox].switch').hide().after('<span class="switch-replace"></span>').next().click(function() {
			$(this).prev().click();
		}).prev('.with-tip').next().addClass('with-tip').each(function() {
			$(this).attr('title', $(this).prev().attr('title'));
		});
		this.find('input[type=radio].mini-switch, input[type=checkbox].mini-switch').hide().after('<span class="mini-switch-replace"></span>').next().click(function() {
			$(this).prev().click();
		}).prev('.with-tip').next().addClass('with-tip').each(function() {
			$(this).attr('title', $(this).prev().attr('title'));
		});

		// Tabs links behaviour
		this.find('.js-tabs a[href^="#"]').click(function(event) {
			event.preventDefault();

			// If hashtag enabled
			if ($.fn.updateTabs.enabledHash) {
				// Retrieve hash parts
				var element = $(this);
				var hash = $.trim(window.location.hash || '');
				if (hash.length > 1) {
					// Remove hash from other tabs of the group
					var hashParts = hash.substring(1).split('&');
					var dummyIndex;
					while ((dummyIndex = $.inArray('', hashParts)) > -1) {
						hashParts.splice(dummyIndex, 1);
					}
					while ((dummyIndex = $.inArray('none', hashParts)) > -1) {
						hashParts.splice(dummyIndex, 1);
					}
					element.parent().parent().find('a[href^="#"]').each(function(i) {
						var index = $.inArray($(this).attr('href').substring(1), hashParts);
						if (index > -1) {
							hashParts.splice(index, 1);
						}
					});
				} else {
					var hashParts = [];
				}

				// Add current tab to hash (not if default)
				var defaultTab = getDefaultTabIndex(element.parent().parent());
				if (element.parent().index() != defaultTab) {
					hashParts.push(element.attr('href').substring(1));
				}

				// If only one tab, add a empty id to prevent document from jumping to selected content
				if (hashParts.length == 1) {
					hashParts.unshift('');
				}

				// Put hash, will trigger refresh
				window.location.hash = (hashParts.length > 0) ? '#' + hashParts.join('&') : '#none';
			} else {
				var li = $(this).closest('li');
				li.addClass('current').siblings().removeClass('current');
				li.parent().updateTabs();
			}
		});
	});

	// Document initial setup
	$(document).ready(function() {
		// Template setup
		$(document.body).applyTemplateSetup();

		// Listener
		$(window).bind('hashchange', function() {
			$(document.body).find('.js-tabs').updateTabs();
		});

	});

	/**
	 * Get tab group default tab
	 */

	function getDefaultTabIndex(tabGroup) {
		var defaultTab = tabGroup.data('defaultTab');
		if (defaultTab === null || defaultTab === '' || defaultTab === undefined) {
			var firstTab = tabGroup.children('.current:first');
			defaultTab = (firstTab.length > 0) ? firstTab.index() : 0;
			tabGroup.data('defaultTab', defaultTab);
		}

		return defaultTab;
	}

	;

	/**
	 * Update tabs
	 */
	$.fn.updateTabs = function() {
		// If hashtags enabled
		if ($.fn.updateTabs.enabledHash) {
			var hash = $.trim(window.location.hash || '');
			var hashParts = (hash.length > 1) ? hash.substring(1).split('&') : [];
		} else {
			var hash = '';
			var hashParts = [];
		}

		// Check all tabs
		var hasHash = (hashParts.length > 0);
		this.each(function(i) {
			// Check if already inited
			var tabGroup = $(this);
			var defaultTab = getDefaultTabIndex(tabGroup);

			// Look for current tab
			var current = false;
			if ($.fn.updateTabs.enabledHash) {
				if (hasHash) {
					var links = tabGroup.find('a[href^="#"]');
					links.each(function(i) {
						var linkHash = $(this).attr('href').substring(1);
						if (linkHash.length > 0) {
							var index = $.inArray(linkHash, hashParts);
							if (index > -1) {
								current = $(this).parent();
								return false;
							}
						}
					});
				}
			} else {
				current = tabGroup.children('.current:first');
			}

			// If none found : get the default tab
			if (!current) {
				current = tabGroup.children(':eq(' + defaultTab + ')');
			}

			if (current.length > 0) {
				// Display current tab content block
				hash = $.trim(current.children('a').attr('href').substring(1));
				if (hash.length > 0) {
					// Highlight current
					current.addClass('current');
					var tabContainer = $('#' + hash),
					    tabHidden = tabContainer.is(':hidden');

					// Show if hidden
					if (tabHidden) {
						tabContainer.show();
					}

					// Hide others
					current.siblings().removeClass('current').children('a').each(function(i) {
						var hash = $.trim($(this).attr('href').substring(1));
						if (hash.length > 0) {
							var tabContainer = $('#' + hash);

							// Hide if visible
							if (tabContainer.is(':visible')) {
								tabContainer.trigger('tabhide').hide();
							}
								// Or init if first round
							else if (!tabContainer.data('tabInited')) {
								tabContainer.trigger('tabhide');
								tabContainer.data('tabInited', true);
							}
						}
					});

					// Callback
					if (tabHidden) {
						tabContainer.trigger('tabshow');
					}
						// Or init if first round
					else if (!tabContainer.data('tabInited')) {
						tabContainer.trigger('tabshow');
						tabContainer.data('tabInited', true);
					}
				}
			}
		});

		return this;
	};

	/**
	 * Indicate whereas JS tabs hashtag is enabled
	 * @var boolean
	 */
	$.fn.updateTabs.enabledHash = true;

	/**
	 * Reset tab content blocks heights
	 */
	$.fn.resetTabContentHeight = function() {
		this.find('a[href^="#"]').each(function(i) {
			var hash = $.trim($(this).attr('href').substring(1));
			if (hash.length > 0) {
				$('#' + hash).css('height', '');
			}
		});

		return this;
	}; /**
	 * Equalize tab content blocks heights
	 */
	$.fn.equalizeTabContentHeight = function() {
		var i;
		var g;
		var maxHeight;
		var tabContainers;
		var block;
		var blockHeight;
		var marginAdjustTop;
		var marginAdjustBot;
		var first;
		var last;
		var firstMargin;
		var lastMargin;

		// Process in reverse order to equalize sub-tabs first
		for (i = this.length - 1; i >= 0; --i) {
			// Look for max height
			maxHeight = -1;
			tabContainers = [];
			this.eq(i).find('a[href^="#"]').each(function(i) {
				var hash = $.trim($(this).attr('href').substring(1));
				if (hash.length > 0) {
					block = $('#' + hash);
					if (block.length > 0) {
						blockHeight = block.outerHeight() + parseInt(block.css('margin-bottom'));

						// First element top-margin affects real height
						marginAdjustTop = 0;
						first = block.children(':first');
						if (first.length > 0) {
							firstMargin = parseInt(first.css('margin-top'));
							if (!isNaN(firstMargin) && firstMargin < 0) {
								marginAdjustTop = firstMargin;
							}
						}

						// Same for last element bottom-margin
						marginAdjustBot = 0;
						last = block.children(':last');
						if (last.length > 0) {
							lastMargin = parseInt(last.css('margin-bottom'));
							if (!isNaN(lastMargin) && lastMargin < 0) {
								marginAdjustBot = lastMargin;
							}
						}

						if (blockHeight + marginAdjustTop + marginAdjustBot > maxHeight) {
							maxHeight = blockHeight + marginAdjustTop + marginAdjustBot;
						}

						tabContainers.push([block, marginAdjustTop]);
					}
				}
			});

			// Setup height
			for (g = 0; g < tabContainers.length; ++g) {
				tabContainers[g][0].height(maxHeight - parseInt(tabContainers[g][0].css('padding-top')) - parseInt(tabContainers[g][0].css('padding-bottom')) - parseInt(tabContainers[g][0].css('margin-bottom')) - tabContainers[g][1]);

				// Only the first tab remains visible
				if (g > 0) {
					tabContainers[g][0].hide();
				}
			}
		}

		return this;
	};

	/**
	 * Display the selected tab (apply on tab content-blocks, not tabs, ie: $('#my-tab').showTab(); )
	 */
	$.fn.showTab = function() {
		this.each(function(i) {
			$('a[href="#' + this.id + '"]').trigger('click');
		});

		return this;
	};

	/**
	 * Add a listener fired when a tab is shown
	 * @param function callback any function to call when the listener is fired.
	 * @param boolean runOnce if true, the callback will be run one time only. Default: false - optional
	 */
	$.fn.onTabShow = function(callback, runOnce) {
		if (runOnce) {
			var runOnceFunc = function() {
				callback.apply(this, arguments);
				$(this).unbind('tabshow', runOnceFunc);
			};
			this.bind('tabshow', runOnceFunc);
		} else {
			this.bind('tabshow', callback);
		}

		return this;
	};

	/**
	 * Add a listener fired when a tab is hidden
	 * @param function callback any function to call when the listener is fired.
	 * @param boolean runOnce if true, the callback will be run one time only. Default: false - optional
	 */
	$.fn.onTabHide = function(callback, runOnce) {
		if (runOnce) {
			var runOnceFunc = function() {
				callback.apply(this, arguments);
				$(this).unbind('tabhide', runOnceFunc);
			};
			this.bind('tabhide', runOnceFunc);
		} else {
			this.bind('tabhide', callback);
		}

		return this;
	};

	/**
	 * Insert a message into a block
	 * @param string|array message a message, or an array of messages to inserted
	 * @param object options optional object with following values:
	 * 		- type: one of the available message classes : 'info' (default), 'warning', 'error', 'success', 'loading'
	 * 		- position: either 'top' (default) or 'bottom'
	 * 		- animate: true to show the message with an animation (default), else false
	 * 		- noMargin: true to apply the no-margin class to the message (default), else false
	 */
	$.fn.blockMessage = function(message, options) {
		var settings = $.extend({}, $.fn.blockMessage.defaults, options);

		this.each(function(i) {
			// Locate content block
			var block = $(this);
			if (!block.hasClass('block-content')) {
				block = block.find('.block-content:first');
				if (block.length == 0) {
					block = $(this).closest('.block-content');
					if (block.length == 0) {
						return;
					}
				}
			}

			// Compose message
			var messageClass = (settings.type == 'info') ? 'message' : 'message ' + settings.type;
			if (settings.noMargin) {
				messageClass += ' no-margin';
			}
			var finalMessage = (typeof message == 'object') ? '<ul class="' + messageClass + '"><li>' + message.join('</li><li>') + '</li></ul>' : '<p class="' + messageClass + '">' + message + '</p>';

			// Insert message
			if (settings.position == 'top') {
				var children = block.find('h1, .h1, .block-controls, .block-header, .wizard-steps');
				if (children.length > 0) {
					var lastHeader = children.last();
					var next = lastHeader.next('.message');
					while (next.length > 0) {
						lastHeader = next;
						next = lastHeader.next('.message');
					}
					var messageElement = lastHeader.after(finalMessage).next();
				} else {
					var messageElement = block.prepend(finalMessage).children(':first');
				}
			} else {
				var children = block.find('.block-footer:last-child');
				if (children.length > 0) {
					var messageElement = children.before(finalMessage).prev();
				} else {
					var messageElement = block.append(finalMessage).children(':last');
				}
			}

			if (settings.animate) {
				messageElement.expand();
			}
		});

		return this;
	};

	// Default config for the blockMessage function
	$.fn.blockMessage.defaults = {
		type: 'info',
		position: 'top',
		animate: true,
		noMargin: true
	};

	/**
	 * Remove all messages from the block
	 * @param object options optional object with following values:
	 * 		- only: string or array of strings of message classes which will be removed
	 * 		- except: string or array of strings of message classes which will not be removed (ignored if 'only' is provided)
	 * 		- animate: true to remove the message with an animation (default), else false
	 */
	$.fn.removeBlockMessages = function(options) {
		var settings = $.extend({}, $.fn.removeBlockMessages.defaults, options);

		this.each(function(i) {
			// Locate content block
			var block = $(this);
			if (!block.hasClass('block-content')) {
				block = block.find('.block-content:first');
				if (block.length == 0) {
					block = $(this).closest('.block-content');
					if (block.length == 0) {
						return;
					}
				}
			}

			var messages = block.find('.message');
			if (settings.only) {
				if (typeof settings.only == 'string') {
					settings.only = [settings.only];
				}
				messages.filter('.' + settings.only.join(', .'));
			} else if (settings.except) {
				if (typeof settings.except == 'string') {
					settings.except = [settings.except];
				}
				messages.not('.' + settings.except.join(', .'));
			}

			if (settings.animate) {
				messages.foldAndRemove();
			} else {
				messages.remove();
			}
		});

		return this;
	};

	// Default config for the blockMessage function
	$.fn.removeBlockMessages.defaults = {
		only: false,				// string or array of strings of message classes which will be removed
		except: false,				// except: string or array of strings of message classes which will not be removed (ignored if only is provided)
		animate: true				// animate: true to remove the message with an animation (default), else false
	};

	/**
	 * Fold an element
	 * @param string|int duration a string (fast, normal or slow) or a number of millisecond. Default: 'normal'. - optional
	 * @param function callback any function to call at the end of the effect. Default: none. - optional
	 */
	$.fn.fold = function(duration, callback) {
		this.each(function(i) {
			var element = $(this);
			var marginTop = parseInt(element.css('margin-top'));
			var marginBottom = parseInt(element.css('margin-bottom'));

			var anim = {
				'height': 0,
				'paddingTop': 0,
				'paddingBottom': 0
			};

			// IE8 and lower do not understand border-xx-width
			// http://forum.jquery.com/topic/ie-invalid-argument
			if (!$.browser.msie || $.browser.version > 8) {
				// Border width is not set to 0 because it does not allow fluid movement 
				anim.borderTopWidth = '1px';
				anim.borderBottomWidth = '1px';
			}

			// Detection of elements sticking to their predecessor
			var prev = element.prev();
			if (prev.length === 0 && parseInt(element.css('margin-bottom')) + marginTop !== 0) {
				anim.marginTop = Math.min(0, marginTop);
				anim.marginBottom = Math.min(0, marginBottom);
			}

			// Effect
			element.stop(true).css({
				'overflow': 'hidden'
			}).animate(anim, {
				'duration': duration,
				'complete': function() {
					// Reset properties
					$(this).css({
						'display': 'none',
						'overflow': '',
						'height': '',
						'paddingTop': '',
						'paddingBottom': '',
						'borderTopWidth': '',
						'borderBottomWidth': '',
						'marginTop': '',
						'marginBottom': ''
					});

					// Callback function
					if (callback) {
						callback.apply(this);
					}
				}
			});
		});

		return this;
	};

	/*
	 * Expand an element
	 * @param string|int duration a string (fast, normal or slow) or a number of millisecond. Default: 'normal'. - optional
	 * @param function callback any function to call at the end of the effect. Default: none. - optional
	 */
	$.fn.expand = function(duration, callback) {
		this.each(function(i) {
			// Init
			var element = $(this);
			element.css('display', 'block');

			// Reset and get values
			element.stop(true).css({
				'overflow': '',
				'height': '',
				'paddingTop': '',
				'paddingBottom': '',
				'borderTopWidth': '',
				'borderBottomWidth': '',
				'marginTop': '',
				'marginBottom': ''
			});
			var height = element.height();
			var paddingTop = parseInt(element.css('padding-top'));
			var paddingBottom = parseInt(element.css('padding-bottom'));
			var marginTop = parseInt(element.css('margin-top'));
			var marginBottom = parseInt(element.css('margin-bottom'));

			// Initial and target values
			var css = {
				'overflow': 'hidden',
				'height': 0,
				'paddingTop': 0,
				'paddingBottom': 0
			};
			var anim = {
				'height': height,
				'paddingTop': paddingTop,
				'paddingBottom': paddingBottom
			};

			// IE8 and lower do not understand border-xx-width
			// http://forum.jquery.com/topic/ie-invalid-argument
			if (!$.browser.msie || $.browser.version > 8) {
				var borderTopWidth = parseInt(element.css('border-top-width'));
				var borderBottomWidth = parseInt(element.css('border-bottom-width'));

				// Border width is not set to 0 because it does not allow fluid movement 
				css.borderTopWidth = '1px';
				css.borderBottomWidth = '1px';
				anim.borderTopWidth = borderTopWidth;
				anim.borderBottomWidth = borderBottomWidth;
			}

			// Detection of elements sticking to their predecessor
			var prev = element.prev();
			if (prev.length === 0 && parseInt(element.css('margin-bottom')) + marginTop !== 0) {
				css.marginTop = Math.min(0, marginTop);
				css.marginBottom = Math.min(0, marginBottom);
				anim.marginTop = marginTop;
				anim.marginBottom = marginBottom;
			}

			// Effect
			element.stop(true).css(css).animate(anim, {
				'duration': duration,
				'complete': function() {
					// Reset properties
					$(this).css({
						'display': '',
						'overflow': '',
						'height': '',
						'paddingTop': '',
						'paddingBottom': '',
						'borderTopWidth': '',
						'borderBottomWidth': '',
						'marginTop': '',
						'marginBottom': ''
					});

					// Callback function
					if (callback) {
						callback.apply(this);
					}

					// Required for IE7 - don't ask me why...
					if ($.browser.msie && $.browser.version < 8) {
						$(this).css('zoom', 1);
					}
				}
			});
		});

		return this;
	};

	/**
	 * Remove an element with folding effect
	 * @param string|int duration a string (fast, normal or slow) or a number of millisecond. Default: 'normal'. - optional
	 * @param function callback any function to call at the end of the effect. Default: none. - optional
	 */
	$.fn.foldAndRemove = function(duration, callback) {
		$(this).fold(duration, function() {
			// Callback function
			if (callback) {
				callback.apply(this);
			}

			$(this).remove();
		});

		return this;
	}; /**
	 * Remove an element with fading then folding effect
	 * @param string|int duration a string (fast, normal or slow) or a number of millisecond. Default: 'normal'. - optional
	 * @param function callback any function to call at the end of the effect. Default: none. - optional
	 */
	$.fn.fadeAndRemove = function(duration, callback) {
		this.animate({ 'opacity': 0 }, {
			'duration': duration,
			'complete': function() {
				// No folding required if the element has position: absolute (not in the elements flow)
				if ($(this).css('position') == 'absolute') {
					// Callback function
					if (callback) {
						callback.apply(this);
					}

					$(this).remove();
				} else {
					$(this).slideUp(duration, function() {
						// Callback function
						if (callback) {
							callback.apply(this);
						}

						$(this).remove();
					});
				}
			}
		});

		return this;
	};

	/**
	 * Open/close fieldsets
	 */
	$.fn.toggleFieldsetOpen = function() {
		this.each(function() {
			/*
			 * Tip: if you want to add animation or do anything that should not occur at startup closing, 
			 * check if the element has the class 'collapse':
			 * if (!$(this).hasClass('collapse')) { // Anything that sould no occur at startup }
			 */

			// Change
			$(this).closest('fieldset, .fieldset').toggleClass('collapsed');
		});

		return this;
	};

	/**
	 * Add a semi-transparent layer in front of an element
	 */
	$.fn.addEffectLayer = function(options) {
		var settings = $.extend({}, $.fn.addEffectLayer.defaults, options);

		this.each(function(i) {
			var element = $(this);

			// Add layer
			var refElement = getNodeRefElement(this);
			var layer = $('<div class="loading-mask"><span>' + settings.message + '</span></div>').insertAfter(refElement);

			// Position
			var elementOffset = element.position();
			layer.css({
				top: elementOffset.top + 'px',
				left: elementOffset.left + 'px'
			}).width(element.outerWidth()).height(element.outerHeight());

			// Effect
			var span = layer.children('span');
			var marginTop = parseInt(span.css('margin-top'));
			span.css({ 'opacity': 0, 'marginTop': (marginTop - 40) + 'px' });
			layer.css({ 'opacity': 0 }).animate({ 'opacity': 1 }, {
				'complete': function() {
					span.animate({ 'opacity': 1, 'marginTop': marginTop + 'px' });
				}
			});
		});

		return this;
	};

	/**
	 * Retrieve the reference element after which the layer will be inserted
	 * @param HTMLelement node the node on which the effect is applied
	 * @return HTMLelement the reference node
	 */

	function getNodeRefElement(node) {
		var element = $(node);

		// Special case
		if (node.nodeName.toLowerCase() == 'document' || node.nodeName.toLowerCase() == 'body') {
			var refElement = $(document.body).children(':last').get(0);
		} else {
			// Look for the reference element, the one after which the layer will be inserted
			var refElement = node;
			var offsetParent = element.offsetParent().get(0);

			// List of elements in which we can add a div
			var absPos = ['absolute', 'relative'];
			while (refElement && refElement !== offsetParent && !$.inArray($(refElement.parentNode).css('position'), absPos)) {
				refElement = refElement.parentNode;
			}
		}

		return refElement;
	}

	// Default params for the loading effect layer
	$.fn.addEffectLayer.defaults = {
		message: 'Please wait...'
	};

	/**
	 * jQuery load() method wrapper : add a visual effect on the load() target
	 * Parameters are the same as load()
	 */
	$.fn.loadWithEffect = function() {
		// Add effect layer
		this.addEffectLayer({
			message: $.fn.loadWithEffect.defaults.message
		});

		// Rewrite callback function
		var target = this;
		var callback = false;
		var args = $.makeArray(arguments);
		var index = args.length;
		if (args[2] && typeof args[2] == 'function') {
			callback = args[2];
			index = 2;
		} else if (args[1] && typeof args[1] == 'function') {
			callback = args[1];
			index = 1;
		}

		// Custom callback
		args[index] = function(responseText, textStatus, XMLHttpRequest) {
			// Get the effect layer
			var refElement = getNodeRefElement(this);
			var layer = $(refElement).next('.loading-mask');
			var span = layer.children('span');

			// If success
			if (textStatus == 'success' || textStatus == 'notmodified') {
				// Initial callback
				if (callback) {
					callback.apply(this, arguments);
				}

				// Remove effect layer
				layer.stop(true);
				span.stop(true);
				var currentMarginTop = parseInt(span.css('margin-top'));
				var marginTop = parseInt(span.css('margin-top', '').css('margin-top'));
				span.css({ 'marginTop': currentMarginTop + 'px' }).animate({ 'opacity': 0, 'marginTop': (marginTop - 40) + 'px' }, {
					'complete': function() {
						layer.fadeAndRemove();
					}
				});
			} else {
				span.addClass('error').html($.fn.loadWithEffect.defaults.errorMessage + '<br><a href="#">' + $.fn.loadWithEffect.defaults.retry + '</a> / <a href="#">' + $.fn.loadWithEffect.defaults.cancel + '</a>');
				span.children('a:first').click(function(event) {
					event.preventDefault();

					// Relaunch request
					$.fn.load.apply(target, args);

					// Reset
					span.removeClass('error').html($.fn.loadWithEffect.defaults.message).css('margin-left', '');
				});
				span.children('a:last').click(function(event) {
					event.preventDefault();

					// Remove effect layer
					layer.stop(true);
					span.stop(true);
					var currentMarginTop = parseInt(span.css('margin-top'));
					var marginTop = parseInt(span.css('margin-top', '').css('margin-top'));
					span.css({ 'marginTop': currentMarginTop + 'px' }).animate({ 'opacity': 0, 'marginTop': (marginTop - 40) + 'px' }, {
						'complete': function() {
							layer.fadeAndRemove();
						}
					});
				});

				// Centering
				span.css('margin-left', -Math.round(span.outerWidth() / 2));
			}
		};

		// Redirect to jQuery load
		$.fn.load.apply(target, args);

		return this;
	};

	// Default texts for the loading effect layer
	$.fn.loadWithEffect.defaults = {
		message: 'Loading...',
		errorMessage: 'Error while loading',
		retry: 'Retry',
		cancel: 'Cancel'
	};

	/**
	 * Enable any button with workaround for IE lack of :disabled selector
	 */
	$.fn.enableBt = function() {
		$(this).attr('disabled', false);
		if ($.browser.msie && $.browser.version < 9) {
			$(this).removeClass('disabled');
		}
	}; /**
	 * Disable any button with workaround for IE lack of :disabled selector
	 */
	$.fn.disableBt = function() {
		$(this).attr('disabled', true);
		if ($.browser.msie && $.browser.version < 9) {
			$(this).addClass('disabled');
		}
	};
})(jQuery);