function getArticleGenerator(articles) {
    let index = 0;
    let contentElement = document.getElementById('content');

    return () => {
        if (index < articles.length) {
            let newArticle = document.createElement('article');
            newArticle.textContent = articles[index];
            contentElement.appendChild(newArticle);
            index++;
        }
    };
}