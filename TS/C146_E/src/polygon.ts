/// <reference path="../Scripts/typings/jquery/jquery.d.ts" />

declare var SVG: any;

interface JQuery {
    noUiSlider(settings: any): any;
}

function createPolygon(): void {
    svgElement = SVG("polygon");
    polygon = svgElement
        .polygon()
        .fill("none")
        .stroke({ width: 2 })
        .ngon({
            radius: 0,
            edges: 0
        });
}

function createSlider(id: string, range: number[], start: number): void {
    $(id)
        .noUiSlider({
            range: range,
            start: start,
            step: 1,
            handles: 1,
            slide: process,
            set: process
        });
}

function calculatePermiter(sides: number, length: number): number {
    var perimeter = sides * length;
    return perimeter;
}

function drawPolygon(edges: number, radius: number): void {
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

function process(): void {
    var sides = getValueAsNumber("#sides");
    var length = getValueAsNumber("#length");
    var perimeter = calculatePermiter(sides, length);

    updateElement("#sidesValue", sides);
    updateElement("#lengthValue", length);
    updateElement("#perimeter", perimeter);
    drawPolygon(sides, length);
}

function getValueAsNumber(id: string): number {
    var value = Number($(id).val());
    return value;
}

function resizeSvgContainer() {
    svgElement.size(polygon.width() * 1.3, polygon.height() + 2);
}

function updateElement(id: string, value: any): void {
    var element = $(id);
    element.text(value.toString());
}

var svgElement;
var polygon;

$(document).ready(function () {
    createPolygon();
    createSlider("#sides", [3, 20], 3);
    createSlider("#length", [1, 2000], 100);

    process();
});
