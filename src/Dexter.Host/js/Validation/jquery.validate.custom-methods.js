/*!
 * Custom methods for jQuery Validate and jQuery Validate Unobtrusive
 *
 * Methods written specifically for Alumni CareerService project
 * Daniel Crisp - gaia.is.it
 *
 */

// add adaptor to unobtrusive
jQuery.validator.unobtrusive.adapters.addMinMax('checkbox-range', 'minCheckboxes', 'maxCheckboxes', 'minMaxCheckboxes');

// create a reusable function to count the number of checked checkboxes
jQuery.validator.countCheckboxRangeGroup = function(element) {
	var $selectedItems = jQuery('input[data-checkbox-range-group="' + $(element).data('checkbox-range-group') + '"]:checked'),
	    selectedCheckboxes = jQuery.map($selectedItems, function(n, i) { return n.value; });

	return selectedCheckboxes.length;
};

// min validator
jQuery.validator.addMethod('minCheckboxes', function(value, element, params) {

	var selectedCheckboxes = jQuery.validator.countCheckboxRangeGroup(element);

	return (selectedCheckboxes >= parseInt(params, 10));

}, '');

// max validator
jQuery.validator.addMethod('maxCheckboxes', function(value, element, params) {

	var selectedCheckboxes = jQuery.validator.countCheckboxRangeGroup(element);

	return (selectedCheckboxes <= params);

}, '');

// min max validator
jQuery.validator.addMethod('minMaxCheckboxes', function(value, element, params) {

	var selectedCheckboxes = jQuery.validator.countCheckboxRangeGroup(element);

	return (selectedCheckboxes >= parseInt(params[0], 10) && selectedCheckboxes <= parseInt(params[1], 10));

}, '');