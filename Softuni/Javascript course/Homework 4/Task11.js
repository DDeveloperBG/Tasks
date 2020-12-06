function arenaTier(arenaData) {
    arenaData.pop();
    arenaData = arenaData.map(gladiatorData => gladiatorData.split(' -> '));
    let gladiators = {};
    let gladiatorsTotalSkills = {};

    arenaData.forEach(gladiatorData => {
        if (gladiatorData.length == 3) {
            addGladiator(gladiators, gladiatorsTotalSkills, gladiatorData);
        } else {
            let [gladName1, gladName2] = gladiatorData[0].split(' vs ');
            fightBetweenGladiators(gladiators, gladiatorsTotalSkills, gladName1, gladName2);
        }
    });

    let sortedGladiators = Object.entries(gladiators).sort((currGlad, nextGlad) =>
        gladiatorsTotalSkills[nextGlad[0]] - gladiatorsTotalSkills[currGlad[0]] ||
        currGlad[0].localeCompare(nextGlad[0]));

    console.log(sortedGladiators.map(gladData => {
        let sortedTechniques = Object.entries(gladData[1]).sort((curr, next) => next[1] - curr[1] || curr[0].localeCompare(next[0]));

        let result = `${gladData[0]}: ${sortedTechniques.reduce((acc, skill) => acc + skill[1], 0)} skill\n`;
        result += sortedTechniques.map(technique => `- ${technique[0]} <!> ${technique[1]}`).join('\n');

        return result;
    }).join('\n'));

    function addGladiator(gladiators, gladiatorsTotalSkills, gladiatorData) {
        let [name, technique, skill] = gladiatorData;
        skill = Number(skill);

        if (!gladiators[name]) {
            gladiators[name] = {};
        }

        if (!gladiators[name][technique]) {
            if (!gladiatorsTotalSkills[name]) {
                gladiatorsTotalSkills[name] = 0;
            }

            gladiatorsTotalSkills[name] += skill;
            gladiators[name][technique] = skill;
        }
        else if (gladiators[name][technique] < skill) {
            gladiatorsTotalSkills[name] -= gladiators[name][technique];
            gladiatorsTotalSkills[name] += skill;

            gladiatorsTotalSkills[name] = skill
        }
    }

    function fightBetweenGladiators(gladiators, gladiatorsTotalSkills, gladName1, gladName2) {
        let gladiator1 = gladiators[gladName1];
        let gladiator2 = gladiators[gladName2];

        if (gladiator1 && gladiator2) {
            let glad1Techniques = Object.keys(gladiator1);
            let commonTechnique = glad1Techniques.find(technique => gladiator2[technique]);

            if (commonTechnique) {
                let glad1TotalSkills = gladiatorsTotalSkills[gladName1];
                let glad2TotalSkills = gladiatorsTotalSkills[gladName2];

                let lostGladiator = glad1TotalSkills > glad2TotalSkills ? gladName2 :
                    (glad1TotalSkills < glad2TotalSkills ? gladName1 : null);

                if (lostGladiator) {
                    delete gladiators[lostGladiator];
                }
            }
        }
    }
}

arenaTier([
    'Pesho -> Duck -> 400',
    'Julius -> Shield -> 150',
    'Julius -> Heal -> 150',
    'Gladius -> Heal -> 200',
    'Gladius -> Support -> 250',
    'Gladius -> Shield -> 250',
    'Pesho vs Gladius',
    'Gladius vs Julius',
    'Gladius vs Gosho',
    'Ave Cesar'
]);