function findGreatestCommonDivisor(number1, number2) {
    while (number1 > number2 || number2 > number1) { // Repeat until they become equal
        if (number1 > number2) { // If one number is bigger, subtract from the bigger smaller
            number1 -= number2;
        }
        else {
            number2 -= number1;
        }
    }

    console.log(number1); // Print result
}

findGreatestCommonDivisor(2154, 458); // Test with example data