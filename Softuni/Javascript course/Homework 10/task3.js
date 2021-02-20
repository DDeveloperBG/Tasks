function solve() {
    function Employee(name, age) {
        this.name = name;
        this.age = age;
        this.salary = 0;
        this._taskIndex = 0;
    }

    Employee.prototype.work = function () {
        if (this.tasks.length === this._taskIndex) {
            this._taskIndex = 0;
        }
        console.log(this.tasks[this._taskIndex]);
        this._taskIndex++;
    };

    Employee.prototype.collectSalary = function () {
        console.log(`${this.name} received ${this.salary + (this.dividend ? this.dividend : 0)} this month.`);
    };

    function Junior(name, age) {
        Employee.call(this, name, age);
        this.tasks = [`${name} is working on a simple task.`];
    }

    function Senior(name, age) {
        Employee.call(this, name, age);
        this.tasks = [`${name} is working on a complicated task.`, `${name} is taking time off work.`, `${name} is supervising junior workers.`];
    }

    function Manager(name, age) {
        Employee.call(this, name, age);
        this.tasks = [`${name} scheduled a meeting.`, `${name} is preparing a quarterly report.`];
        this.dividend = 0;
    }

    Junior.prototype = Object.create(Employee.prototype);
    Senior.prototype = Object.create(Employee.prototype);
    Manager.prototype = Object.create(Employee.prototype);

    return {
        Employee,
        Junior,
        Senior,
        Manager,
    };
}