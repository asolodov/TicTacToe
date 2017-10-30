const _ = require('lodash');
const PIXI = require('pixi.js');
const WinType = require('./winService').WinType;

let DrawManager = (function () {
    const defaultOptions = {
        width: 800,
        height: 600,
        backgroundColor: 0x1099bb,
        element: null,
        onCellSelected: function () { },
        cellHeight: 100,
        cellWidth: 100
    };

    function DrawManager(gridModel, options) {
        this._gridModel = gridModel;
        this._options = _.extend({}, defaultOptions, options);
    }

    _.extend(DrawManager.prototype, {
        init: function () {
            this._pixiApp = new PIXI.Application(this._options.width, this._options.height,
                {
                    backgroundColor: this._options.backgroundColor
                });

            this._options.element.appendChild(this._pixiApp.view);
        },
        reDrawModel: function () {
            this._clearStage();
            for (let i = 0; i < this._gridModel.height; i++) {
                for (let j = 0; j < this._gridModel.width; j++) {
                    const graphics = this._drawCell(this._gridModel.getCell(j, i));
                    this._addToStage(graphics);
                }
            }
        },
        drawWinner: function (winner) {
            const graphics = new PIXI.Graphics();
            graphics.lineStyle(10, 0x00FF00, 1);

            switch (winner.type) {
                case WinType.ROW:
                    this._drawCrossHorizontalCells(graphics, winner.cells);
                    break;
                case WinType.COLUMN:
                    this._drawCrossVerticalCells(graphics, winner.cells);
                    break;
                case WinType.UP_DIAGONAL:
                    this._drawCrossUpDiagonalCells(graphics, winner.cells);
                    break;
                case WinType.DOWN_DIAGONAL:
                    this._drawCrossDownDiagonalCells(graphics, winner.cells);
                    break;
                default:
            }
            this._addToStage(graphics);
        },
        _clearStage: function () {
            for (let i = 0; i < this._pixiApp.stage; i++) {
                this._pixiApp.stage.removeChild(this._pixiApp.stage.children[i]);
            }
        },
        _addToStage: function (graphics) {
            this._pixiApp.stage.addChild(graphics);
        },
        _getCellPosition: function () {
            switch (arguments.length) {
                case 0:
                    return {
                        x: this._options.cellWidth,
                        y: this._options.cellHeight
                    };
                case 1:
                    return {
                        x: arguments[0].x * this._options.cellWidth,
                        y: arguments[0].y * this._options.cellHeight
                    };
                case 2:
                    return {
                        x: arguments[0] * this._options.cellWidth,
                        y: arguments[1] * this._options.cellHeight
                    };
                default:
                    throw new Error('Incorrect parameters');
            }
        },
        _drawCell: function (cell) {
            let graphics = new PIXI.Graphics();

            graphics.lineStyle(2, 0x0000FF, 1);
            graphics.beginFill(0xFF700B, 1);

            const pos = this._getCellPosition(cell);
            graphics.drawRect(pos.x, pos.y, this._options.cellHeight, this._options.cellWidth);

            if (cell.state === 0) {
                this._drawCellToe(graphics, cell);
            } else if (cell.state === 1) {
                this._drawCellTic(graphics, cell);
            }

            graphics.interactive = true;
            graphics.hitArea = graphics.getBounds();
            var that = this;
            graphics.click = function (e) {
                that._handleCellClick(cell);
            };

            return graphics;
        },
        _handleCellClick: function (cell) {
            this._options.onCellSelected(cell);
        },
        _drawCellToe: function (graphics, cell) {
            graphics.lineStyle(5, 0x000000, 1);
            const rad = 50,
                pos = this._getCellPosition(cell),
                delt = 5;
            graphics.drawCircle(pos.x + rad, pos.y + rad, rad - delt);
        },
        _drawCellTic: function (graphics, cell) {
            graphics.lineStyle(5, 0x000000, 1);
            const pos = this._getCellPosition(cell),
                delt = 5;
            graphics.moveTo(pos.x + delt, pos.y + delt);
            graphics.lineTo(pos.x + this._options.cellWidth - delt, pos.y + this._options.cellHeight - delt);
            graphics.moveTo(pos.x + this._options.cellWidth - delt, pos.y + delt);
            graphics.lineTo(pos.x + delt, pos.y + this._options.cellHeight - delt);
        },
        _drawCrossHorizontalCells: function (graphics, cells) {
            let firstPos = this._getCellPosition(cells[0]),
                lastPos = this._getCellPosition(cells.slice(-1)[0]);

            const linePosY = (firstPos.y + this._options.cellHeight / 2);
            graphics.moveTo(firstPos.x, linePosY);
            graphics.lineTo(lastPos.x + this._options.cellWidth, linePosY);
        },
        _drawCrossVerticalCells: function (graphics, cells) {
            let firstPos = this._getCellPosition(cells[0]),
                lastPos = this._getCellPosition(cells.slice(-1)[0]);

            const linePosX = (firstPos.x + this._options.cellWidth / 2);
            graphics.moveTo(linePosX, firstPos.y);
            graphics.lineTo(linePosX, lastPos.y + this._options.cellHeight);
        },
        _drawCrossUpDiagonalCells: function (graphics, cells) {
            let firstPos = this._getCellPosition(cells[0]),
                lastPos = this._getCellPosition(cells.slice(-1)[0]);
            if (firstPos.x > lastPos.x) {
                const t = firstPos;
                firstPos = lastPos;
                lastPos = t;
            }

            graphics.moveTo(firstPos.x, firstPos.y + this._options.cellHeight);
            graphics.lineTo(lastPos.x + this._options.cellWidth, lastPos.y);
        },
        _drawCrossDownDiagonalCells: function (graphics, cells) {
            let firstPos = this._getCellPosition(cells[0]),
                lastPos = this._getCellPosition(cells.slice(-1)[0]);
            if (firstPos.x > lastPos.x) {
                const t = firstPos;
                firstPos = lastPos;
                lastPos = t;
            }

            graphics.moveTo(firstPos.x, firstPos.y);
            graphics.lineTo(lastPos.x + this._options.cellWidth, lastPos.y + this._options.cellHeight);
        }
    });

    return DrawManager;
}());

module.exports = DrawManager;