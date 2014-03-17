function processForm(form: HTMLFormElement) {
    var sides = getValueAsNumber("sides");
    var length = getValueAsNumber("length");
    var perimeter = calculatePermiter(sides, length);
    setPerimeter(perimeter);
}

function getValueAsNumber(id: string) {
    var element = <HTMLInputElement>document.getElementById(id);
    var value = parseFloat(element.value);
    return value;
}

function setPerimeter(value: number) {
    document.getElementById("perimeter").innerHTML = value.toString();
}

function calculatePermiter(sides: number, length: number): number {
    var perimeter = sides * length;
    return perimeter;
}