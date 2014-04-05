/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

function processForm(e) {
    e.preventDefault();
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
    var xCenter = length + 5;
    var yCenter = length + 5;
    function getPoint(center: number, coor: number, func: (x: number) => number) {
        return center + length * func(coor);
    }
    function getX(coor: number) {
        return getPoint(xCenter, coor, Math.cos);
    }
    function getY(coor: number) {
        return getPoint(yCenter, coor, Math.sin);
    }
    var canvas = <HTMLCanvasElement>document.getElementById("polygon");
    var context = canvas.getContext("2d");
    canvas.width = xCenter * 2;
    canvas.height = yCenter * 2;

    context.beginPath();
    context.moveTo(getX(0), getY(0));

    for (var i = 1; i <= sides; i++) {
        var coor = i * 2 * Math.PI / sides;
        context.lineTo(getX(coor), getY(coor));
    }

    context.strokeStyle = "#000000";
    context.lineWidth = 1;
    context.stroke();
}

$(document).ready(function () {
    $("#polygonForm").submit(processForm);
});
