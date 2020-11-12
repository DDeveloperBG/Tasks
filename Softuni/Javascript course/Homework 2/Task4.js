function rotateArray(array) {
    let rotationsCount = Number(array.pop()) % array.length;

    for (let i = 0; i < rotationsCount; i++) {
        let lastElement = array.pop();
        array.unshift(lastElement);
    }

    console.log(array.join(' '));
}

rotateArray(['Banana', 'Orange', 'Coconut', 'Apple', '15']);