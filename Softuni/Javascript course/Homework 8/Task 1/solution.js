function sort(array, type) {
    if (type == 'asc') {
        array = array.sort((a, b) => a - b);
    } else {
        array = array.sort((a, b) => b - a);
    }

    return array;
}

sort([14, 7, 17, 6, 8], 'asc'); // [6, 7, 8, 14, 17]