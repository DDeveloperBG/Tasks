function getValuesInfo() {
    let values = Array.from(arguments);
    let valuesTypes = values.map(value => typeof value);
    let typesAndTheirCount = {};

    valuesTypes.forEach(type => {
        if (!typesAndTheirCount[type]) {
            typesAndTheirCount[type] = 0;
        }
        typesAndTheirCount[type]++;
    });

    values.map((val, index) => `${valuesTypes[index]}: ${val}`).
        forEach(row => console.log(row));

    Object.entries(typesAndTheirCount).
        sort((a, b) => b[1] - a[1]).
        map(typeAndCount => `${typeAndCount[0]} = ${typeAndCount[1]}`).
        forEach(row => console.log(row));
}

getValuesInfo('cat', 42, 52, function () { console.log('Hello world!'); });

/*
string: cat
number: 42
number: 52
function: function () { console.log('Hello world!'); }
number = 2
string = 1
function = 1
*/