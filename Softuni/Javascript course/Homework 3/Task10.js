class Stringer {
    constructor(innerString, innerLength) {
        this.innerString = innerString;
        this.innerLength = innerLength;
    }

    increase(length) {
        this.innerLength += length;
    }

    decrease(length) {
        this.innerLength -= length;

        if (this.innerLength < 0) this.innerLength = 0;
    }

    toString() {
        let toughtLength = this.innerLength;
        let realLength = this.innerString.length;
        let addDots = false;

        if (toughtLength < realLength) {
            realLength -= toughtLength;
            addDots = true;
        }

        if (toughtLength <= 0) {
            realLength = 0;
            addDots = true;
        }

        return this.innerString.slice(0, realLength) + (addDots ? '...' : '');
    }
}

function test() {
    let test = new Stringer("Test", 5);
    console.log(test.toString()); // Test
    
    test.decrease(3);
    console.log(test.toString()); // Te...
    
    test.decrease(5);
    console.log(test.toString()); // ...
    
    test.increase(4); 
    console.log(test.toString()); // Test
    
}

test();