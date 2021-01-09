function solve() {
    let inputElement = document.querySelector("input[type=text]");

    if (inputElement.value && /^[a-zA-Z]+$/.test(inputElement.value)) {
        let inputValue = inputElement.value[0].toUpperCase() + inputElement.value.slice(1).toLowerCase();

        let index = inputValue.charCodeAt(0) - 65;
        let liElements = document.getElementsByTagName("li");

        if (!liElements[index].innerHTML) {
            liElements[index].innerHTML = inputValue;
        } else {
            liElements[index].innerHTML += ", " + inputValue;
        }
    } else {
        let buttonElement = document.querySelector("button[type=button]");
        buttonElement.addEventListener("click", solve);
    }
}