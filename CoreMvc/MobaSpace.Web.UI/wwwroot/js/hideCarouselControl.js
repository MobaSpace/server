$(document).ready(function () {
    $('.carousel-control-modal-next').hide();
})

var patientId;

/**
 * Set patientId 
 * @param {any} currentPatientId
 */
function setIdPatient(currentPatientId) {

    patientId = currentPatientId;
    document.getElementById('SelectPatientId').value = currentPatientId;
    document.getElementById('ModalType').value = "Nuit"
}

/**
 * Update corresponding carousel
 */
$('div[id^="carouselPatient_"]').on('slide.bs.carousel', function (e) {
    var slidingItemsAsIndex = $("div[name = \"carousel-" + patientId + "-item\"]").length - 1;
    var currentSlide = e.to;

    if (currentSlide == slidingItemsAsIndex) {
        $('.carousel-control-modal-prev').hide();
    } else {
        $('.carousel-control-modal-prev').show();
    }

    if (currentSlide == 0) {
        $('.carousel-control-modal-next').hide();
    } else {
        $('.carousel-control-modal-next').show();
    }
});
