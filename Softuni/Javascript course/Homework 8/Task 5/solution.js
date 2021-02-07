function solve() {
    let recipes = {
        apple: {
            carbohydrate: 1,
            flavour: 2
        },
        lemonade: {
            carbohydrate: 10,
            flavour: 20
        },
        burger: {
            carbohydrate: 5,
            fat: 7,
            flavour: 3
        },
        eggs: {
            protein: 5,
            fat: 1,
            flavour: 1
        },
        turkey: {
            protein: 10,
            carbohydrate: 10,
            fat: 10,
            flavour: 10
        },
    };
    let products = { protein: 0, carbohydrate: 0, fat: 0, flavour: 0, };

    return function (command) {
        let commandParts = command.split(' ');

        switch (commandParts[0]) {
            case 'restock':
                let microelement = commandParts[1];
                products[microelement] += Number(commandParts[2]);
                return 'Success';

            case 'prepare':
                let recipe = commandParts[1], quantity = Number(commandParts[2]);
                let neededProducts = Object.entries(recipes[recipe]).map(product => [product[0], product[1] * quantity]);
                let exhaustedNutrient = neededProducts.find(product => !products[product[0]] || product[1] > products[product[0]]);

                if (!exhaustedNutrient) {
                    neededProducts.forEach(product => {
                        products[product[0]] -= product[1];
                    });
                    return 'Success';
                } else {
                    return `Error: not enough ${exhaustedNutrient[0]} in stock`;
                }

            case 'report':
                return `protein=${products.protein} carbohydrate=${products.carbohydrate} fat=${products.fat} flavour=${products.flavour}`;
        }
    };
}

let managmentFunction = solve();
managmentFunction('restock carbohydrate 10'); // Success
managmentFunction('restock flavour 10'); // Success
managmentFunction('prepare apple 1'); // Success
managmentFunction('restock fat 10'); // Success
managmentFunction('prepare burger 1'); // Success
managmentFunction('report'); // protein=0 carbohydrate=4 fat=3 flavour=5
