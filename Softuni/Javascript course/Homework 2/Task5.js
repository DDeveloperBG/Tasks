function extractIncreasingSubsequence(array) {
    let lastNum = array[0];

    for (let i = 0; i < array.length; i++) 
    {
        if (array[i] >= lastNum) 
        {
            lastNum = array[i];
            console.log(array[i]);
        }
    }
}

extractIncreasingSubsequence([1, 3, 8, 4, 10, 12, 3, 2, 24]);