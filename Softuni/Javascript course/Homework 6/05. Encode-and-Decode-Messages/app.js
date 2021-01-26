function encodeAndDecodeMessages() {
    let containerElement = document.querySelector('div[id=container]');

    let decodeMessageElement = containerElement.querySelector('div:first-of-type');
    let decodeButtonElement = decodeMessageElement.querySelector('button');
    let decodeInputTextElement = decodeMessageElement.querySelector('textarea');

    let encodeMessageElement = containerElement.querySelector('div:last-of-type');
    let encodeInputTextElement = encodeMessageElement.querySelector('textarea');

    decodeButtonElement.addEventListener('click', () => {
        let inputText = decodeInputTextElement.value;
        decodeInputTextElement.value = '';

        inputText = Array.from(inputText).map(sign => String.fromCharCode(sign.charCodeAt() + 1)).join('');

        encodeInputTextElement.value = inputText;
    });

    let encodeButtonElement = encodeMessageElement.querySelector('button');

    encodeButtonElement.addEventListener('click', () => {
        let messageText = encodeInputTextElement.value;
        
        messageText = Array.from(messageText).map(sign => String.fromCharCode(sign.charCodeAt() - 1)).join('');

        encodeInputTextElement.value = messageText;
    });
}