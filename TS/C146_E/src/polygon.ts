/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

declare var SVG: any;

function processForm(e) {
    e.preventDefault();
    $("#polygonContainer").show();
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
    polygon.animate(500).ngon({
        radius: length,
        edges: sides
    });
}

var draw;
var polygon;

$(document).ready(function () {
    $("#polygonForm").submit(processForm);
    $("#polygonContainer").hide();
    draw = SVG('polygon');
    polygon = draw.polygon().ngon({
        radius: 0,
        edges: 0
    });
});
