
function InitQCM(NumCh = null) {
    $.ajax({
        url: "/api/QCM",
        type: 'GET',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success:function (result) {
            const Json = JSON.parse(result);
            var form = document.createElement("form");
            form.setAttribute("id", "formQCM");
            //form.setAttribute("action", "/withings_evt");
            //form.setAttribute("method", "POST");
            var divNumCh = document.createElement("div");
            divNumCh.className = "form-check";
            divNumCh.setAttribute("id", "divNumCh");

            var labelNumCh = document.createElement("label");
            labelNumCh.className = "form-label h5 mt-3 row ";
            labelNumCh.innerHTML = "Numéro de la chambre";
            labelNumCh.setAttribute("for", "numChamber" );

            var numChamber = document.createElement("input");
            numChamber.setAttribute("type", "text");
            numChamber.setAttribute("class", "form-input row");
            numChamber.setAttribute("id", "numChamber");
            if (NumCh != null) {
                numChamber.value = NumCh;
            }
            divNumCh.appendChild(labelNumCh);
            divNumCh.appendChild(numChamber);

            var submit = document.createElement("input");
            submit.setAttribute("type", "submit");
            submit.setAttribute("value", "Submit");
            submit.setAttribute("id", "submitForm");
            submit.setAttribute("onClick", "SendQCM()");
            submit.className = "mt-5 btn btn-success";

            for (var Key in Json) {
               BuildFormField(Json[Key], form);
            }
            if (document.getElementById("QCM").childElementCount >= 1) {
                document.getElementById("QCM").removeChild(document.getElementById("formQCM"));
                document.getElementById("QCM").removeChild(document.getElementById("submitForm"));
                document.getElementById("QCM").removeChild(document.getElementById("divNumCh"));
            }
            //form.appendChild(submit);
            document.getElementById("QCM").appendChild(divNumCh);
            document.getElementById("QCM").appendChild(form);
            document.getElementById("QCM").appendChild(submit);
            $('#ModalQCM').modal();
        },
        error: function () {
            console.log('Erreur lors de la récupération des données !');
        }
    });

}

function BuildFormField(element, form) {

    var div = document.createElement("div");
    div.className = "table";
    var divDesc =  document.createElement("div");
    divDesc.className = "row";
    var description = document.createElement("p")
    description.className = "h5 ml-2 mt-3";
    description.innerHTML = element.description;
    if (element.type == "mandatory") {
        description.innerHTML = element.description + "*";
    }
    divDesc.appendChild(description);
    div.appendChild(divDesc);
    var divListRb = document.createElement("div");
    divListRb.className = "row";
    for (var Key in element.score) {
        var radiodiv = document.createElement("div");
        radiodiv.className = "ml-4 col-2 form-check";
        var elementLabel = document.createElement("label");
        elementLabel.innerHTML = element.score[Key][0];
        elementLabel.className = "form-check-label";
        elementLabel.setAttribute("for", element.name + "" + Key);

        var elementInput = document.createElement("input");
        elementInput.setAttribute("type", "radio");
        elementInput.className = "form-check-input";
        elementInput.setAttribute("id", element.name + "" + Key);
        elementInput.setAttribute("name", element.name);
        elementInput.setAttribute("value", element.score[Key][1]);

        radiodiv.appendChild(elementInput);
        radiodiv.appendChild(elementLabel);

        divListRb.appendChild(radiodiv);
    }
    div.appendChild(divListRb);

    form.appendChild(div);

}

function SendQCM() {
    $.ajax({
        url: "/api/QCM",
        type: 'GET',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        success: function (result) {
            const Json = JSON.parse(result);
            var resultJson = [];
            var Status = true;
            for (var Key in Json) {
                GetFormData(Json[Key], resultJson, Key, Status);
                if (!Status) {
                    break;
                }
            }
            var StringTuple = JSON.stringify(resultJson);
            if (Status) {
                $('#ModalQCM').modal('hide');
                $.ajax({
                    url: "/withings_evt",
                    type: "POST",
                    //contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                    data: {
                        appli: 888,
                        chambre: document.getElementById("numChamber").value,
                        type: 'Questionnaire',
                        values: StringTuple,
                        userId: document.getElementById("idUser").value
                    },
                    success: function (result) {
                        console.log(result);
                    },
                    error: function () {
                        console.log('Erreur lors de la récupération des données !');
                    }
                })

            }
        },
        error: function () {
            console.log('Erreur lors de la récupération des données !');
        }
    });

}

function GetFormData(element, resultJson, key, Status) {
    var ElementValue;
    for (var Key in element.score) {
        if (document.getElementById(element.name + "" + Key).checked) {
            ElementValue = [parseInt(key), parseInt(document.getElementById(element.name + "" + Key).value)];
        }
    }
    if (ElementValue == null && element.type != "mandatory") {

    } else if (ElementValue == null && element.type == "mandatory") {

        alert("Le champ obligatoire " + element.name + " n'est pas rempli.");
        throw ("Le champ obligatoire " + element.name + " n'est pas rempli.");
        Status = false;

    } else {
        resultJson.push(ElementValue);
    }

}