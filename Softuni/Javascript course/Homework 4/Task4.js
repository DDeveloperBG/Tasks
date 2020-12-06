function cityMarkets(input) {
    let productsData = input.map(product => product.split(' -> '));
    let productsAndProfit = {};

    productsData.forEach(productData => {
        let [town, product, businessData] = productData;
        businessData = businessData.split(' : ');

        let amountOfSales = Number(businessData[0]);
        let priceForOneUnit = Number(businessData[1]);

        if (!productsAndProfit[town]) {
            productsAndProfit[town] = [];
        }

        productsAndProfit[town].push({ product: product, price: amountOfSales * priceForOneUnit });
    });

    console.log(Object.entries(productsAndProfit).map(town => `Town - ${town[0]}\n` + 
                                                        town[1].map(product => `$$$${product.product} : ${product.price}`)
                                                               .join('\n'))
                                                 .join('\n'));
}

cityMarkets([
    'Sofia -> Laptops HP -> 200 : 2000',
    'Sofia -> Raspberry -> 200000 : 1500',
    'Sofia -> Audi Q7 -> 200 : 100000',
    'Montana -> Portokals -> 200000 : 1',
    'Montana -> Qgodas -> 20000 : 0.2',
    'Montana -> Chereshas -> 1000 : 0.3'
]);