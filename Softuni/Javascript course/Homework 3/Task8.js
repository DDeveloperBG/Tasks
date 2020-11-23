function tickets(ticketsRawData, sortingCriteria) {
    class Ticket {
        constructor(destination, price, status) {
            this.destination = destination;
            this.price = price;
            this.status = status;
        }
    }

    function CompareDestinations(ticketA, ticketB) {
        return ticketA.destination.localeCompare(ticketB.destination);
    }

    function CompareStatus(ticketA, ticketB) {
        return ticketA.status.localeCompare(ticketB.status);
    }

    function ComparePrices(ticketA, ticketB) {
        return ticketA.price - ticketB.price;
    }

    let ticketsData = ticketsRawData.map(row => row.split('|'));
    let tickets = [];

    for (let ticketData of ticketsData) {
        let [destination, price, status] = ticketData;
        price = Number(price);

        tickets.push(new Ticket(destination, price, status));
    }

    if (sortingCriteria == 'destination') {
        tickets.sort(CompareDestinations);
    }
    else if (sortingCriteria == 'price') {
        tickets.sort(ComparePrices);
    }
    else {
        tickets.sort(CompareStatus);
    }

    return tickets;
}

tickets(['Philadelphia|94.20|available',
    'New York City|95.99|available',
    'New York City|95.99|sold',
    'Boston|126.20|departed'],
    'price');