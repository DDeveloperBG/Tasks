function cookingByNumbers(inputArray) {
    let number = Number.parseInt(inputArray[0]); // Parse start number from input array

    for (let i = 1; i < inputArray.length; i++) { // Go through all commands from input array
        switch (inputArray[i]) { // Check for what is current command
            case 'chop': number /= 2; break; // Divide the number by two
            case 'dice': number = Math.sqrt(number); break; // Square root of number
            case 'spice': number += 1; break; // Add 1 to number
            case 'bake': number *= 3; break; // Multiply number by 3
            case 'fillet': number *= 0.80; break; // Subtract 20% from number
        }

        console.log(number); // Print result of current operation
    }
}

cookingByNumbers(['32', 'chop', 'chop', 'chop', 'chop', 'chop']); // Test with example data