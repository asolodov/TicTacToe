import * as _ from 'lodash';
import  DrawManager from './drawManager';
import { GridModel, CellState } from './gridModel';
import { WinService } from './winService';
import ConnectionManager from './connectionManager'

const defaultOptions = {
    height: 3,
    width: 3,
    areaElement: null
};

export class TicTacToe {
    constructor(options) {
        this._options = _.extend(this._options, defaultOptions, options);
        this._gridModel = new GridModel(6, 8);
        this._drawManager = new DrawManager(this._gridModel,
            {
                element: this._options.areaElement,
                onCellSelected: this._cellSelected.bind(this),
                sizes: {
                    areaWidth: 400,
                    areaHeight: 300
                }
            });
        this._winService = new WinService(this._gridModel, { winCellsCount: 4 });
        this._connectionManager = new ConnectionManager({
            hubUrl: '/gamehub',
            onGameStarted: this._gameStarted.bind(this),
            onGameStopped: this._gameStopped.bind(this),
            onStateUpdate: this._stateUpdateReceived.bind(this)
        });
    }

    start() {
        this._drawManager.init();
        this._drawManager.reDrawModel();
        this._last = CellState.TIC;
    }

    _drawSelectedCell(cell, cellType) {
        cell.state = cellType;
        this._drawManager.reDrawModel();
        const winner = this._winService.checkWinner();
        if (winner) {
            this._drawManager.drawWinner(winner);
        }
    }

    _cellSelected(cell) {
        console.log(cell);
        if (!this._isActive)
            return;

        this._connectionManager.sendUserStep(this._cellType, {
            x: cell.x,
            y: cell.y
        });

        this._drawSelectedCell(cell, this._cellType);
        this._isActive = false;
    }

    _gameStarted(message) {
        console.log(message);
        this._cellType = message.cellType;
        this._isActive = message.isActive;
    }

    _gameStopped(message) {
        console.log(message);
        console.log('reset');
        this._gridModel.reset();
        this._drawManager.reDrawModel();
    }

    _stateUpdateReceived(message) {
        console.log(message);
        var model = this._gridModel.getCell(message.cellPosition.x, message.cellPosition.y);
        this._drawSelectedCell(model, message.cellType);
        this._isActive = message.isActive;
    }
}