function solve() {
    let buttonsElements = Array.from(document.getElementsByTagName('button'));
    buttonsElements.forEach(button => {
        let expressionFieldElement = document.getElementById('expressionOutput');

        if (button.innerHTML != '=' && button.innerHTML != 'C') {
            if (button.innerHTML == '+' || button.innerHTML == '-' || button.innerHTML == 'x' || button.innerHTML == '/') {
                button.addEventListener('click', () => {
                    expressionFieldElement.innerHTML += ' ' + button.innerHTML + ' ';
                });
            } else {
                button.addEventListener('click', () => {
                    expressionFieldElement.innerHTML += button.innerHTML;
                });
            }
        } else {
            let resultFieldElement = document.getElementById('resultOutput');

            if (button.innerHTML == '=') {
                button.addEventListener('click', () => {
                    let result = 0;
                    let splittedExpresion = expressionFieldElement.innerHTML.split(' ');
                    if (validateExpression(splittedExpresion)) {
                        let operation = '+';
                        splittedExpresion.forEach(numOrSign => {
                            let number = Number(numOrSign);
                            if (number) {
                                result = doOperation(operation, number, result);
                            }
                            else {
                                operation = numOrSign;
                            }
                        });
                    } else {
                        result = 'NaN';
                    }

                    resultFieldElement.innerHTML = result;
                });
            } else if (button.innerHTML == 'C') {
                button.addEventListener('click', () => {
                    expressionFieldElement.innerHTML = '';
                    resultFieldElement.innerHTML = '';
                });
            }
        }
    });

    function validateExpression(expression) {
        return Number(expression[expression.length - 1]);
    }

    function doOperation(operation, number, result) {
        switch (operation) {
            case '+':
                return result + number;
            case '-':
                return result - number;
            case 'x':
                return result * number;
            case '/':
                return result / number;
        }
    }
}