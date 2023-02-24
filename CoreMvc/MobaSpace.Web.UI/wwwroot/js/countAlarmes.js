
updateNBalarmes();

setInterval(function () {
                updateNBalarmes();
            }, 300000);

/**
 * Calculates and returns the number of alarms
 */
function updateNBalarmes() {
    $.ajax({
        url: "/Alarmes/CountAlarmes",
        type: 'GET',
        success: function (nbAlarmes) {
            document.getElementById("notifAlarmes").innerHTML = nbAlarmes;
            if (nbAlarmes == 0) {
                document.getElementById("notifAlarmes").classList.remove("badge-danger");
                document.getElementById("notifAlarmes").classList.add("badge-success");
            } else {
                document.getElementById("notifAlarmes").classList.remove("badge-success");
                document.getElementById("notifAlarmes").classList.add("badge-danger");
            }
        },
        error: function () {
            console.error("error in CoutAlarmes");
        }
    });
}

