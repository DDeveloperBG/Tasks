class List {
    constructor() {
        this.numbers = [];
        this.size = 0;
    }

    add(element) {
        let newIndex = this.size;

        for (let i = 0; i < this.size; i++) {
            if (this.numbers[i] > element) {
                newIndex = i;
                break;
            }
        }

        this.numbers.splice(newIndex, 0, element);
        this.size++;
    }

    remove(index) {
        if (index > -1 && index < this.size) {
            this.numbers.splice(index, 1);
            this.size--;
        }
    }

    get(index) {
        if (index > -1 && index < this.size) {
            return this.numbers[index];
        }
    }
}