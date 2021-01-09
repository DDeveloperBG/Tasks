function toggle() {
    let buttonElement = document.getElementsByClassName("button")[0];
    let extraElement = document.getElementById("extra");

    if(extraElement.style.display === "none") {
        buttonElement.innerHTML = "Less";
        extraElement.style.display = "block";
    } else {
        buttonElement.innerHTML = "More";
        extraElement.style.display = "none";
    }
}