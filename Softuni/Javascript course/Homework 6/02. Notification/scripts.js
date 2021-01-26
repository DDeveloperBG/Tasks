function notify(message) {
    let notificationElement = document.getElementById('notification');
    notificationElement.textContent = message;
    notificationElement.style.display = 'block';

    let interval = setInterval(() => {
        notificationElement.style.display = 'none';
        clearInterval(interval);
    }, 2000);
}