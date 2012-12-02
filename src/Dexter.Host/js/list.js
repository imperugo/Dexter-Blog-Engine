/**
 * Lists generic controls
 */

(function($)
{
	// List styles setup
	$.fn.addTemplateSetup(function()
	{
		// Closed elements
		this.find('.close').toggleBranchOpen().removeClass('close');
		
		// :first-of-type is buggy with jQuery under IE
		this.find('dl.accordion dt:first-child + dd').siblings('dd').hide();
		
		// Tasks dialog
		if (!$.browser.msie || $.browser.version > 8)	// IE is buggy on this animation
		{
			this.find('.task-dialog').parent().hover(function()
			{
				$(this).find('.task-dialog > li.auto-hide').expand();
				
			}, function()
			{
				$(this).find('.task-dialog > li.auto-hide').fold();
			});
		}
		
		// Arbo elements controls
		this.find('.arbo .toggle, .collapsible-list li:has(ul) > :first-child, .collapsible-list li:has(ul) > :first-child + span').click(function(event)
		{
			// Toggle style
			$(this).toggleBranchOpen();
			
			// Prevent link action
			if (this.nodeName.toLowerCase() == 'a')
			{
				event.preventDefault();
			}
		});
		
		// Accordions effect
		this.find('dl.accordion dt').click(function()
		{
			$(this).next('dd').slideDown().siblings('dd').slideUp().prev('dt');
			
			// Effect need for rounded corners
			$(this).addClass('opened').siblings('dt').removeClass('opened');
		});
		
	}, true);
	
	/**
	 * Open/close branch
	 */
	$.fn.toggleBranchOpen = function()
	{
		this.each(function()
		{
			/*
			 * Tip: if you want to add animation or do anything that should not occur at startup closing, 
			 * check if the element has the class 'close':
			 * if (!$(this).hasClass('close')) { // Anything that sould no occur at startup }
			 */
			 
			// Change
			$(this).closest('li').toggleClass('closed');
		});
		
		return this;
	};
	
})(jQuery);