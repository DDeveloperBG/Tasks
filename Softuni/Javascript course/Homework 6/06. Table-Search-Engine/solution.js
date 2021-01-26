function solve() {
    let results = [];
    let buttonElement = document.getElementById('searchBtn');

    buttonElement.addEventListener('click', () => {
        removeLastResultsEffects(results);
        let wantedText = getWantedText();
        results = getElementsPassingRequirement(wantedText);
        applyEffectsOnResults(results);
    });
}

function removeLastResultsEffects(results) {
    results.forEach(element => {
        element.parentElement.removeAttribute('class');
    });
}

function getWantedText() {
    let wantedTextFieldElement = document.getElementById('searchField');
    let wantedText = wantedTextFieldElement.value;
    wantedTextFieldElement.value = '';
    return wantedText;
}

function getElementsPassingRequirement(wantedText) {
    let elements = Array.from(document.getElementsByTagName('tbody')[0].getElementsByTagName('td'));
    let results = [];
    elements.forEach(element => {
        if (element.textContent.includes(wantedText)) {
            results.push(element);
        }
    });
    return results;
}

function applyEffectsOnResults(results) {
    results.forEach(element => {
        element.parentElement.setAttribute('class', 'select');
    });
}