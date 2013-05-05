jQuery(document).ready(function(){
/*global jQuery:false */
/*jshint devel:true, laxcomma:true, smarttabs:true */
"use strict";

		// hide .scrollTo_top first
		jQuery(".scrollTo_top").hide();
		// fade in .scrollTo_top
		jQuery(function () {
			jQuery(window).scroll(function () {
				if (jQuery(this).scrollTop() > 100) {
					jQuery('.scrollTo_top').fadeIn();
				} else {
					jQuery('.scrollTo_top').fadeOut();
				}
			});
	
			// scroll body to 0px on click
		jQuery('.scrollTo_top a').click(function(){
			jQuery('html, body').animate({scrollTop:0}, 500 );
			return false;
		});
		});


	// hide menu on mobile screens
	  jQuery(function() {
	  /* Check width on page load*/
		  if ( jQuery(window).width() < 639) {
			   jQuery('#navigation').addClass('hidenav');
			   jQuery('a#navtrigger').addClass('showtrig');
			  }
	  });
	
	
	// trigger + show menu on fire
	  jQuery(window).resize(function() {
	  /*If browser resized, check width again */
		  if (jQuery(window).width() < 639) {
			   jQuery('#navigation').addClass('hidenav');
			   jQuery('a#navtrigger').addClass('showtrig');
			  }
		  else {
			  jQuery('#navigation').removeClass('hidenav');
			  jQuery('a#navtrigger').removeClass('showtrig');}
	  });
	  
        jQuery('a#navtrigger').click(function(){ 
                jQuery('#navigation').toggleClass('shownav'); 
                jQuery('#sec-nav').toggleClass('shownav'); 
                jQuery(this).toggleClass('active'); 
                return false; 
        }); 


		/* wp gallery hover */	
				
		jQuery('.tabbig_small,.tab-post,.big_single,.small_posts,.fblock,.widgetflexslider ul li,ul.raws li').hover(function() {
			jQuery(this).find('img')
				.animate({opacity:'0.6'}, 300); 
		
			} , function() {
		
			jQuery(this).find('img')
				.animate({opacity:'1'}, 400); 
		});

		/* flex slider arrows */	

		jQuery('.mainflex').on('mouseenter', function() {
			jQuery(this).find('.flex-direction-nav').stop(true, true).animate({'opacity': '1'}, {duration:350});
	
		});
	
		jQuery('.mainflex').on('mouseleave', function() {
			jQuery(this).find('.flex-direction-nav').stop(true, true).animate({'opacity': '0'}, {duration:350});                           
		}); 


		/* wp gallery hover */	
				
		jQuery('.gallery-item,.mi-slider ul li,.related li').hover(function() {
			jQuery(this).find('img')
				.animate({opacity:'0.2'}, 300); 
		
			} , function() {
		
			jQuery(this).find('img')
				.animate({opacity:'1'}, 400); 
		});


	/* Tooltips */
		jQuery("body").prepend('<div class="tooltip"><p></p></div>');
		var tt = jQuery("div.tooltip");
		
		jQuery(".flickr_badge_image a img,ul.social-menu li a").hover(function() {								
			var btn = jQuery(this);
			
			tt.children("p").text(btn.attr("title"));								
						
			var t = Math.floor(tt.outerWidth(true)/2),
				b = Math.floor(btn.outerWidth(true)/2),							
				y = btn.offset().top - 30,
				x = btn.offset().left - (t-b);
						
			tt.css({"top" : y+"px", "left" : x+"px", "display" : "block"});			
			   
		}, function() {		
			tt.hide();			
		});


	function lightbox() {
		// Apply PrettyPhoto to find the relation with our portfolio item
		jQuery("a[rel^='prettyPhoto']").prettyPhoto({
			// Parameters for PrettyPhoto styling
			animationSpeed:'fast',
			slideshow:5000,
			theme:'pp_default',
			show_title:false,
			overlay_gallery: false,
			social_tools: false
		});
	}
	
	if(jQuery().prettyPhoto) {
		lightbox();
	}



});