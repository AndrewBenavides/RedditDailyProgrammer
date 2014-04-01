function processForm(form: HTMLFormElement) {
    var sides = getValueAsNumber("sides");
    var length = getValueAsNumber("length");
    var perimeter = calculatePermiter(sides, length);
    setPerimeter(perimeter);
    drawPolygon(sides, length);
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

function drawPolygon(sides: number, length: number) {
    var canvas = <HTMLCanvasElement>document.getElementById("polygon");
    var context = canvas.getContext("2d");
    var xCenter = length + 20;
    var yCenter = length + 20;
    canvas.width = xCenter * 2;
    canvas.height = yCenter * 2;

    context.beginPath();
    context.moveTo(xCenter + length * Math.cos(0), yCenter + length * Math.sin(0));

    for (var i = 1; i <= sides; i++) {
        context.lineTo(xCenter + length * Math.cos(i * 2 * Math.PI / sides), yCenter + length * Math.sin(i * 2 * Math.PI / sides))
    }

    context.strokeStyle = "#000000";
    context.lineWidth = 1;
    context.stroke();
}