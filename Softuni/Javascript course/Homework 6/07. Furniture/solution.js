function solve() {
  let generateButton = document.querySelector('button:first-of-type');
  let generateTextArea = document.querySelector('textarea:first-of-type');
  generateButton.addEventListener('click', () => {
    let furniture = JSON.parse(generateTextArea.value);
    appendFurnitureElements(furniture);
  });

  let buyButton = document.querySelector('button:last-of-type');
  let buyTextArea = document.querySelector('textarea:last-of-type');
  buyButton.addEventListener('click', () => {
    buyTextArea.textContent = buyFurniture();
  });
}

function appendFurnitureElements(furniture) {
  let templateFurnitureElement = document.querySelector('tbody tr:first-of-type');
  let parentElement = templateFurnitureElement.parentElement;

  furniture.forEach(element => {
    let currentFurniture = templateFurnitureElement.cloneNode(true);
    currentFurniture.children[0].children[0].src = element['img'];
    currentFurniture.children[1].children[0].textContent = element['name'];
    currentFurniture.children[2].children[0].textContent = element['price'];
    currentFurniture.children[3].children[0].textContent = element['decFactor'];
    currentFurniture.children[4].children[0].removeAttribute('disabled');
    parentElement.appendChild(currentFurniture);
  });
}

function buyFurniture() {
  let resultValues = getResultValues();

  let result = 'Bought furniture: ' + resultValues[0] + '\n';
  result += 'Total price: ' + resultValues[1] + '\n';
  result += 'Average decoration factor: ' + resultValues[2];

  return result;
}

function getResultValues() {
  let furnitureElements = Array.from(document.querySelectorAll('input[type=checkbox]')).filter(element => element.checked == true).map(element => element.parentElement.parentElement);
  let furnitureNames = furnitureElements.map(element => element.children[1].children[0].textContent).join(', ');
  let reducer = (accumulator, currentValue) => accumulator + currentValue;
  let totalPrice = furnitureElements.map(element => Number(element.children[2].children[0].textContent))
    .reduce(reducer, 0);
  let averageDecFactor = furnitureElements.map(element => Number(element.children[3].children[0].textContent))
    .reduce(reducer, 0) / furnitureElements.length;

  return [furnitureNames, totalPrice, averageDecFactor];
}