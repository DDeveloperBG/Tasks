function sortedList() {
    let array = [];

    return {
        add(elemenent) {
            let index = this.size;

            for (let i = 0; i < this.size; i++) {
                if (array[i] > elemenent) {
                    index = i;
                    break;
                }
            }

            array.splice(index, 0, elemenent);
            this.size++;
        },
        remove(index) {
            if (index > -1 && index < this.size) {
                array.splice(index, 1);
                this.size--;
            }
        },
        get(index) {
            if (index > -1 && index < this.size) {
                return array[index];
            }
        },
        size: 0,
    };
}

myList = sortedList();

// Generate random list of 20 numbers
var expectedArray = [];
for (let i = 0; i < 20; i++) {
    expectedArray.push(Math.floor(Math.random() * 200) - 100);
}

// Add to collection
for (let i = 0; i < expectedArray.length; i++) {
    myList.add(expectedArray[i]);
}

// Sort array
expectedArray.sort((a, b) => a - b);

// Compare with collection
for (let i = 0; i < expectedArray.length; i++) {
    if (myList.get(i) == expectedArray[i]) {
        console.log('test passed');
    } else {
        console.log('test' + i + 'doesn\'t passed');
    }
}