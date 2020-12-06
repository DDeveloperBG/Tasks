function uniqueSequences(input) {
    let sequences = input.map(array => JSON.parse(array));
    let uniqueSequences = [];

    sequences.forEach(sequence => {
        let seqIsUnique = true;
        uniqueSequences.forEach(uniqueSequence => {
            if (!seqIsUnique) return;

            let areEqual = true;
            if (uniqueSequence.length == sequence.length) {
                for (let i = 0; i < uniqueSequence.length; i++) {
                    if (!uniqueSequence.includes(sequence[i])) {
                        areEqual = false;
                        break;
                    }
                }

                if (areEqual) seqIsUnique = false;
            }
        });

        if (seqIsUnique) {
            let orderedSequence = sequence.sort((curr, next) => next - curr);

            uniqueSequences.push(orderedSequence);
        }
    });

    uniqueSequences = uniqueSequences.sort((curr, next) => curr.length - next.length);
    console.log(uniqueSequences.map(sequence => '[' + sequence.join(', ') + ']').join('\n'));
}

uniqueSequences([
    "[-3, -2, -1, 0, 1, 2, 3, 4]",
    "[10, 1, -17, 0, 2, 13]",
    "[4, -3, 3, -2, 2, -1, 1, 0]"
]);