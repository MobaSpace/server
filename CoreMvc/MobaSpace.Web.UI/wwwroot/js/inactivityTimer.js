
var timeChangeWindows;
var timeDisconnected;

function startResetTime(disconnectTimer, changeWindows) {
    window.addEventListener('load', resetTimer(disconnectTimer, changeWindows), true);
    var Events = ['mousemove', 'mousedown', 'click', 'keypress', 'scroll', 'touchstart'];
    // all DOM events
    Events.forEach(function (event) {
        document.addEventListener(event, resetTimer(disconnectTimer, changeWindows), true);
    });
}

function alertSwitch() {
    if (!location.href.includes("Alarmes")) {
        location.href = '/Alarmes';
    }
}

function alertDisconnected() {
    document.getElementById('LogoutForm').submit();
}

function resetTimer(disconnectTimer, changeWindows){
    window.clearTimeout(timeChangeWindows);
    timeChangeWindows = window.setTimeout(alertSwitch, changeWindows);
    timeDisconnected = window.setTimeout(alertDisconnected, disconnectTimer)
}