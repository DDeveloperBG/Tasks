function magicMatrices(matrix) {
    let lastSum = undefined;
    let matrixIsMagic = true;

    for (let column = 0; column < matrix[0].length; column++) {
        let currentSum = 0;
        for (let row = 0; row < matrix.length; row++) {
            currentSum += matrix[row][column];
        }

        if (lastSum === undefined) {
            lastSum = currentSum;
        }
        else if (lastSum !== currentSum) {
            matrixIsMagic = false;
            break;
        }
    }

    console.log(matrixIsMagic)
}

magicMatrices([[4, 5, 6],
               [6, 5, 4],
               [5, 5, 5]]);