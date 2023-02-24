/**
 * define range 
 * @param {any} min
 * @param {any} max
 * @param {any} value
 * @param {any} idRanger
 * @param {any} idLabel
 * @param {any} i
 * @param {any} end
 * @param {any} type
 */
function endRanger(min, max, value, idRanger, idLabel, i, end, type) {
    var intMin = parseInt(min);
    var intMax = parseInt(max);
    var intValue = parseInt(value);
    var newValue = "null";
    if (intValue >= intMax) {

        intValue = intMax;
    }
    else if (intValue <= intMin) {

        intValue = intMin;
    }
    if (type == "label") {
        if (intValue != parseInt(value)) {
            intValue += i;
            newValue = intValue.toString();
            $(idLabel).val(newValue);
        }
        newValue = intValue.toString();
        $(idRanger).val(newValue);
    }
    else if (type == "range") {
        if (intValue != parseInt(value)) {
            intValue += parseInt(i);
            newValue = intValue.toString();
            $(idRanger).val(newValue);
        }
        newValue = intValue.toString()
        $(idLabel).val(newValue);
    }

    return newValue;
}


/**
 * for existing value print this value
 */
$(function () {


    // Coucher Heure
    $('#rangeCoucherH').on('input change', function () {
        $('#labelCoucherH').val($('#rangeCoucherH').val());
    })

    $('#labelCoucherH').on('value change', function () {
        $('#rangeCoucherH').val($('#labelCoucherH').val());
    })

    // Coucher Min
    $('#rangeCoucherMin').on('input change', function () {
        $('#labelCoucherMin').val($('#rangeCoucherMin').val());
    })

    $('#labelCoucherMin').on('value change', function () {
        $('#rangeCoucherMin').val($('#labelCoucherMin').val());
    })

    // Lever Heure
    $('#rangeLeverH').on('input change', function () {
        $('#labelLeverH').val($('#rangeLeverH').val());
    })

    $('#labelLeverH').on('value change', function () {
        $('#rangeLeverH').val($('#labelLeverH').val());
    })

    // Lever Min
    $('#rangeLeverMin').on('input change', function () {
        var value = $('#rangeLeverMin').val();
        $('#labelLeverMin').val(value);
    })

    $('#labelLeverMin').on('value change', function () {
        $('#rangeLeverMin').val($('#labelLeverMin').val());
    })

    // HDL 
    $('#rangeMaxHDL').on('input change', function () {
        $('#labelHDLMax').val($('#rangeMaxHDL').val());
    })

    $('#labelHDLMax').on('value change', function () {
        $('#rangeMaxHDL').val($('#labelHDLMax').val());
    })
    $('#rangeAALMax').on('input change', function () {
        $('#labelAALMax').val($('#rangeAALMax').val());
    })

    $('#labelAALMax').on('value change', function () {
        $('#rangeAALMax').val($('#labelAALMax').val());
    })
});

