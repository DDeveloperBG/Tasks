function ticTakToeGame(playersMoves) {
    const dashboardSize = 3;
    const firstPlayerMark = 'X';
    const secondPlayerMark = 'O';
    const dashboard = [[false, false, false],
    [false, false, false],
    [false, false, false]];
    let currentPlayerMark = firstPlayerMark;

    for (let i = 0; i < playersMoves.length; i++) {
        let [row, column] = playersMoves[i].split(' ').map(a => Number(a));

        if (dashboard[row][column]) {
            console.log('This place is already taken. Please choose another!');
            continue;
        }

        dashboard[row][column] = currentPlayerMark;

        let somebodyWon = checkIfGameEnded(dashboard, currentPlayerMark, dashboardSize);

        if (somebodyWon) {
            console.log(`Player ${currentPlayerMark} wins!`);
            break;
        }

        let nobodyWin = !dashboard.some(row => row.some(column => !column));

        if (nobodyWin) {
            console.log('The game ended! Nobody wins :(');
            break;
        }

        if (currentPlayerMark === firstPlayerMark) currentPlayerMark = secondPlayerMark;
        else currentPlayerMark = firstPlayerMark;
    }

    console.log(dashboard.map(row => row.join('\t')).join('\n'));

    function checkIfGameEnded(dashboard, currentPlayerMark, dashboardSize) {
        let playerWins = false;

        for (let currentRow = 0; currentRow < dashboardSize; currentRow++) {
            let rowWins = true;

            for (let currentColumn = 0; currentColumn < dashboardSize; currentColumn++) {
                if (dashboard[currentRow][currentColumn] != currentPlayerMark) {
                    rowWins = false;
                }
            }

            if (rowWins) {
                playerWins = true;
                break;
            }
        }

        if (playerWins) return true;

        for (let currentColumn = 0; currentColumn < dashboardSize; currentColumn++) {
            let columnWins = true;

            for (let currentRow = 0; currentRow < dashboardSize; currentRow++) {
                if (dashboard[currentRow][currentColumn] != currentPlayerMark) {
                    columnWins = false;
                }
            }

            if (columnWins) playerWins = true;
        }

        if (playerWins) return true;

        let diagonalWins = true;
        for (let diagonal = 0; diagonal < dashboardSize; diagonal++) {
            if (dashboard[diagonal][diagonal] != currentPlayerMark) {
                diagonalWins = false;
                break;
            }
        }

        if (diagonalWins) return true;

        diagonalWins = true;
        let currentRow = dashboardSize - 1;
        for (let currentColumn = 0; currentColumn < dashboardSize; currentColumn++, currentRow--) {
            if (dashboard[currentRow][currentColumn] != currentPlayerMark) {
                diagonalWins = false;
                break;
            }
        }

        if (diagonalWins) return true;
        else return false;
    }
}

ticTakToeGame(["0 1",
    "0 0",
    "0 2",
    "2 0",
    "1 0",
    "1 2",
    "1 1",
    "2 1",
    "2 2",
    "0 0"]);