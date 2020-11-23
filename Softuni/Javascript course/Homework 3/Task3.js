function cappyJuice(input) {
    class Juice {
        constructor(name, quantity) {
            this.name = name;
            this.quantity = quantity;
        }
    }

    let allJuices = [];
    let bottledJuices = [];

    let juicesData = input.map(juice => juice.split(' => '));

    for (let juice of juicesData) {
        let [name, quantity] = juice;
        quantity = Number(quantity);

        let juiceIndex = allJuices.findIndex(juice => juice.name == name);
        if (juiceIndex != -1) {
            if (allJuices[juiceIndex].quantity < 1000 && allJuices[juiceIndex].quantity + quantity >= 1000) {
                bottledJuices.push(allJuices[juiceIndex]);
            }

            allJuices[juiceIndex].quantity += quantity;
        }
        else {
            allJuices.push(new Juice(name, quantity));

            if (quantity >= 1000) {
                bottledJuices.push(allJuices[allJuices.length - 1]);
            }
        }
    }

    console.log(bottledJuices.map(juice => juice.name + " => " + Math.trunc(juice.quantity / 1000)).join('\n'));
}

cappyJuice(['Orange => 2000',
    'Peach => 1432',
    'Banana => 450',
    'Peach => 600',
    'Strawberry => 549']);