function solve() {
  let rightOptions = ['onclick', 'JSON.stringify()', 'A programming API for HTML and XML documents'];
  let pElements = Array.from(document.getElementsByTagName('p'));
  let sectionElements = Array.from(document.getElementsByTagName('section'));
  let correctAnswers = 0;
  pElements.forEach(pElement => {
    if (rightOptions.includes(pElement.innerHTML)) {
      pElement.setAttribute('isAnswer', true);
    }

    pElement.addEventListener('click', () => {
      if (pElement.hasAttribute('isAnswer')) {
        correctAnswers++;
      }
      let currentQuestionElement = pElement.parentElement.parentElement.parentElement.parentElement;
      currentQuestionElement.setAttribute('class', 'hidden');
      currentQuestionElement.nextElementSibling.removeAttribute('class');
      if (sectionElements.every(sectionElement => sectionElement.hasAttribute('class'))) {
        showResults();
      }
    });
  });

  function showResults() {
    let resultFieldElement = document.querySelector('li[class=results-inner] h1');
    if (sectionElements.length == correctAnswers) {
      resultFieldElement.innerHTML = "You are recognized as top JavaScript fan!";
    } else {
      resultFieldElement.innerHTML = correctAnswers;
    }
    resultFieldElement.parentElement.parentElement.style.display = 'block';
  }
}
