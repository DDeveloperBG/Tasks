function solve() {
   let tableRowElements = Array.from(document.querySelectorAll('tbody tr'));
   this.lastClickedElement = undefined;

   tableRowElements.forEach(element => element.addEventListener('click', clickOnRow.bind(window)));

   function clickOnRow(e) {
      if (this.lastClickedElement) {
         let check = e.currentTarget.style.backgroundColor;
         this.lastClickedElement.style.removeProperty('background-color');

         if (check) {
            return;
         }
      }

      e.currentTarget.style.backgroundColor = '#413f5e';
      this.lastClickedElement = e.currentTarget;
   }
}
