function jsonToHTMLTable(input) {
    let parsedObjects = input.map(row => JSON.parse(row));
    let htmlTable = '<table>';

    for (let objectInd = 0; objectInd < parsedObjects.length; objectInd++) {
        htmlTable += '\n\t<tr>';

        for (let objectValue of Object.values(parsedObjects[objectInd])) {
            htmlTable += '\n\t\t<td>' + objectValue + '</td>';
        }

        htmlTable += '\n\t</tr>';
    }

    htmlTable += '\n</table>';

    console.log(htmlTable);
}

jsonToHTMLTable([
    '{"name":"Pesho","position":"Promenliva","salary":100000}',
    '{"name":"Teo","position":"Lecturer","salary":1000}',
    '{"name":"Georgi","position":"Lecturer","salary":1000}']);