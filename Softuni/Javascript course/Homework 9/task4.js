function result() {
    return {
        extend(template) {
            Object.keys(template).forEach(key => {
                if (typeof template[key] == 'function') {
                    Object.getPrototypeOf(this)[key] = template[key];
                } else {
                    this[key] = template[key];
                }
            });
        }
    };
}

let template = {
    extensionMethod: function () { },
}

let storingObject = result();
storingObject.extend(template);

if (Object.getPrototypeOf(storingObject).hasOwnProperty('extensionMethod')) {
    console.log('success');
}