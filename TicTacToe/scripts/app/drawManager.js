import * as _ from 'lodash';
import * as PIXI from 'pixi.js';
import { WinType } from './winService';
import { CellState } from './gridModel';

const defaultOptions = {
    element: null,
    onCellSelected: () => { },
    colors: {
        backgroundColor: 0xFFFFFF,
        cellLineColor: 0x000000,
        cellColor: 0xE8E8E8,
        toeLineColor: 0x000000,
        ticLineColor: 0x000000,
        winCrossColor: 0xFF0000
    },
    sizes: {
        areaWidth: 800,
        areaHeight: 600,
        cellWidth: 50,
        cellHeight: 50,
        cellLineWidth: 2,
        ticLineWidth: 5,
        toeLineWidth: 5,
        winCrossWidth: 7
    }
};

export default class DrawManager {
    constructor(gridModel, options) {
        this._gridModel = gridModel;
        this._options = _.merge(defaultOptions, options);
    }

    init() {
        this._pixiApp = new PIXI.Application(this._options.sizes.areaWidth, this._options.sizes.areaHeight,
            {
                backgroundColor: this._options.colors.backgroundColor
            });

        this._options.element.appendChild(this._pixiApp.view);
    }

    reDrawModel() {
        this._clearStage();
        for (let i = 0; i < this._gridModel.height; i++) {
            for (let j = 0; j < this._gridModel.width; j++) {
                const graphics = this._drawCell(this._gridModel.getCell(j, i));
                this._addToStage(graphics);
            }
        }
    }

    drawWinner(winner) {
        const graphics = new PIXI.Graphics();
        graphics.lineStyle(this._options.sizes.winCrossWidth, this._options.colors.winCrossColor, 1);

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
    }

    _clearStage() {
        for (let i = 0; i < this._pixiApp.stage; i++) {
            this._pixiApp.stage.removeChild(this._pixiApp.stage.children[i]);
        }
    }

    _addToStage(graphics) {
        this._pixiApp.stage.addChild(graphics);
    }

    _getCellPosition() {
        switch (arguments.length) {
            case 0:
                return {
                    x: this._options.sizes.cellWidth,
                    y: this._options.sizes.cellHeight
                };
            case 1:
                return {
                    x: arguments[0].x * this._options.sizes.cellWidth,
                    y: arguments[0].y * this._options.sizes.cellHeight
                };
            case 2:
                return {
                    x: arguments[0] * this._options.sizes.cellWidth,
                    y: arguments[1] * this._options.sizes.cellHeight
                };
            default:
                throw new Error('Incorrect parameters');
        }
    }

    _drawCell(cell) {
        let graphics = new PIXI.Graphics();

        graphics.lineStyle(this._options.sizes.cellLineWidth, this._options.colors.cellLineColor, 1);
        graphics.beginFill(this._options.colors.cellColor, 1);

        const pos = this._getCellPosition(cell);
        graphics.drawRect(pos.x, pos.y, this._options.sizes.cellHeight, this._options.sizes.cellWidth);

        if (cell.state === CellState.TOE) {
            this._drawCellToe(graphics, cell);
        } else if (cell.state === CellState.TIC) {
            this._drawCellTic(graphics, cell);
        }

        graphics.interactive = true;
        graphics.hitArea = graphics.getBounds();
        let that = this;
        graphics.click = function (e) {
            that._handleCellClick(cell);
        };

        return graphics;
    }

    _handleCellClick(cell) {
        this._options.onCellSelected(cell);
    }

    _drawCellToe(graphics, cell) {
        graphics.lineStyle(this._options.sizes.toeLineWidth, this._options.colors.toeLineColor, 1);
        const rad = Math.min(this._options.sizes.cellWidth, this._options.sizes.cellHeight) / 2,
            pos = this._getCellPosition(cell),
            delt = 5;
        graphics.drawCircle(pos.x + rad, pos.y + rad, rad - delt);
    }

    _drawCellTic(graphics, cell) {
        graphics.lineStyle(this._options.sizes.ticLineWidth, this._options.colors.ticLineColor, 1);
        const pos = this._getCellPosition(cell),
            delt = 5;
        graphics.moveTo(pos.x + delt, pos.y + delt);
        graphics.lineTo(pos.x + this._options.sizes.cellWidth - delt, pos.y + this._options.sizes.cellHeight - delt);
        graphics.moveTo(pos.x + this._options.sizes.cellWidth - delt, pos.y + delt);
        graphics.lineTo(pos.x + delt, pos.y + this._options.sizes.cellHeight - delt);
    }

    _drawCrossHorizontalCells(graphics, cells) {
        let firstPos = this._getCellPosition(cells[0]),
            lastPos = this._getCellPosition(cells.slice(-1)[0]);

        const linePosY = (firstPos.y + this._options.sizes.cellHeight / 2);
        graphics.moveTo(firstPos.x, linePosY);
        graphics.lineTo(lastPos.x + this._options.sizes.cellWidth, linePosY);
    }

    _drawCrossVerticalCells(graphics, cells) {
        let firstPos = this._getCellPosition(cells[0]),
            lastPos = this._getCellPosition(cells.slice(-1)[0]);

        const linePosX = (firstPos.x + this._options.sizes.cellWidth / 2);
        graphics.moveTo(linePosX, firstPos.y);
        graphics.lineTo(linePosX, lastPos.y + this._options.sizes.cellHeight);
    }

    _drawCrossUpDiagonalCells(graphics, cells) {
        let firstPos = this._getCellPosition(cells[0]),
            lastPos = this._getCellPosition(cells.slice(-1)[0]);
        if (firstPos.x > lastPos.x) {
            const t = firstPos;
            firstPos = lastPos;
            lastPos = t;
        }

        graphics.moveTo(firstPos.x, firstPos.y + this._options.sizes.cellHeight);
        graphics.lineTo(lastPos.x + this._options.sizes.cellWidth, lastPos.y);
    }

    _drawCrossDownDiagonalCells(graphics, cells) {
        let firstPos = this._getCellPosition(cells[0]),
            lastPos = this._getCellPosition(cells.slice(-1)[0]);
        if (firstPos.x > lastPos.x) {
            const t = firstPos;
            firstPos = lastPos;
            lastPos = t;
        }

        graphics.moveTo(firstPos.x, firstPos.y);
        graphics.lineTo(lastPos.x + this._options.sizes.cellWidth, lastPos.y + this._options.sizes.cellHeight);
    }
}