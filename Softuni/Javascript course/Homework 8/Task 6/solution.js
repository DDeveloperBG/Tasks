function add(num) {
    function repeat(num2) {
        num += num2;
        return repeat;
    };

    repeat.toString = () => num;

    return repeat;
}

console.log(Number(add(1)(6)(-3))); // 4