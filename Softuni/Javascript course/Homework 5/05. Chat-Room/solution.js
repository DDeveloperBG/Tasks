function solve() {
   let buttonElement = document.getElementById('send');
   buttonElement.addEventListener('click', () => {
      let message = document.getElementById('chat_input');

      if (message.value) {
         let newMessageElement = document.createElement('div');
         newMessageElement.setAttribute('class', 'message my-message');
         newMessageElement.innerHTML = message.value;

         let chatElement = document.getElementById('chat_messages');
         chatElement.appendChild(newMessageElement);

         message.value = "";
      }
   });
}