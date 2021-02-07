solution = function (argument) {
    switch (argument) {
        case 'upvote': this.upvotes++; break;

        case 'downvote': this.downvotes++; break;

        case 'score':
            let downvotes = this.downvotes;
            let upvotes = this.upvotes;
            if (downvotes + upvotes > 50) {
                let increaseWith;
                if (downvotes > this.upvotes) increaseWith = Math.ceil(downvotes * 0.25);
                else increaseWith = Math.ceil(upvotes * 0.25);

                downvotes += increaseWith;
                upvotes += increaseWith;
            }
            let rating = rate.call(this);
            return [upvotes, downvotes, upvotes - downvotes, rating];
    }

    function rate() {
        let rating;
        let positiveVotesProcentage = (this.upvotes * 100) / (this.upvotes + this.downvotes);

        if (this.upvotes + this.downvotes < 10) {
            rating = 'new';
        } else if (positiveVotesProcentage > 66) {
            rating = 'hot';
        } else if (this.upvotes - this.downvotes < 0) {
            rating = 'unpopular';
        } else if (this.upvotes > 100 && this.downvotes > 100) {
            rating = 'controversial';
        } else rating = 'new';

        return rating;
    }
}

let post = {
    id: '3',
    author: 'emil',
    content: 'wazaaaaa',
    upvotes: 100,
    downvotes: 100
};

solution.call(post, 'upvote');
solution.call(post, 'downvote');
let score = solution.call(post, 'score'); // [127, 127, 0, 'controversial']
solution.call(post, 'downvote');          // (executed 50 times)
score = solution.call(post, 'score');     // [139, 189, -50, 'unpopular']