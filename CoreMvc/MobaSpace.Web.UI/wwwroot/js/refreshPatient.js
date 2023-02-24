$(document).ready(function () {
    window.setInterval("checkNuitspatients()", 30000);
    window.setInterval("CheckPatientPosition()", 3000);
});

/**
 * Refresh page if max Id is differents 
 */
function checkNuitspatients() {
    $.ajax({
        url: "/Patients/GetMaxNuitsId",
        type: "GET",
        dataType: "html",
        success: function (maxId) {
            var maxIdElement = document.getElementById('maxNuitId').value;
            if (maxId != maxIdElement) {
                updatePartialView();
            }
        },
        error: function () {
            console.error('Erreur durant la vérification des Nuits');
        }
    });
   
}

/**
 * Change icon position if value of posture as change
 */
function CheckPatientPosition() {
    var LimiteLastUpdate;
    $.ajax({
        url: "/Patients/GetLimitLastUpdate",
        type: 'GET',
        success:
            function (result) {
                LimiteLastUpdate = result;
            }
    });

    $.ajax({
        url: "/Patients/GetPatientsPosition",
        type: 'GET',
        success:
            function (result) {
                $.each(result, function (index, patient) {
                    if (patient.posture != null) {
                        var Posture = "";
                        if (patient.posture == 0) {
                            Posture = "Allonge";
                        } else if (patient.posture == 1) {
                            Posture = "Assis";
                        } else if (patient.posture == 2) {
                            Posture = "Debout";
                        }
                        document.getElementById("imgPosture_" + patient.id).setAttribute('src', '/images/patients/' + Posture + '.png');
                    } else {
                        document.getElementById("imgPosture_" + patient.id).setAttribute('src', '/images/patients/PostureInc.png');
                    }
                    if (LimiteLastUpdate == patient.lastUpdate) {
                        document.getElementById("lastUpdate " + patient.id).innerHTML = "Indisponible";
                    } else {
                        document.getElementById("lastUpdate " + patient.id).innerHTML = patient.lastUpdate;
                    }
                   
                });
            }
    });
}
