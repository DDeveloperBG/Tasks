function getFibonator() {
    this.last = 0;
    this.current = 0;

    return () => {
        let next = this.current + this.last;
        if (next == 0) next = 1;

        this.last = this.current;
        this.current = next;
        
        return next;
    };
}

let fib = getFibonator();
console.log(fib()); // 1
console.log(fib()); // 1
console.log(fib()); // 2
console.log(fib()); // 3
console.log(fib()); // 5
console.log(fib()); // 8
console.log(fib()); // 13