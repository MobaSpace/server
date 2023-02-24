let allCanvas = document.querySelectorAll('div[id^="gauge"]');

/**
 * build gauge from data
 * @param {any} id
 * @param {any} value
 */
function buildgauge(id, value) {
	var g = new JustGage({
		id: id,
		value: value,
		min: 0,
		max: 100,
		relativeGaugeSize: true,
		gaugeWidthScale: 0.7,
		customSectors: [{
				color: "#FF1300",
				lo: 0,
				hi: 25
			},{
				color: "#EE8C36",
				lo: 25,
				hi: 50
			}, {
				color: "#B2F629",
				lo: 50,
				hi: 75
			}, {
				color: "#1BE50B",
				lo: 75,
				hi: 100
            }
		]
    })

}

for (let i = 0; i < allCanvas.length; i++) {

	buildgauge(allCanvas[i].getAttribute("id"), allCanvas[i].getAttribute("value"));

}

