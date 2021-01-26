function attachEventsListeners() {
    let mainElement = document.getElementsByTagName('main')[0];
    mainElement.addEventListener('click', onClickOfButton);
}

function onClickOfButton(e) {
    if (e.target.attributes['type']?.value === 'button') {
        let inputTime = Number(e.target.parentElement.children[1].value);
        let days, hours, minutes, seconds;

        switch (e.target.attributes['id'].value) {
            case 'daysBtn':
                days = inputTime;

                hours = days * 24;
                minutes = hours * 60;
                seconds = minutes * 60;
                setTimeValues(null, hours, minutes, seconds);
                break;
            case 'hoursBtn':
                hours = inputTime;

                days = Math.round(hours / 24);
                minutes = hours * 60;
                seconds = minutes * 60;
                setTimeValues(days, null, minutes, seconds);
                break;
            case 'minutesBtn':
                minutes = inputTime;

                hours = Math.round(minutes / 60);
                days = Math.round(hours / 24);
                seconds = minutes * 60;
                setTimeValues(days, hours, null, seconds);
                break;
            case 'secondsBtn':
                seconds = inputTime;

                minutes = Math.round(seconds / 60);
                hours = Math.round(minutes / 60);
                days = Math.round(hours / 24);
                setTimeValues(days, hours, minutes, null);
                break;
        }
    }
}

function setTimeValues(days, hours, minutes, seconds) {
    if (days != null) document.querySelector('input[id=days]').value = days;
    if (hours != null) document.querySelector('input[id=hours]').value = hours;
    if (minutes != null) document.querySelector('input[id=minutes]').value = minutes;
    if (seconds != null) document.querySelector('input[id=seconds]').value = seconds;
}