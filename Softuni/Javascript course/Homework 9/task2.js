function hidratateWorker(worker) {
    if (worker.dizziness) {
        worker.levelOfHydrated += 0.1 * worker.experience * worker.weight;
        worker.dizziness = false;
    }

    return worker;
}

let worker = {
    weight: 80,
    experience: 1,
    levelOfHydrated: 0,
    dizziness: true
};

let result = hidratateWorker(worker);

console.log(result);