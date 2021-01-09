function solve() {
    let defaultOptionElement = document.querySelector('#selectMenuTo option');
    defaultOptionElement.removeAttribute('selected');

    let firstOptionElement = defaultOptionElement.cloneNode(true);
    firstOptionElement.value = 'binary';
    firstOptionElement.innerHTML = 'Binary';

    let secondOptionElement = defaultOptionElement.cloneNode(true);
    secondOptionElement.value = 'hexadecimal';
    secondOptionElement.innerHTML = 'Hexadecimal';

    defaultOptionElement.parentElement.appendChild(firstOptionElement);
    defaultOptionElement.parentElement.appendChild(secondOptionElement);

    defaultOptionElement.setAttribute('selected', 'selected');

    let convertButtonElement = document.getElementsByTagName('button')[0];
    convertButtonElement.addEventListener('click', () => {
        let selectElement = document.getElementById('selectMenuTo');
        let numberElement = document.getElementById('input');
        let resultElement = document.getElementById('result');
        let number = Number(numberElement.value);
        let result;

        if (selectElement.value == 'binary') {
            result = number.toString(2);
        } else {
            result = number.toString(16).toUpperCase();
        }

        resultElement.value = result;
    });
}