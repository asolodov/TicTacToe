const _ = require('lodash');
const CellState = require('./gridModel').CellState;

const WinType = {
    COLUMN: 0,
    ROW: 1,
    UP_DIAGONAL: 2,
    DOWN_DIAGONAL: 3
};

const WinService = (function () {
    const defaultOptions = {
        winCellsCount: 3
    };

    function WinService(gridModel, options) {
        this._model = gridModel;
        this._options = _.extend({}, defaultOptions, options);
        if (this._options.winCellsCount > this._model.height ||
            this._options.winCellsCount > this._model.width) {
            throw new Error('Incorrect model sizes');
        }
    };

    _.extend(WinService.prototype,
        {
            checkWinner: function () {
                return this._checkWinInRows()
                    || this._checkWinInCols()
                    || this._checkWinInUpDiagonalies()
                    || this._checkWinInDownDiagonalies();
            },
            _checkWinIn: function (cellGroups, minCellCount) {
                const lngFilter = _.filter(cellGroups, (group) => group.length >= minCellCount);
                const strGroupsArr = WinSearchHelper.mapCellGroupsToStringGroups(lngFilter);

                return WinSearchHelper.searchWinnerInStringGroups(strGroupsArr, CellState.TIC, minCellCount)
                    || WinSearchHelper.searchWinnerInStringGroups(strGroupsArr, CellState.TOE, minCellCount);
            },
            _checkWinInRows() {
                const winner = this._checkWinIn(this._model.getRows(), this._options.winCellsCount);
                return winner ? _.extend(winner, { type: WinType.ROW }) : null;
            },
            _checkWinInCols() {
                const winner = this._checkWinIn(this._model.getColumns(), this._options.winCellsCount);
                return winner ? _.extend(winner, { type: WinType.COLUMN }) : null;
            },
            _checkWinInUpDiagonalies() {
                const winner = this._checkWinIn(this._model.getUpDiagonalies(), this._options.winCellsCount);
                return winner ? _.extend(winner, { type: WinType.UP_DIAGONAL }) : null;
            },
            _checkWinInDownDiagonalies() {
                const winner = this._checkWinIn(this._model.getDownDiagonalies(), this._options.winCellsCount);
                return winner ? _.extend(winner, { type: WinType.DOWN_DIAGONAL }) : null;
            }
        });

    return WinService;
}());

const WinSearchHelper = (function () {
    function WinSearchHelper() {
    };

    WinSearchHelper.searchWinnerInStringGroups = function (cellStringGroups, searchState, winCellCount) {
        const arr = Array(winCellCount + 1).join(searchState.toString());
        for (var i = 0; i < cellStringGroups.length; i++) {
            const obj = cellStringGroups[i];
            const indx = obj.str.indexOf(arr);
            if (indx !== -1) {
                return {
                    cells: obj.group.slice(indx, winCellCount + indx),
                    state: searchState
                };
            }
        }
    };

    WinSearchHelper.mapCellGroupsToStringGroups = function (cellGroups) {
        return _.map(cellGroups, (group) => {
            return {
                str: WinSearchHelper.mapCellsToString(group),
                group: group
            };
        });
    };

    WinSearchHelper.mapCellsToString = function (cells) {
        return _.map(cells, (cell) => {
            return cell.state !== CellState.NONE ? cell.state.toString() : '-';
        }).join('');
    };

    return WinSearchHelper;
}());

module.exports = {
    WinService,
    WinType
};