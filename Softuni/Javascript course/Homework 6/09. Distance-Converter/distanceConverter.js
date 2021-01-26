function attachEventsListeners() {
    document.getElementById('convert').addEventListener('click', convert);
}

let distanceToMeter = {
    'km': 1000,
    'm': 1,
    'cm': 0.01,
    'mm': 0.001,
    'mi': 1609.34,
    'yrd': 0.9144,
    'ft': 0.3048,
};

function convert(e) {
    let inputValue = Number(document.getElementById('inputDistance').value);
    let inputType = document.getElementById('inputUnits').value;

    let mediumValue = inputValue * distanceToMeter[inputType];

    let outputType = document.getElementById('outputUnits').value;
    let outputValue = mediumValue / distanceToMeter[outputType];
    document.getElementById('outputDistance').value = outputValue;
}