const _ = require('lodash');
const DrawManager = require('./drawManager');
const GridModel = require('./gridModel').GridModel;
const CellState = require('./gridModel').CellState;
const WinService = require('./winService').WinService;

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
    };

    _.extend(TicTacToe.prototype, {
        start: function () {
            this._drawManager.init();

            this._drawManager.reDrawModel();
            this._last = CellState.TIC;
        },
        _cellSelected: function (cell) {
            this._last = this._last === CellState.TIC ? CellState.TOE : CellState.TIC;
            cell.state = this._last;

            this._drawManager.reDrawModel();
            const winner = this._winService.checkWinner();
            if (winner) {
                this._drawManager.drawWinner(winner);
            }
        }
    });

    return TicTacToe;
}());