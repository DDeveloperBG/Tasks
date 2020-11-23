function heroicInventory(input) {
    class Hero {
        constructor(name, level, items) {
            this.name = name;
            this.level = level;
            this.items = items;
        }
    }

    input = input.map(row => row.split(' / '));
    let heroes = [];

    for (let heroData of input) {
        let heroName = heroData[0];
        let heroLevel = Number(heroData[1]);
        let heroItems = heroData.length > 2 ? heroData[2].split(', ') : [];

        let currentHero = new Hero(heroName, heroLevel, heroItems);
        heroes.push(currentHero);
    }

    console.log(JSON.stringify(heroes));
}

heroicInventory([
    'Isacc / 25',
    'Derek / 12 / BarrelVest, DestructionSword',
    'Hes / 1 / Desolator, Sentinel, Antara']);