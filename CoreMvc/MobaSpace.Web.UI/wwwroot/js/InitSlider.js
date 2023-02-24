/**
 * Initialise slider
 */ 
$(document).ready(function () {
    if ($('#labelFreqCardMin').val() == $('#labelFreqCardMax').val()) {
        $('#rangeCardMin').val(0);
        $('#rangeCardMax').val(250);
    } else {
        $('#rangeCardMin').val($('#labelFreqCardMin').val());
        $('#rangeCardMax').val($('#labelFreqCardMax').val());
    }
    if ($('#labelFreqRespMin').val() == $('#labelFreqRespMax').val()) {
        $('#rangeFreqMin').val(0);
        $('#rangeFreqMax').val(250);
    } else {
        $('#rangeFreqMin').val($('#labelFreqRespMin').val());
        $('#rangeFreqMax').val($('#labelFreqRespMax').val());
    }

    $('#rangeCoucherH').val($('#labelCoucherH').val());

    $('#rangeCoucherMin').val($('#labelCoucherMin').val());

    $('#rangeLeverH').val($('#labelLeverH').val());

    $('#rangeLeverMin').val($('#labelLeverMin').val());

    $('#rangeMaxHDL').val($('#labelHDLMax').val());

    $('#rangeAALMax').val($('#labelAALMax').val());

})