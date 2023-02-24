/**
 * Build graph from data controler for Nuit page
 * @param {any} currentPatient
 * @param {any} daySelected
 */
function buildNightGraph(currentPatient, daySelected) {
    document.getElementById('ModalType').value = "GraphNuit"
    $.ajax({
        url: "/Patients/DataGraph",
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {
            id: currentPatient
        },
        dataType: 'json',
        success: function (listData) {
            NRGraph(listData);
            NSGraph(listData);
            SNGraph(listData);
            document.getElementById("titleGraphModal").innerHTML = "Affichage du " + daySelected;
        },
        error: function () {
            console.error("Impossible de récupérer les nuits !");
        }

    })

}

var CurrentPatientName = "";

function buildNightTable(nuitsPatient, daySelected, nameResident) {
    document.getElementById('ModalType').value = "TabNuit";
    BuildTable(nuitsPatient);
    CurrentPatientName = nameResident;
    document.getElementById("titleTabNuit").innerHTML = "Affichage du " + daySelected;
}

/**
 * Build a graph to see change in time
 * @param {any} listData
 */
function NRGraph(listData) {
    var listNR = GetNRData(listData);
    var listDate = GetListDate(listData);

    var layout = {
        title: "Nombre de reveils",
        xaxis: {
            title: 'jours'
        },
        yaxis: {
            title: 'Reveils',
            rangemode: 'tozero'
        }

    }

    var traceNR = {
        name: 'Rapport des ',
        x: listDate,
        y: listNR,
        type: 'scatter'
    }

    var graphData = [traceNR];

    Plotly.newPlot('GraphNR', graphData , layout);


}
/**
 * Build a graph to see change in time
 * @param {any} listData
 */
function NSGraph(listData) {
    var listNS = GetNSData(listData);
    var listDate = GetListDate(listData);

    var layout = {
        title: "Nombre de Sorties",
        xaxis: {
            title: 'jours'
        },
        yaxis: {
            title: 'Sortie',
            rangemode: 'tozero'
        }

    }


    var traceNS = {
        name: 'FRMin',
        x: listDate,
        y: listNS,
        type: 'scatter'
    }

    var graphData = [traceNS];

    Plotly.newPlot('GraphNS', graphData , layout);



}
/**
 * Build a graph to see change in time
 * @param {any} listData
 */
function SNGraph(listData) {
    var listSN = GetSNData(listData);
    var listDate = GetListDate(listData);

    var layout = {
        title: "Temps de Sommeil",
        xaxis: {
            title: 'jours',

        },
        yaxis: {
            title: 'Heures (h) ',
            rangemode: 'tozero'
        },
        legend: {

            xanchor: "left",
            yanchor: "middle",

        }
    }


    var traceSN = {
        name: 'Score nuit',
        x: listDate,
        y: listSN[0],
        yaxis: 'y',
        xaxis: 'x',
        type: 'scatter'
    }


    var graphData = [traceSN];

    Plotly.newPlot('GraphSN', graphData, layout);



}

/**
 * Handle NSData array
 * @param {any} listData
 */
function GetNSData(listData) {

    var NbSorties = new Array();

    listData.forEach(function (temp) {
        NbSorties.push(parseFloat(temp[1]));
    });



    return NbSorties;
}
/**
 * Handle NRData array
 * @param {any} listData
 */
function GetNRData(listData) {


    var NbReveils = new Array();

    listData.forEach(function (temp) {
        NbReveils.push(parseInt(temp[0]));
    });



    return NbReveils;
}

/**
 * Handle SNData array
 * @param {any} listData
 */
function GetSNData(listData) {

    var SNTab = new Array();


    listData.forEach(function (temp) {
        SNTab.push(parseFloat(temp[2]));
    });



    return [SNTab];
}
/**
 * Return table with last days
 */
function GetListDate(listData) {

    var dateTab = new Array();

    listData.forEach(function (temp) {
        dateTab.push(temp[3]);
    });



    return dateTab;
}

function BuildTable(listData) {
    var columnName = ["Date de la nuit", "Couché à", "Levé à", "Tps endormi au lit", "Nbre de reveils", "Tps réveillé au lit", "Tps réveillé hors du lit"]
    var modalBody = document.getElementById('TabNuit7j');
    if (modalBody.childElementCount >= 1) {
        modalBody.firstElementChild.remove();
    }
    var table = document.createElement('table');
    table.id = 'TabRecapNuit7j'
    table.style.width = '100%';
    table.style.backgroundColor = 'white';
    table.setAttribute('border', '1');
    var tableBody = document.createElement('tbody');
    var thr = document.createElement('tr');
    thr.style.backgroundColor = '#4FC1CD';
    columnName.forEach(function (name) {
        var th = document.createElement('th');
        th.appendChild(document.createTextNode(name));
        thr.appendChild(th);
    })
    tableBody.appendChild(thr);
    listData.forEach(function (nuit) {
        var tr = document.createElement('tr');
        columnName.forEach(function (name) {
            tr.appendChild(buildTd(name, nuit))
        })
        tableBody.appendChild(tr)
    })
    table.appendChild(tableBody)
    modalBody.appendChild(table)
}

function buildTd(nameCol, nuit) {
    var td = document.createElement('td');
    if (nameCol == "Date de la nuit") {
        if (nuit.UnencriptedNuit.DateDebut != null && nuit.UnencriptedNuit.DateFin != null) {
            var dateDebut = new Date(nuit.UnencriptedNuit.DateNuit.toString() + 'Z');
            var dateFin = new Date(nuit.UnencriptedNuit.DateFin.toString() + 'Z');
            td.appendChild(document.createTextNode(dateDebut.toLocaleString('fr-FR', { weekday: 'short' }) + " " + dateDebut.getDate() + " à " + dateFin.toLocaleString('fr-FR', { weekday: 'short' }) + " " + dateFin.getDate() + " " + dateFin.toLocaleString('fr-FR', { month: 'long' })));
        }
    }
    else if (nameCol == "Couché à") {
        if (nuit.UnencriptedNuit.DateDebut != null) {
            var dateDebut = new Date(nuit.UnencriptedNuit.DateDebut.toString() + 'Z');
            center = document.createElement('center');
            center.appendChild(document.createTextNode(dateDebut.getHours() + "H" + dateDebut.getMinutes()));
            td.appendChild(center);
        }
    } else if (nameCol == "Levé à") {
        if (nuit.UnencriptedNuit.DateFin != null) {
            var dateFin = new Date(nuit.UnencriptedNuit.DateFin.toString() + 'Z');
            center = document.createElement('center');
            center.appendChild(document.createTextNode(dateFin.getHours() + "H" + dateFin.getMinutes()));
            td.appendChild(center);
        }
    } else if (nameCol == "Tps endormi au lit") {
        if (nuit.UnencriptedNuit.DureeSommeil != null) {
            center = document.createElement('center');
            center.appendChild(document.createTextNode(nuit.UnencriptedNuit.DureeSommeil.Value.Hours + "h " + nuit.UnencriptedNuit.DureeSommeil.Value.Minutes + "min"));
            td.appendChild(center);
        }
    } else if (nameCol == "Nbre de reveils") {
        if (nuit.UnencriptedNuit.NbReveils != null) {
            center = document.createElement('center');
            center.appendChild(document.createTextNode(nuit.UnencriptedNuit.NbReveils));
            td.appendChild(center);
        }
    }
    else if (nameCol == "Tps réveillé au lit") {
        if (nuit.UnencriptedNuit.DureeReveilAuLit != null) {
            center = document.createElement('center');
            center.appendChild(document.createTextNode(nuit.UnencriptedNuit.DureeReveilAuLit.Value.Hours + "h " + nuit.UnencriptedNuit.DureeReveilAuLit.Value.Minutes + "min"));
            td.appendChild(center);
        }
    }
    else if (nameCol == "Tps réveillé hors du lit") {
        if (nuit.UnencriptedNuit.DureeSommeil != null) {
            center = document.createElement('center');
            center.appendChild(document.createTextNode(nuit.UnencriptedNuit.DureeReveilHorsLit.Value.Hours + "h " + nuit.UnencriptedNuit.DureeReveilHorsLit.Value.Minutes + "min"));
            td.appendChild(center);
        }
    }
    return td;
}

function HtmlTOExcel(type, fun, dl) {
    var elt = document.getElementById('TabRecapNuit7j');
    var wb = XLSX.utils.table_to_book(elt, { sheet: "sheet1" });
    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fun || ('resident-' + CurrentPatientName + '-Enregistrement-Nuit.' + (type || 'xlsx')));
}
