/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

declare var SVG: any;

interface JQuery {
    noUiSlider(settings: any): any;
}

function processForm() {
    $("#polygonContainer").show();
    var sides = getValueAsNumber("#sides");
    var length = getValueAsNumber("#length");
    var perimeter = calculatePermiter(sides, length);
    setPerimeter(perimeter);
    drawPolygon(sides, length);
}

function getValueAsNumber(id: string) {
    var value = Number($(id).val());
    return value;
}

function setPerimeter(value: number) {
    document.getElementById("perimeter").innerHTML = value.toString();
}

function calculatePermiter(sides: number, length: number): number {
    var perimeter = sides * length;
    return perimeter;
}

function drawPolygon(edges: number, radius: number) {
    polygon
        .animate(500)
        .ngon({
            radius: (20 * Math.log(radius + 2)),
            edges: edges
        })
        .during(function () {
            resizeSvgContainer();
        })
        .after(function () {
            resizeSvgContainer();
        });
}

function resizeSvgContainer() {
    svgElement.size(polygon.width() + 2, polygon.height() + 2);
}

var svgElement;
var polygon;

$(document).ready(function () {
    $("#polygonContainer").hide();
    svgElement = SVG("polygon");
    polygon = svgElement
        .polygon()
        .fill("none")
        .stroke({ width: 2 })
        .ngon({
            radius: 0,
            edges: 0
        });

    $('#sides')
        .noUiSlider({
            range: [3, 20],
            start: 3,
            step: 1,
            handles: 1,
            slide: processForm,
            set: processForm
        });

  $('#length')
        .noUiSlider({
            range: [1, 2000],
            start: 100,
            step: 1,
            handles: 1,
            slide: processForm,
            set: processForm
        });

    processForm();
});
