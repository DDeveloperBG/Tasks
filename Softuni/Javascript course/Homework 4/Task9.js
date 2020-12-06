class CheckingAccount {
    constructor(clientId, email, firstName, lastName) {
        if (typeof clientId === 'string' && clientId.length == 6 && /^\d+$/.test(clientId)) {
            this.clientId = clientId;
        } else {
            throw new TypeError('Client ID must be a 6-digit number');
        }

        if (/[-A-Za-z0-9_.%]+@[-A-Za-z0-9_.%]+\.[A-Za-z]+/.test(email)) {
            this.email = email;
        } else {
            throw new TypeError('Invalid e-mail');
        }

        if (firstName.length >= 3 && firstName.length <= 20) {
            if (/^[a-zA-Z]+$/.test(firstName)) {
                this.firstName = firstName;
            } else {
                throw new TypeError('First name must contain only Latin characters');
            }
        } else {
            throw new TypeError('First name must be between 3 and 20 characters long');
        }

        if (lastName.length >= 3 && lastName.length <= 20) {
            if (/^[a-zA-Z]+$/.test(lastName)) {
                this.lastName = lastName;
            } else {
                throw new TypeError('Last name must contain only Latin characters');
            }
        } else {
            throw new TypeError('Last name must be between 3 and 20 characters long');
        }
    }
}

function test() {
    let acc = new CheckingAccount('423414', 'petkan@another.co.uk', 'Петкан', 'Draganov');
}

test();