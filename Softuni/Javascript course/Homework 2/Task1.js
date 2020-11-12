function printArrayWithGivenDelimiter(inputArray) {
    let elements = inputArray.slice(0, inputArray.length - 1); // Take elements from input array
    let delimiter = inputArray[inputArray.length - 1]; // Take delimiter from input array

    console.log(elements.join(delimiter)); // Print elements separated by delimiter
}

printArrayWithGivenDelimiter(['1', '2', '3', ' ']); // Test with example data