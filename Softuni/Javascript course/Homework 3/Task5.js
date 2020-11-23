function autoEngineeringCompany(carsData) {
    class CarBrand {
        constructor(brandName) {
            this.brandName = brandName;
            this.models = [];
        }
    }

    class CarModel {
        constructor(modelName, producedCars) {
            this.modelName = modelName;
            this.producedCars = producedCars;
        }
    }

    carsData = carsData.map(car => car.split(' | '));
    let register = [];

    for (let carData of carsData) {
        let [brand, model, producedCars] = carData;
        producedCars = Number(producedCars);

        let carBrandIndex = register.findIndex(carBrand => carBrand.brandName == brand);
        if (carBrandIndex == -1) {
            let carBrand = new CarBrand(brand);
            carBrand.models.push(new CarModel(model, producedCars));

            register.push(carBrand);
        }
        else {
            let carModelIndex = register[carBrandIndex].models.findIndex(car => car.modelName == model);

            if (carModelIndex == -1) {
                register[carBrandIndex].models.push(new CarModel(model, producedCars));
            }
            else {
                register[carBrandIndex].models[carModelIndex].producedCars += producedCars;
            }
        }
    }

    console.log(register.map(carBrand => carBrand.brandName + '\n' + carBrand.models.map(carModel => '###' + carModel.modelName + ' -> ' + carModel.producedCars).join('\n')).join('\n'));
}

autoEngineeringCompany(['Mercedes-Benz | 50PS | 123',
    'Mini | Clubman | 20000',
    'Mini | Convertible | 1000',
    'Mercedes-Benz | 60PS | 3000',
    'Hyunday | Elantra GT | 20000',
    'Mini | Countryman | 100',
    'Mercedes-Benz | W210 | 100',
    'Mini | Clubman | 1000',
    'Mercedes-Benz | W163 | 200']);