function lockedProfile() {
    let mainElement = document.getElementById('main');
    mainElement.addEventListener('click', (e) => {
        if (e.target.tagName == 'button'.toUpperCase()) {
            let parentElement = e.target.parentElement;
            let checkedElement = Array.from(parentElement.querySelectorAll('input[type=radio]')).find(element => element.checked);
            let hiddenElement = parentElement.querySelector('div[id$=HiddenFields]');
            if (checkedElement.value == 'unlock') {
                if (e.target.textContent == 'Show more') {
                    hiddenElement.style.display = 'block';
                    e.target.textContent = 'Hide it';
                } else {
                    hiddenElement.style.display = 'none';
                    e.target.textContent = 'Show more';
                }
            }
        }
    });
}