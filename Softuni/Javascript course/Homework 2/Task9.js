function diagonalAttack(input) {
    let matrix = input.map(row => row.split(' ').map(column => Number(column)));

    let firstDiagonalSum = 0;
    for (let diagonal = 0; diagonal < matrix.length; diagonal++) {
        firstDiagonalSum += matrix[diagonal][diagonal];
    }

    let secondDiagonalSum = 0;
    let column = matrix.length - 1;
    for (let row = 0; row < matrix.length; row++, column--) {
        secondDiagonalSum += matrix[row][column];
    }

    if (firstDiagonalSum === secondDiagonalSum) {
        let matrixSize = matrix.length - 1;
        for (let row = 0; row < matrix.length; row++) {
            for (let column = 0; column < matrix.length; column++) {
                if (column !== row && column !== matrixSize - row) {
                    matrix[row][column] = firstDiagonalSum;
                }
            }
        }

        console.log(matrix.map(row => row.join(' ')).join('\n'));
    }
    else {
        console.log(input.join('\n'));
    }
}

diagonalAttack(['5 3 12 3 1',
    '11 4 23 2 5',
    '101 12 3 21 10',
    '1 4 5 2 2',
    '5 22 33 11 1']);