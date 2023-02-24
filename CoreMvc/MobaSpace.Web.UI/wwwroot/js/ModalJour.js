function fillModalJour(currentPatient) {
    var patient = getDayPatientData(currentPatient);
    var maxJour = getMaxJour(patient);
    document.getElementById('SelectPatientId').value = currentPatient;
    document.getElementById('ModalType').value = "Jour"
    document.getElementById('nextbutton').style.visibility = "hidden";
    updateDayData(maxJour, patient);

}



/**
 * Fill Jour modal
 * @param {any} currentPatient
 */
function getDayPatientData(currentPatient) {
    var patienObject;
    $.ajax({
        url: "/Patients/GetJours",
        type: 'GET',
        async: false,
        contentType: 'application/json; charset=utf-8',
        data: {
            id: currentPatient
        },
        success: function (data) {
            patientObject = JSON.parse(data);
        },
        error: function () {
            console.error("Impossible de récupérer les jours pour le patient " + currentPatient);
        }

    })
    return patientObject;

    
}


/**
 * Insert data in modal 
 * @param {any} Day
 * @param {any} patient
 */
function updateDayData(day, patient) {

    jours = new Array('Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi');
    var currentDay = new Date(Date.parse(day.UnencriptedJour.DateJour.replace('T', ' ').replace('Z', '')));
    var getdays = currentDay.getDay();
    var lever_h = patient.UnencriptedPatient.Lever_h.toLocaleString('fr-FR', {
        minimumIntegerDigits: 2,
        useFrouping: false
    });
    var lever_min = patient.UnencriptedPatient.Lever_min.toLocaleString('fr-FR', {
        minimumIntegerDigits: 2,
        useFrouping: false
    });
    var coucher_h = patient.UnencriptedPatient.Coucher_h.toLocaleString('fr-FR', {
        minimumIntegerDigits: 2,
        useFrouping: false
    });
    var coucher_min = patient.UnencriptedPatient.Coucher_min.toLocaleString('fr-FR', {
        minimumIntegerDigits: 2,
        useFrouping: false
    });
    var Month = currentDay.getMonth() + 1;
    var dateHierString = " " + jours[getdays] + " " + currentDay.getDate() + "/" + Month + "/" + currentDay.getFullYear() + " ";
    document.getElementById("dateCurrentJour").innerHTML = "Du" + dateHierString + " " + lever_h + "h" + lever_min + " au" + dateHierString + " " + coucher_h + "h" + coucher_min + "";

    if (day.UnencriptedJour.NbPas != null) {
        document.getElementById("NbTotalPas").innerHTML = day.UnencriptedJour.NbPas;
    } else {
        document.getElementById("NbTotalPas").innerHTML = "Aucune données";
    }
    if (day.UnencriptedJour.TempsTotalActivite != null) {
        document.getElementById("TempsDeplacement").innerHTML = convertMilliToTime(day.UnencriptedJour.TempsTotalActivite * 1000);

    } else {
        document.getElementById("TempsDeplacement").innerHTML = "Aucune données";
    }
    if (day.UnencriptedJour.VitesseMarcheMoyenne != null) {
        document.getElementById("NbPas/s").innerHTML = ((day.UnencriptedJour.VitesseMarcheMoyenne.reduce((a, b) => a + b, 0)) / day.UnencriptedJour.VitesseMarcheMoyenne.length).toFixed(2);

    } else {
        document.getElementById("NbPas/s").innerHTML = "Aucune données";
    }
    var convertedLimiteLever = convertTimeToMilli(patient.UnencriptedPatient.Lever_h, patient.UnencriptedPatient.Lever_min);
    var convertedLimiteCoucher = convertTimeToMilli(patient.UnencriptedPatient.Coucher_h, patient.UnencriptedPatient.Coucher_min);
    var dureeReveil = Math.abs(convertedLimiteCoucher - convertedLimiteLever);
    document.getElementById("TempsSansDeplacement").innerHTML = convertMilliToTime(dureeReveil - (day.UnencriptedJour.TempsTotalActivite * 1000));

    document.getElementById("IdJour").value = day.UnencriptedJour.Id;
    document.getElementById("IdDayPatient").value = patient.UnencriptedPatient.Id;

    graphPas(patient.Jours);
}

function getMaxJour(patientObject) {
    var listPatientDates = []
    patientObject.Jours.forEach(function (jour) {
        listPatientDates.push(new Date(jour.UnencriptedJour.DateJour))
    })
    var MaxDate = new Date(Math.max.apply(null, listPatientDates));
    var MaxJour = {} ;
    patientObject.Jours.forEach(function (jour) {
        var DateJour = new Date(jour.UnencriptedJour.DateJour);
        if (DateJour.toLocaleDateString() == MaxDate.toLocaleDateString()) {
            MaxJour = jour;
        }
    });
    return MaxJour;
}


/**
 * Convert hour, minute and second in millisecond
 * @param {any} hour
 * @param {any} minute
 * @param {any} second
 */
function convertTimeToMilli(hour, minute, second = 0) {
    var hourInSecond = hour * 3600;
    var minuteInSecond = minute * 60;
    var TimeInMilli = (hourInSecond + minuteInSecond + second) * 1000;
    return TimeInMilli;
}


/**
 * Convert millisecond in Time at format "HH h mm"
 * @param {any} milliSecond
 */
function convertMilliToTime(milliSecond) {
    var secondLeft = milliSecond / 1000;
    var secondToHour = Math.trunc(secondLeft / 3600);
    secondLeft = secondLeft - secondToHour * 3600;
    var secondToMinute = Math.trunc(secondLeft / 60);
    secondToHour = secondToHour.toLocaleString('fr-FR', {
        minimumIntegerDigits: 2,
        useFrouping: false
    });
    secondToMinute = secondToMinute.toLocaleString('fr-FR', {
        minimumIntegerDigits: 2,
        useFrouping: false
    });
    var Time = secondToHour + " h " + secondToMinute;
    return Time;
}

function graphPas(ListJours) {
    var listNbPas = [];
    var listDate = [];

    ListJours.forEach(function (jour) {
        listNbPas.push(jour.UnencriptedJour.NbPas);
        listDate.push(jour.DateJour);
    });

    var layout = {
        xaxis: {
            title: 'jours'
        },
        yaxis: {
            title: 'NbPas',
            rangemode: 'tozero'
        },
        margin: {
            l: 50,
            r: 50,
            b: 50,
            t: 50,
            pad: 4
        },
        autosize: true,
        bargap : 0.5
    };

    var traceNbPas = {
        name: "NbPas/jour",
        x: listDate,
        y: listNbPas,
        type: 'scatter'

    }

    var graphData = [traceNbPas];

    Plotly.newPlot('GraphJour', graphData, layout);

}

/**
 * Change modal value
 * @param {any} idJour
 * @param {any} status
 */
async function ChangeDay(idJour, idPatient, status) {
    var patient = await getDayPatientData(idPatient);
    var posJour = patient.Jours.findIndex(i => i.Id === idJour);
    if (status == "next") {
        var newDay = patient.Jours[posJour + 1];
        if (posJour + 1 == patient.Jours.length - 1) {
            document.getElementById('nextbutton').style.visibility = "hidden";
            document.getElementById('prevbutton').style.visibility = "visible";
        }
        else {
            document.getElementById('nextbutton').style.visibility = "visible";
            document.getElementById('prevbutton').style.visibility = "visible";
        }
        updateDayData(newDay, patient);
    }
    else if (status == "prev") {
        var newDay = patient.Jours[posJour - 1];
        if (posJour - 1 == 0) {
            document.getElementById('prevbutton').style.visibility = "hidden";
            document.getElementById('nextbutton').style.visibility = "visible";
        }
        else {
            document.getElementById('nextbutton').style.visibility = "visible";
            document.getElementById('prevbutton').style.visibility = "visible";
        }
        updateDayData(newDay, patient);
    }
}