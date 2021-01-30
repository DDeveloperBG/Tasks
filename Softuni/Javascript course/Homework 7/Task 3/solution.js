class Hex {
    constructor(value) {
        this.value = value;
    }

    valueOf() {
        return this.value;
    }

    toString() {
        return '0x' + this.value.toString(16).toUpperCase();
    }

    plus(number) {
        let newHex;

        if (typeof number == 'object') {
            newHex = new Hex(this.value + number.valueOf());
        } else {
            newHex = new Hex(this.value + number);
        }

        return newHex;
    }

    minus(number) {
        let newHex;

        if (typeof number == 'object') {
            newHex = new Hex(this.value - number.valueOf());
        } else {
            newHex = new Hex(this.value - number);
        }

        return newHex;
    }

    parse(string) {
        return parseInt(string, 16);;
    }
}

let FF = new Hex(255);
console.log(FF.toString());
FF.valueOf() + 1 == 256;

let a = new Hex(10);
let b = new Hex(5);
console.log(a.plus(b).toString());
console.log(a.plus(b).toString() === '0xF');

// 0xFF
// 0xF
// true