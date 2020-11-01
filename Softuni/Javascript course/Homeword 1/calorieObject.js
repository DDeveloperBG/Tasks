function calorieObject(inputValues) {
    let valuesInAppropriateFormat = '{ '; // Declare variable which would contain result and initialize it with opening sign

    for (let i = 0; i < inputValues.length; i++) { // Go through all values of input array
        if (i % 2 == 0) { // Check if i is even
            valuesInAppropriateFormat += inputValues[i] + ': '; // If i is even, add to result product + ': '
        }
        else {
            valuesInAppropriateFormat += inputValues[i] + (i + 1 < inputValues.length ? ', ' : ' '); // If i is odd, add to result calories per 100 grams and if curent element is last ' ', else ', '
        }
    }

    valuesInAppropriateFormat += '}'; // Finaly add closing sign '}'
    console.log(valuesInAppropriateFormat); // Print result
}

calorieObject(['Potato', '93', 'Skyr', '63', 'Cucumber', '18', 'Milk', '42']); // Test with example data