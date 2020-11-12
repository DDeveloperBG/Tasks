function timeToWalk(steps, stepSize, speed) {
    stepSize /= 1000; // Make step size from meters to km

    let distance = steps * stepSize; // Calculate distance to university
    let time = distance / speed; // Calculate raw time to university
    time += Math.floor(distance * 1000 / 500) / 60; // Add 1 minute of every 500 meters for rest

    let hours = Math.trunc(time); // Take hours from result time
    time = (time - hours) * 60; // Remove received hours and make time into minutes 

    let minutes = Math.trunc(time); // Take minutes from result time
    time = (time - minutes) * 60; // Remove received minutes and make time into seconds

    let seconds = Math.round(time); // Take seconds from result time

    if (hours < 10) { // Add leading zero if hours are smaller than 10
        hours = '0' + hours; // Add zero before the hours
    }

    if (minutes < 10) { // Add leading zero if minutes are smaller than 10
        minutes = '0' + minutes; // Add zero before the hours
    }

    if (seconds < 10) { // Add leading zero if seconds are smaller than 10
        seconds = '0' + seconds; // Add zero before the hours
    }

    console.log(`${hours}:${minutes}:${seconds}`); // Print result
}

timeToWalk(2564, 0.70, 5.5); // Test with example data