function scoreToHTML(input) {
    const NewLineSign = '\n';
    const SpaceSign = '   ';
    const Column1Name = 'name';
    const Column2Name = 'score';

    let arrayOfObjects = JSON.parse(input);
    let result = '<table>' + NewLineSign;
    result += SpaceSign + `<tr><th>${Column1Name}</th><th>${Column2Name}</th></tr>` + NewLineSign;

    arrayOfObjects.forEach(object => {
        result += SpaceSign + '<tr>';

        let escapedName = object[Column1Name];

        escapedName = escapedName.split('&').join('&amp;');
        escapedName = escapedName.split('<').join('&lt;');
        escapedName = escapedName.split('>').join('&gt;');
        escapedName = escapedName.split('\"').join('&quot;');
        escapedName = escapedName.split('\'').join('&#39;');

        result += '<td>' + escapedName + '</td>';
        result += '<td>' + object[Column2Name] + '</td>';

        result += '</tr>' + NewLineSign;
    });

    result += '</table>';

    console.log(result);
}

scoreToHTML(['[{"name":"Pesho","score":479},{"name":"Gosho","score":205}]']);