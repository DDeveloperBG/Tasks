function printEveryNthElement(inputArray) {
    let elementsCount = inputArray.length - 1; // Calculate elements count
    let n = Number(inputArray[elementsCount]); // Take n, with it will be find every n-th element

    for (let i = 0; i < elementsCount; i += n) { // Go through the n-th elements
        console.log(inputArray[i]); // Print the nth element
    }
}

printEveryNthElement(['1', '2', '3', '2']); // Test with example data