function create(words) {
   let contentElement = document.getElementById('content');
   contentElement.addEventListener('click', onClickShowDivElement);
   words.forEach(word => {
      let divElement = document.createElement('div');
      let paragraphElement = document.createElement('p');
      paragraphElement.textContent = word;
      paragraphElement.style.display = 'none';

      divElement.appendChild(paragraphElement);
      contentElement.appendChild(divElement);
   });
}

function onClickShowDivElement(e) {
   if (e.target.tagName == 'DIV') {
      e.target.firstChild.style.display = 'block';
   }
}