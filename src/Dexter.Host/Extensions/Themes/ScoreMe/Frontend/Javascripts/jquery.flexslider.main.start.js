jQuery(window).load(function() {
/*global jQuery:false */
"use strict";
	
  jQuery('.mainflex,.singleslider').flexslider({
	animation: "fade",
	slideshow: true,                //Boolean: Animate slider automatically
	slideshowSpeed: 7000,           //Integer: Set the speed of the slideshow cycling, in milliseconds
	animationDuration: 600,         //Integer: Set the speed of animations, in milliseconds
	smoothHeight: true
    });
  
});