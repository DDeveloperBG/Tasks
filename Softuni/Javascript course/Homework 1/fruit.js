function buyFruit(fruit, weight, pricePerKg) {
    weight /= 1000; // Make weight form grams to kilograms
    let neededMoney = weight * pricePerKg; // Calculate price for wanted fruit with input weight
    neededMoney = neededMoney.toFixed(2); // Round price to second digit after zero
    weight = weight.toFixed(2); // Round weight to second digit after zero
    console.log(`I need $${neededMoney} to buy ${weight} kilograms ${fruit}.`); // Print result
}

buyFruit('orange', 2500, 1.80); // Test with example data