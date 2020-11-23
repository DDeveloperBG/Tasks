function storeCatalogue(productsData) {
    class ProductsGroup {
        constructor(name) {
            this.name = name;
            this.products = [];
        }
    }

    productsData = productsData.map(product => product.replace(' :', ':')).sort();
    let catalogue = [];

    for (let product of productsData) {
        let firstLetter = product[0];
        let productGroupIndex = catalogue.findIndex(group => group.name == firstLetter);
        
        if(productGroupIndex == -1) {
            let group = new ProductsGroup(firstLetter);
            group.products.push(product);

            catalogue.push(group);
        }
        else {
            catalogue[productGroupIndex].products.push(product);
        }
    }

    console.log(catalogue.map(group => group.name + '\n  ' + group.products.join('\n  ')).join('\n'));
}

storeCatalogue(['Appricot : 20.4',
    'Fridge : 1500',
    'TV : 1499',
    'Deodorant : 10',
    'Boiler : 300',
    'Apple : 1.25',
    'Anti-Bug Spray : 15',
    'T-Shirt : 10']);