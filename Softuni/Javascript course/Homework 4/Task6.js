function usernames(input) {
    let uniqueUsernames = {};

    input.forEach(name => {
        if(!uniqueUsernames[name]) {
            uniqueUsernames[name] = true;
        }
    });

    let sortedUsernames = Object.keys(uniqueUsernames).sort((name, otherName) => name.length - otherName.length || name.localeCompare(otherName));
    console.log(sortedUsernames.join('\n'));
}

usernames([
    'Ashton',
    'Kutcher',
    'Ariel',
    'Lilly',
    'Keyden',
    'Aizen',
    'Billy',
    'Braston'
]);