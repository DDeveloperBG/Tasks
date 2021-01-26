function solve() {
   let imgElements = Array.from(document.getElementsByTagName('img'));
   let halfOfElements = imgElements.length / 2;

   imgElements.forEach((element, index) => {
      if (index < halfOfElements) element.setAttribute('side', 'top');
      else element.setAttribute('side', 'bottom');

      element.addEventListener('click', onImgClick);
   });
}

let lastCard;

function onImgClick(e) {
   e.target.src = 'images/whiteCard.jpg';

   let element = 0;
   if (e.target.getAttribute('side') === 'bottom') {
      element = 2;
   }

   let resultElement = document.getElementById('result').children[element];
   resultElement.textContent = e.target.name;

   if (lastCard) {
      document.getElementById('result').children[0].textContent = '';
      document.getElementById('result').children[2].textContent = '';

      let cards = [e.target, lastCard];
      let values = [e.target.name, lastCard.name];

      cards = chooseWinner(values, cards);
      document.getElementById('history').textContent += `[${cards[0].name} vs ${cards[1].name}] `;
      applyStyleOfWinnerAndLosser(cards);

      lastCard = null;
   } else {
      lastCard = e.target;
   }

   e.target.removeEventListener('click', onImgClick);
}

function chooseWinner(values, cards) {
   if (Number(values[0]) > Number(values[1])) {
      return [cards[0], cards[1]];
   } else {
      return [cards[1], cards[0]];
   }
}

function applyStyleOfWinnerAndLosser(cards) {
   cards[0].style.border = "2px solid green";
   cards[1].style.border = "2px solid red";
}