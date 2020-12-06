function countWordsInText(text) {
    let allWords = text[0].match(/[\w_1-9]+/g);
    let uniqueWords = {};

    allWords.forEach(word => {
        if(!uniqueWords[word]) {
            uniqueWords[word] = 0;
        }

        uniqueWords[word]++;
    });

    console.log(JSON.stringify(uniqueWords));
}

countWordsInText('Far too slow, you\'re far too slow.');