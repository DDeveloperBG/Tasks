function processOrder(wantedCar) {
    let resultCar = { model: wantedCar.model };

    addEngine(resultCar, wantedCar);
    addCarriage(resultCar, wantedCar);
    addWheels(resultCar, wantedCar);

    return resultCar;

    function addEngine(resultCar, wantedCar) {
        let engines = {
            smallEngine: { power: 90, volume: 1800 },
            normalEngine: { power: 120, volume: 2400 },
            monsterEngine: { power: 200, volume: 3500 },
        };

        for (key in engines) {
            if (wantedCar.power <= engines[key].power) {
                resultCar.engine = engines[key];
                return;
            }
        }
    }

    function addCarriage(resultCar, wantedCar) {
        let carriages = {
            hatchback: { type: 'hatchback' },
            coupe: { type: 'coupe' },
        };

        let wantedCarriage = carriages[wantedCar.carriage];
        wantedCarriage.color = wantedCar.color;

        resultCar.carriage = wantedCarriage;
    }

    function addWheels(resultCar, wantedCar) {
        let wheelsSize = wantedCar.wheelsize;
        wheelsSize -= (wheelsSize % 2 == 0) ? 1 : 0; // make odd if needed

        resultCar.wheels = [wheelsSize, wheelsSize, wheelsSize, wheelsSize];
    }
}

let wantedCar = {
    model: 'VW Golf II',
    power: 90,
    color: 'blue',
    carriage: 'hatchback',
    wheelsize: 14,
};

console.log(processOrder(wantedCar));
;
/*{
    model: 'VW Golf II',
        engine: {
        power: 90,
        volume: 1800
    },
    carriage: {
        type: 'hatchback',
        color: 'blue'
    },
    wheels: [13, 13, 13, 13],
}*/