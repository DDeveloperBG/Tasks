class Rat {
    constructor(name) {
        this.name = name;
        this.currentRat = { name, unitedRats: [] };
    }

    unite(otherRat) {
        if (otherRat instanceof Rat) {
            this.currentRat.unitedRats.push(otherRat);
        }
    }

    getRats() {
        return this.currentRat.unitedRats;
    }

    toString() {
        let result = this.currentRat.name;

        if (this.currentRat.unitedRats.length > 0) {
            result += '\n' + this.currentRat.unitedRats.map(rat => '##' + rat.name).join('\n');
        }

        return result;
    }
}

function test() {
    let firstRat = new Rat("Peter");
    console.log(firstRat.toString()); // Peter

    console.log(firstRat.getRats()); // []

    firstRat.unite(new Rat("George"));
    firstRat.unite(new Rat("Alex"));
    console.log(firstRat.getRats());
    // [ Rat { name: 'George', unitedRats: [] },
    //  Rat { name: 'Alex', unitedRats: [] } ]

    console.log(firstRat.toString());
    // Peter
    // ##George
    // ##Alex    
}

test();