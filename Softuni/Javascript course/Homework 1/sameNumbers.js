function sameNumbers(number) {
    let areTheSame = true; // Value for checking if number digits are equal
    let lastDigit = number % 10; // Take last digit
    let sum = 0; // Value for sum of number digits

    while (number > 0) { // Go through all digits
        let currentDigit = number % 10; // Take last digit
        number /= 10; // Remove last digit
        number = Math.trunc(number); // Rount up to remove last digit
        sum += currentDigit; // Add curent digit to sum

        if (areTheSame && lastDigit != currentDigit) // Check if current and previous values are equal
        {
            areTheSame = false; // Indicate that values differ
        }
    }

    console.log(areTheSame); // Print result for if number digits are equal
    console.log(sum); // Print number digits sum
}

sameNumbers(22222); // Test with example data