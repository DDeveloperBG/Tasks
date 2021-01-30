class Company {
    constructor() {
        this.departments = [];
    }

    addEmployee(username, salary, position, department) {
        if (username && salary > 0 && position && department) {
            if (!this.departments[department]) {
                this.departments[department] = [];
            }
            this.departments[department].push({ username, salary, position });
            return `New employee is hired. Name: ${username}. Position: ${position}`;
        } else {
            throw "Invalid input!";
        }
    }

    bestDepartment() {
        function averageSalary(department) {
            return department.reduce((acc, curr) => acc + curr.salary, 0) / department.length;
        }
        function compareDepartments(a, b) {
            return averageSalary(b) - averageSalary(a);
        }
        let bestDepartment = Object.entries(this.departments).sort(compareDepartments)[0];
        let sortedEmployees = bestDepartment[1].sort((a, b) => b.salary - a.salary || a.username.localeCompare(b.username));
        let res = `Best Department is: ${bestDepartment[0]}\n`;
        
        res += `Average salary: ${averageSalary(bestDepartment[1]).toFixed(2)}\n`;
        res += sortedEmployees.map(employee => `${employee.username} ${employee.salary} ${employee.position}`).join('\n');
        return res;
    }
}

let c = new Company();
c.addEmployee("Stanimir", 2000, "engineer", "Construction");
c.addEmployee("Pesho", 1500, "electrical engineer", "Construction");
c.addEmployee("Slavi", 500, "dyer", "Construction");
c.addEmployee("Stan", 2000, "architect", "Construction");
c.addEmployee("Stanimir", 1200, "digital marketing manager", "Marketing");
c.addEmployee("Pesho", 1000, "graphical designer", "Marketing");
c.addEmployee("Gosho", 1350, "HR", "Human resources");
console.log(c.bestDepartment());

/*
Best Department is: Construction
Average salary: 1500.00
Stan 2000 architect
Stanimir 2000 engineer
Pesho 1500 electrical engineer
Slavi 500 dyer
*/