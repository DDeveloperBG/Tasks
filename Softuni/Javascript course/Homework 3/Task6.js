function systemComponents(input) {
    class System {
        constructor(name) {
            this.name = name;
            this.components = [];
        }

        addComponent(component) {
            this.components.push(component);
        }
    }

    class Component {
        constructor(name) {
            this.name = name;
            this.subcomponents = [];
        }

        addSubcomponent(subcomponent) {
            this.subcomponents.push(subcomponent);
        }
    }

    function sortSystems(systemA, systemB) {
        if (systemA.components.length > systemB.components.length) {
            return -1;
        }
        else if (systemA.components.length < systemB.components.length) {
            return 1;
        }
        else {
            return systemA.name.localeCompare(systemB.name);
        }
    }

    function sortComponents(componentA, componentB) {
        if (componentA.subcomponents.length > componentB.subcomponents.length) {
            return -1;
        }
        else if (componentA.subcomponents.length < componentB.subcomponents.length) {
            return 1;
        }
        else {
            return 0;
        }
    }

    let systemsData = input.map(row => row.split(' | '));
    let systemsRegister = [];

    for (let systemInfo of systemsData) {
        let [systemName, componentName, subcomponentName] = systemInfo;

        let systemIndex = systemsRegister.findIndex(system => system.name == systemName);
        if (systemIndex == -1) {
            let system = new System(systemName);
            let component = new Component(componentName);

            component.addSubcomponent(subcomponentName);
            system.addComponent(component);

            systemsRegister.push(system);
        }
        else {
            let componentIndex = systemsRegister[systemIndex].components.findIndex(component => component.name == componentName);
            if (componentIndex == -1) {
                let component = new Component(componentName);
                component.addSubcomponent(subcomponentName);

                systemsRegister[systemIndex].addComponent(component);
            }
            else {
                systemsRegister[systemIndex].components[componentIndex].addSubcomponent(subcomponentName);
            }
        }
    }

    systemsRegister.sort(sortSystems);
    systemsRegister.map(system => system.components.sort(sortComponents));

    const normalSeparator = '\n';
    const componentsSeparator = '\n|||';
    const subcomponentsSeparator = '\n||||||';

    console.log(systemsRegister
        .map(system => system.name + componentsSeparator
            + system.components
                .map(component => component.name
                    + subcomponentsSeparator + component.subcomponents.join(subcomponentsSeparator))
                .join(componentsSeparator))
        .join(normalSeparator));
}

systemComponents(['SULS | Main Site | Home Page',
    'SULS | Main Site | Login Page',
    'SULS | Main Site | Register Page',
    'SULS | Judge Site | Login Page',
    'SULS | Judge Site | Submittion Page',
    'Lambda | CoreA | A23',
    'SULS | Digital Site | Login Page',
    'Lambda | CoreB | B24',
    'Lambda | CoreA | A24',
    'Lambda | CoreA | A25',
    'Lambda | CoreC | C4',
    'Indice | Session | Default Storage',
    'Indice | Session | Default Security']);