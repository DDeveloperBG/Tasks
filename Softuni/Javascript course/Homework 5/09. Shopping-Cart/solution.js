function solve() {
   let products = {};
   let productAddButtonElements = Array.from(document.querySelectorAll('button.add-product'));
   let textAreaElement = document.getElementsByTagName('textarea');
   productAddButtonElements.forEach((buttonElement, index) => buttonElement.addEventListener('click', () => {
      let productElement = document.querySelector(`div.product:nth-of-type(${index + 2})`);
      let productName = productElement.querySelector('div.product-title').innerHTML;
      let productPrice = productElement.querySelector('div.product-line-price').innerHTML;
      if (!products[productName]) {
         products[productName] = [0, productPrice];
      }
      products[productName][0]++;
      textAreaElement.innerHTML += `Added ${productName} for ${productPrice} to the cart.\n`;
   }));
   let checkoutButtonElement = document.querySelector('button.checkout');
   checkoutButtonElement.addEventListener('click', () => {
      let result = 'You bought ';
      let totalPrice = 0;
      Object.entries(products).forEach(product => {
         result += product[0] + ' ';
         totalPrice += product[1][0] * product[1][1];
      });
      result += `for ${totalPrice.toFixed(2)}.`;
      let textAreaElement = document.getElementsByTagName('textarea')[0];
      textAreaElement.innerHTML += result;
      let buttonElements = Array.from(document.getElementsByTagName('button'));
      buttonElements.forEach(buttonElement => {
         buttonElement.disabled = true;
      });
   });
}