function roadRadar(inputArray) {
    let speed = inputArray[0]; // Seperate speed from input array
    let area = inputArray[1]; // Seperate area from input array

    switch (area) { // Check area and based on it call function check Speed with each speed limit
        case 'motorway': checkSpeed(speed, 130); break;
        case 'interstate': checkSpeed(speed, 90); break;
        case 'city': checkSpeed(speed, 50); break;
        case 'residential': checkSpeed(speed, 20); break;
    }

    function checkSpeed(speed, speedLimit) {
        let speedDifference = speed - speedLimit; // Calculate speed difference

        if (speedDifference > 0) { // Check if the speed limit has been violated
            if (speedDifference <= 20) { // If speed exceeds speed limit with 20 return 'speeding'
                console.log('speeding');
            }
            else if (speedDifference <= 40) { // If speed exceeds speed limit with 40 return 'excessive speeding'
                console.log('excessive speeding');
            }
            else { // If speed exceeds speed limit with value higher than 40 return 'reckless driving'
                console.log('reckless driving');
            }
        }
    }
}

roadRadar([40, 'city']); // Test with example data