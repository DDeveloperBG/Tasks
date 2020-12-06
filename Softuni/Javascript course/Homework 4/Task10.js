let result = (function () {
    let nextID = 0;

    class Extensible {
        constructor() {
            this.id = nextID;
            nextID++;
        }

        extend(template) {
            Object.entries(template).forEach(prop => {
                let a = typeof prop[1];
                if (a === 'function') {
                    Extensible.prototype[prop[0]] = prop[1];
                } else {
                    this[prop[0]] = prop[1];
                }
            });
        }
    }

    return Extensible;
})()

function test() {
    let obj1 = new result();
    let obj2 = new result();
    let obj3 = new result();

    console.log(obj1.id);
    console.log(obj2.id);
    console.log(obj3.id);

    obj1.extend({ a: 2 });
    console.log(obj1.a);
    console.log(obj2.a);
}

test();