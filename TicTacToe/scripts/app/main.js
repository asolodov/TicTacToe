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
            OnPlayerStepMessage: this._cellSelectionReceived.bind(this)
        });
    };

    _.extend(TicTacToe.prototype, {
        start: function () {
            this._drawManager.init();

            this._drawManager.reDrawModel();
            this._last = CellState.TIC;
        },
        _drawSelectedCell: function (cell) {
            this._last = this._last === CellState.TIC ? CellState.TOE : CellState.TIC;
            cell.state = this._last;

            this._drawManager.reDrawModel();
            const winner = this._winService.checkWinner();
            if (winner) {
                this._drawManager.drawWinner(winner);
            }
        },
        _cellSelected: function (cell) {
            this._connectionManager.sendUserStep({
                x: cell.x,
                y: cell.y
            });

            this._drawSelectedCell(cell);
        },
        _cellSelectionReceived: function (cell) {
            debugger;
            var model = this._gridModel.getCell(cell.X, cell.Y);
            this._drawSelectedCell(model);
        }
    });

    return TicTacToe;
}());