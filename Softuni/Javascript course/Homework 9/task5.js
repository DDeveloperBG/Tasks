(function () {
    let stringPrototype = String.prototype;

    stringPrototype.ensureStart = function (str) {
        let baseStr = this.toString();

        if (!baseStr.startsWith(str)) {
            return str + baseStr;
        }
        return baseStr;
    };

    stringPrototype.ensureEnd = function (str) {
        let baseStr = this.toString();

        if (!baseStr.endsWith(str)) {
            return baseStr + str;
        }
        return baseStr;
    };

    stringPrototype.isEmpty = function () {
        return this == '';
    };

    stringPrototype.truncate = function (n) {
        let baseStr = this.toString();

        if (baseStr.length > n) {
            if (n < 4) {
                return '.'.repeat(n);
            } else if (baseStr.includes(' ')) {
                let lastSpaceIndex = -1;
                for (let i = 0; i <= n - 3; i++) {
                    if (baseStr[i] == ' ') lastSpaceIndex = i;
                }
                return baseStr.slice(0, lastSpaceIndex) + '...';
            } else {
                return baseStr.slice(0, baseStr.length - 3) + '...';
            }
        }
        return baseStr;
    };

    String.format = function (string) {
        let [, ...params] = arguments;

        params.forEach((param, index) => {
            let wantedPlaceholder = '{' + index + '}';
            string = string.replace(wantedPlaceholder, param);
        });

        return string;
    }
})();

let str = 'my string';

console.log(str.ensureStart('my') === 'my string'); // 'my' already present

str = str.ensureStart('hello ');
console.log(str === 'hello my string');

console.log(str.truncate(16) === 'hello my string'); // length is 15

str = str.truncate(14);
console.log(str === 'hello my...'); // length is 11

str = str.truncate(8);
console.log(str === 'hello...');

str = str.truncate(2);
console.log(str === '..');

str = String.format('The {0} {1} fox', 'quick', 'brown');
console.log(str === 'The quick brown fox');

str = String.format('jumps {0} {1}', 'dog');
console.log(str === 'jumps dog {1}'); // no parameter at 1