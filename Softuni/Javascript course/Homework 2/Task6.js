function sortArrayByTwoCriteria(array) {
    function comparingFunction(a, b) {
        if (a.length > b.length) return 1;
        else if (a.length < b.length) return -1;
        else return a.localeCompare(b);
    }

    array.sort(comparingFunction);
    console.log(array.join('\n'));
}

sortArrayByTwoCriteria(['alpha', 'beta', 'gamma']);