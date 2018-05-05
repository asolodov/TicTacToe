const _ = require('lodash');
const DrawManager = require('./drawManager');
const GridModel = require('./gridModel').GridModel;
const CellState = require('./gridModel').CellState;
const WinService = require('./winService').WinService;
const ConnectionManager = require('./connectionManager');

module.exports = (function () {
    const defaultOptions = {
        height: 3,
        width: 3,
        areaElement: null
    };

    function TicTacToe(options) {
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
    };

    _.extend(TicTacToe.prototype, {
        start: function () {
            this._drawManager.init();

            this._drawManager.reDrawModel();
            this._last = CellState.TIC;
        },
        _drawSelectedCell: function (cell, cellType) {
            //this._last = this._last === CellState.TIC ? CellState.TOE : CellState.TIC;
            //cell.state = this._last;
            cell.state = cellType;

            this._drawManager.reDrawModel();
            const winner = this._winService.checkWinner();
            if (winner) {
                this._drawManager.drawWinner(winner);
            }
        },
        _cellSelected: function (cell) {
            console.log(cell);
            if (!this._isActive)
                return;

            this._connectionManager.sendUserStep(this._cellType, {
                x: cell.x,
                y: cell.y
            });

            this._drawSelectedCell(cell, this._cellType);
            this._isActive = false;
        },
        _gameStarted: function (message) {
            console.log(message);
            this._cellType = message.cellType;
            this._isActive = message.isActive;
        },
        _gameStopped: function (message) {
            console.log(message);
            console.log('reset');
            this._gridModel.reset();
            this._drawManager.reDrawModel();
        },
        _stateUpdateReceived: function (message) {
            console.log(message);
            var model = this._gridModel.getCell(message.cellPosition.x, message.cellPosition.y);
            this._drawSelectedCell(model, message.cellType);
            this._isActive = message.isActive;
        },
    });

    return TicTacToe;
}());