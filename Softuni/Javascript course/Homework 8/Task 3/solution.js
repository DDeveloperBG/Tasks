function calculateBMI(name, age, weight, height) {
    let BMI = Math.round(weight / Math.pow(height, 2) * 10 ** 4);
    let personalInfo = { age, weight, height };
    let result = { name, personalInfo, BMI };
    let status;

    if (BMI < 18.5) status = 'underweight';
    else if (BMI < 25) status = 'normal';
    else if (BMI < 30) status = 'overweight';
    else {
        status = 'obese';
        result['recommendation'] = 'admission required';
    }

    result.status = status;
    return result;
}

console.log(calculateBMI('Peter', 29, 75, 182));
/*
{
    {
        name: 'Peter',
            personalInfo: {
                age: 29,
                weight: 75,
                height: 182
        }
        BMI: 23
        status: 'normal'
    }
}
*/