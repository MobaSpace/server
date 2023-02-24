/**
 * Build graph from data controler for Score page 
 * @param {any} currentRoom
 */
function buildScoreGraph(currentRoom) {

    $.ajax({
        url: "/Forme/DataGraph",
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {
            NumCh: currentRoom
        },
        dataType: 'json',
        success: function (listData) {
            SVSPGraph(listData[0], listData[1], listData[4]);
            ICGraph(listData[2], listData[4]);
            TRGraph(listData[3], listData[4]);
            document.getElementById("titleGraphModal").innerHTML = "Historique hebdomadaire";
        },
        error: function () {
            console.error("Impossible de récupérer les Scores !");
        }

    })

}

/**
 * build graph to compare score veille and score prédit
 * @param {any} listSV
 * @param {any} listSP
 */
function SVSPGraph(listSV, listSP, listDate) {

    const intListSV = listSV.map((i) => parseFloat(i));
    const intListSP = listSP.map((i) => parseFloat(i));

    var layout = {
        title: "Score Veille / Score Prédit",
        xaxis: {
            title: 'jours'
        },
        yaxis: {
            title: '%',
            rangemode: 'tozero',
            range: [0, 100],
            dtick: 10

        }

    }

    var TraceSV = {
        name: 'Score Veille',
        x: listDate,
        y: intListSV,
        type: 'scatter'
    }

    var TraceSP = {
        name: 'Score Prédit',
        x: listDate,
        y: intListSP,
        type: 'scatter'
    }

    var graphData = [TraceSV, TraceSP ];

    Plotly.newPlot('GraphSVSP', graphData , layout);


}

/**
 * Build a graph to see change in time
 * @param {any} listIC
 */
function ICGraph(listIC, listDate) {

    const intListIC = listIC.map((i) => parseFloat(i));

    var layout = {
        title: "Indice de Confiance",
        xaxis: {
            title: 'jours'
        },
        yaxis: {
            title: '%',
            rangemode: 'tozero',
            range: [0, 100],
            dtick: 10
        }

    }


    var traceIC = {
        name: 'IndicedeConfiance',
        x: listDate,
        y: intListIC,
        type: 'scatter'
    }

    var graphData = [traceIC];

    Plotly.newPlot('GraphIC', graphData , layout);



}
/**
 * Build a graph to see change in time
 * @param {any} listTR
 */
function TRGraph(listTR, listDate) {

    const intListTR = listTR.map((i) => parseFloat(i));

    var layout = {
        title: "Taux de remplissage",
        xaxis: {
            title: 'jours',

        },
        yaxis: {
            title: '%',
            rangemode: 'tozero',
            range: [0, 100],
            dtick: 10

        },
        legend: {

            xanchor: "left",
            yanchor: "middle",

        }
    }


    var traceTR = {
        name: 'Taux de remplissage',
        x: listDate,
        y: intListTR,
        yaxis: 'y',
        xaxis: 'x',
        type: 'scatter'
    }


    var graphData = [traceTR];

    Plotly.newPlot('GraphTRemp', graphData, layout);



}

