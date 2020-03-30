"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var fabric_1 = require("fabric");
var FabricFactory = /** @class */ (function () {
    function FabricFactory() {
    }
    FabricFactory.prototype.createCanvas = function (canvas) {
        return new fabric_1.fabric.Canvas(canvas);
    };
    FabricFactory.prototype.createImage = function (image) {
        return new fabric_1.fabric.Image(image);
    };
    FabricFactory.prototype.createText = function (sampleText, canvasWidth) {
        return new fabric_1.fabric.IText(sampleText, {
            fontFamily: "Impact",
            fontSize: 50,
            stroke: "#000000",
            strokeWidth: 3,
            fill: "#ffffff",
            textAlign: "center",
            left: canvasWidth / 2,
            originX: "center"
        });
    };
    return FabricFactory;
}());
exports.FabricFactory = FabricFactory;
//# sourceMappingURL=fabric-factory.js.map