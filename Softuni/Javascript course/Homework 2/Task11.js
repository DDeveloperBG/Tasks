function spiralMatrix(rowsCount, columnsCount) {
    let maxNum = rowsCount * columnsCount;
    let currentNum = 1;
    let matrix = [];
    createMatrix(matrix, rowsCount, columnsCount);
    let currentRow = 0, currentColumn = 0;
    matrix[currentRow][currentColumn] = currentNum;
    currentNum++;

    while (currentNum <= maxNum) {
        [currentNum, currentColumn] = goRight(matrix, currentNum, currentRow, currentColumn + 1);
        if(endSpiral(currentNum, maxNum)) break;

        [currentNum, currentRow] = goDown(matrix, currentNum, currentRow + 1, currentColumn);
        if(endSpiral(currentNum, maxNum)) break;

        [currentNum, currentColumn] = goLeft(matrix, currentNum, currentRow, currentColumn - 1);
        if(endSpiral(currentNum, maxNum)) break;

        [currentNum, currentRow] = goUp(matrix, currentNum, currentRow - 1, currentColumn);
    }

    console.log(matrix.map(row => row.join(' ')).join('\n'));

    function createMatrix(matrix, rowsCount, columnsCount) {
        for (let row = 0; row < rowsCount; row++) {
            matrix[row] = [];
            for (let column = 0; column < columnsCount; column++) {
                matrix[row][column] = false;
            }
        }
    }

    function goRight(matrix, currentNum, row, column) {
        for (; column < matrix[0].length; column++, currentNum++) {
            if (matrix[row][column]) {
                break;
            }
            else {
                matrix[row][column] = currentNum;
            }
        }

        return [currentNum, column - 1];
    }

    function goDown(matrix, currentNum, row, column) {
        for (; row < matrix.length; row++, currentNum++) {
            if (matrix[row][column]) {
                break;
            }
            else {
                matrix[row][column] = currentNum;
            }
        }

        return [currentNum, row - 1];
    }

    function goLeft(matrix, currentNum, row, column) {
        for (; column > -1; column--, currentNum++) {
            if (matrix[row][column]) {
                break;
            }
            else {
                matrix[row][column] = currentNum;
            }
        }

        return [currentNum, column + 1];
    }

    function goUp(matrix, currentNum, row, column) {
        for (; row > -1; row--, currentNum++) {
            if (matrix[row][column]) {
                break;
            }
            else {
                matrix[row][column] = currentNum;
            }
        }

        return [currentNum, row + 1];
    }

    function endSpiral(currentNum, maxNum) {
        return currentNum > maxNum;
    }
}

spiralMatrix(7, 7);