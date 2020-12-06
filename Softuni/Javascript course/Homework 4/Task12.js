function gameOfEpicness(kingdomsData, battlesData) {
    let kingdoms = {};
    kingdomsData.forEach(kingdomData => {
        if (!kingdoms[kingdomData.kingdom]) {
            kingdoms[kingdomData.kingdom] = {};
        }

        if (!kingdoms[kingdomData.kingdom][kingdomData.general]) {
            kingdoms[kingdomData.kingdom][kingdomData.general] = { army: 0, wins: 0, losses: 0 };
        }

        kingdoms[kingdomData.kingdom][kingdomData.general].army += kingdomData.army;
    });

    battlesData.forEach(battleData => {
        let [attackingKingdom, attackingGeneral, defendingKingdom, defendingGeneral] = battleData;

        if (attackingKingdom !== defendingKingdom) {
            let attakingSideArmy = kingdoms[attackingKingdom][attackingGeneral].army;
            let defendingSideArmy = kingdoms[defendingKingdom][defendingGeneral].army;

            let winnerKingdom, winnerGeneral;
            let lostKingdom, lostGeneral;

            if (attakingSideArmy > defendingSideArmy) {
                winnerKingdom = attackingKingdom;
                winnerGeneral = attackingGeneral;

                lostKingdom = defendingKingdom;
                lostGeneral = defendingGeneral;
            } else if (attakingSideArmy < defendingSideArmy) {
                winnerKingdom = defendingKingdom;
                winnerGeneral = defendingGeneral;

                lostKingdom = attackingKingdom;
                lostGeneral = attackingGeneral;
            }
            else {
                return;
            }

            kingdoms[winnerKingdom][winnerGeneral].army = Math.floor(kingdoms[winnerKingdom][winnerGeneral].army * (110 / 100));
            kingdoms[winnerKingdom][winnerGeneral].wins++;

            kingdoms[lostKingdom][lostGeneral].army = Math.floor(kingdoms[lostKingdom][lostGeneral].army * (90 / 100));
            kingdoms[lostKingdom][lostGeneral].losses++;
        }
    });

    let sortedKingdoms = Object.entries(kingdoms).sort((currKingdome, nextKingdome) =>
        Object.values(nextKingdome[1]).reduce((wins, currGeneral) => wins + currGeneral.wins, 0) -
        Object.values(currKingdome[1]).reduce((wins, currGeneral) => wins + currGeneral.wins, 0) ||
        Object.values(currKingdome[1]).reduce((losses, currGeneral) => losses + currGeneral.losses, 0) -
        Object.values(nextKingdome[1]).reduce((losses, currGeneral) => losses + currGeneral.losses, 0) ||
        currKingdome[0].localeCompare(nextKingdome[0]));
    let kingdomeWinner = sortedKingdoms[0];
    kingdomeWinner[1] = Object.entries(kingdomeWinner[1]).sort((currGeneral, nextGeneral) => nextGeneral[1].army - currGeneral[1].army);

    console.log('Winner: ' + kingdomeWinner[0]);
    console.log(kingdomeWinner[1].map(general => `/\\general: ${general[0]}\n` +
        Object.entries(general[1]).map(status => `---${status[0]}: ${status[1]}`).join('\n')).
        join('\n'));
}

gameOfEpicness(
    [{ kingdom: "Maiden Way", general: "Merek", army: 5000 },
    { kingdom: "Stonegate", general: "Ulric", army: 4900 },
    { kingdom: "Stonegate", general: "Doran", army: 70000 },
    { kingdom: "YorkenShire", general: "Quinn", army: 0 },
    { kingdom: "YorkenShire", general: "Quinn", army: 2000 },
    { kingdom: "Maiden Way", general: "Berinon", army: 100000 }],

    [["YorkenShire", "Quinn", "Stonegate", "Ulric"],
    ["Stonegate", "Ulric", "Stonegate", "Doran"],
    ["Stonegate", "Doran", "Maiden Way", "Merek"],
    ["Stonegate", "Ulric", "Maiden Way", "Merek"],
    ["Maiden Way", "Berinon", "Stonegate", "Ulric"]]
);
