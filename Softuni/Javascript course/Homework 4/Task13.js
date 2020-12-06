class Kitchen {
    constructor(budget) {
        this.budget = budget;

        this.menu = {};
        this.productsInStock = {};
        this.actionsHistory = [];
    }

    loadProducts(products) {
        products.forEach(product => {
            let [productName, productQuantity, productPrice] = product.split(' ');
            productQuantity = Number(productQuantity);
            productPrice = Number(productPrice);

            let message;
            if (this.budget >= productPrice) {
                this.budget -= productPrice;

                if (!this.productsInStock[productName]) {
                    this.productsInStock[productName] = 0;
                }

                this.productsInStock[productName] += productQuantity;
                message = `Successfully loaded ${productQuantity} ${productName}`;
            } else {
                message = `There was not enough money to load ${productQuantity} ${productName}`;
            }

            this.actionsHistory.push(message);
        });

        return this.actionsHistory.join('\n');
    }

    addToMenu(meal, neededProducts, price) {
        let message;
        if (!this.menu[meal]) {
            let products = neededProducts.map(productData => {
                let [name, quantity] = productData.split(' ');
                return { name, quantity };
            });

            this.menu[meal] = { products, price, neededProducts };
            message = `Great idea! Now with the ${meal} we have ${Object.keys(this.menu).length} meals in the menu, other ideas?`;
        } else {
            message = `The ${meal} is already in our menu, try something different.`;
        }

        this.actionsHistory.push(message);
        return message;
    }

    showTheMenu() {
        let meals = Object.entries(this.menu);

        if (meals.length > 0) {
            return meals.map(meal => `${meal[0]} - $ ${meal[1].price}`).join('\n') + '\n';
        } else {
            return "Our menu is not ready yet, please come later...";
        }
    }

    makeTheOrder(meal) {
        let message;
        if (!this.menu[meal]) {
            message = `There is not ${meal} yet in our menu, do you want to order something else?`;
        } else {
            let mealIsAvaliable = this.menu[meal].neededProducts.every(product => this.productsInStock[product.name] &&
                this.productsInStock[product.name] >= product.quantity);

            if (mealIsAvaliable) {
                this.menu[meal].neededProducts.forEach(product => {
                    this.productsInStock[product.name] -= product.quantity;

                    if (this.productsInStock[product.name] == 0) {
                        delete this.productsInStock[product.name];
                    }
                });
                this.budget += this.menu[meal].price;

                message = `Your order (${meal}) will be completed in the next 30 minutes and will cost you ${this.menu[meal].price}.`;
            } else {
                message = `For the time being, we cannot complete your order (${meal}), we are very sorry...`;
            }
        }

        this.actionsHistory.push(message);
        return message;
    }
}

function test() {
    let kitchen = new Kitchen(1000);

    console.log(kitchen.addToMenu('frozenYogurt', ['Yogurt 1', 'Honey 1', 'Banana 1', 'Strawberries 10'], 9.99));
    console.log(kitchen.addToMenu('Pizza', ['Flour 0.5', 'Oil 0.2', 'Yeast 0.5', 'Salt 0.1', 'Sugar 0.1', 'Tomato sauce 0.5', 'Pepperoni 1', 'Cheese 1.5'], 15.55));

    console.log(kitchen.showTheMenu());
}

test();