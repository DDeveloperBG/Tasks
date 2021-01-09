function subtract() {
    let firstNum = Number(document.getElementById("firstNumber").value);
    let secondNum = Number(document.getElementById("secondNumber").value);
    let result = firstNum - secondNum;

    let resultElement = document.getElementById("result");
    resultElement.innerHTML = result.toString(); 
}