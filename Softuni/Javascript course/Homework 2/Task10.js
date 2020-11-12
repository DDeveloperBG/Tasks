function orbit(input) {
    let [rowSize, columnSize, startRow, startColumn] = input;
    let matrix = [];
    
    for (let row = 0; row < rowSize; row++) {
        matrix[row] = [];
        for (let column = 0; column < columnSize; column++) {
            matrix[row][column] = Math.max(Math.abs(row - startRow), Math.abs(column - startColumn)) + 1;
        }
    }

    console.log(matrix.map(row => row.join(' ')).join('\n'));
}

orbit([5, 5, 4, 4]);