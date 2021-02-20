function createComputerHierarchy() {
    class Keyboard {
        constructor(manufacturer, responseTime) {
            this.manufacturer = manufacturer;
            this.responseTime = responseTime;
        }
    }

    class Monitor {
        constructor(manufacturer, width, height) {
            this.manufacturer = manufacturer;
            this.width = width;
            this.height = height;
        }
    }

    class Battery {
        constructor(manufacturer, expectedLife) {
            this.manufacturer = manufacturer;
            this.expectedLife = expectedLife;
        }
    }

    function Computer(manufacturer, processorSpeed, ram, hardDiskSpace) {
        if (this.constructor === Computer) {
            throw new Error();
        }

        this.manufacturer = manufacturer;
        this.processorSpeed = processorSpeed;
        this.ram = ram;
        this.hardDiskSpace = hardDiskSpace;
    }

    class Laptop {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace, weight, color, battery) {
            Computer.call(this, manufacturer, processorSpeed, ram, hardDiskSpace);

            this.weight = weight;
            this.color = color;
            this.battery = battery;
        }

        set battery(battery) {
            if (battery instanceof Battery) {
                this._battery = battery;
            } else {
                throw new TypeError();
            }
        }

        get battery() {
            return this._battery;
        }
    }

    class Desktop {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace, keyboard, monitor) {
            Computer.call(this, manufacturer, processorSpeed, ram, hardDiskSpace);

            this.keyboard = keyboard;
            this.monitor = monitor;
        }

        set keyboard(keyboard) {
            if (keyboard instanceof Keyboard) {
                this._keyboard = keyboard;
            } else {
                throw new TypeError();
            }
        }

        get keyboard() {
            return this._keyboard;
        }

        set monitor(monitor) {
            if (monitor instanceof Monitor) {
                this._monitor = monitor;
            } else {
                throw new TypeError();
            }
        }
        get monitor() {
            return this._monitor;
        }
    }

    return {
        Keyboard,
        Monitor,
        Battery,
        Computer,
        Laptop,
        Desktop,
    }
}