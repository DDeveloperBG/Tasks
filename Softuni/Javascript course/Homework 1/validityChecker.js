function validityChecker(pointsCordinates) {
    function calculateAndValidatePointsDistance(x1, y1, x2, y2) { // Function calculating distance from one point to another, checks if result value is valid and prints result
        let pointsDistance = Math.sqrt((x1 - x2) ** 2 + (y1 - y2) ** 2); // Calculate points distance
        let pointsDistanceStatus = 'valid'; // Value indicating distance validnes

        if (!Number.isInteger(pointsDistance)) { // Check if distance is valid
            pointsDistanceStatus = 'invalid'; // If not, set pointsDistanceStatus to 'invalid'
        }

        console.log(`{${x1}, ${y1}} to {${x2}, ${y2}} is ${pointsDistanceStatus}`); // Print result
    }

    calculateAndValidatePointsDistance(pointsCordinates[0], pointsCordinates[1], 0, 0); // Call function for first cordinates and (0, 0)
    calculateAndValidatePointsDistance(pointsCordinates[2], pointsCordinates[3], 0, 0); // Call function for second cordinates and (0, 0)
    calculateAndValidatePointsDistance(pointsCordinates[0], pointsCordinates[1], pointsCordinates[2], pointsCordinates[3]); // Call function for first and second cordinates
}

validityChecker([2, 1, 1, 1]); // Test with example data