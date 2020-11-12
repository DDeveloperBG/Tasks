function addAndRemoveElements(inputArray) {
    let result = [];

    for (let i = 0; i < inputArray.length; i++) {
        switch (inputArray[i]) {
            case 'add': result.push(i + 1); break;
            case 'remove': result.pop(); break;
        }
    }

    console.log(result.length > 0 ? result.join('\n') : 'Empty');
}

addAndRemoveElements(['add', 'add', 'remove', 'add', 'add']);