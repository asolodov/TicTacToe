import * as _ from 'lodash';
import { CellState } from './gridModel';

export const WinType = {
    COLUMN: 0,
    ROW: 1,
    UP_DIAGONAL: 2,
    DOWN_DIAGONAL: 3
};


const defaultOptions = {
    winCellsCount: 3
};

export class WinService {
    constructor(gridModel, options) {
        this._model = gridModel;
        this._options = _.extend({}, defaultOptions, options);
        if (this._options.winCellsCount > this._model.height ||
            this._options.winCellsCount > this._model.width) {
            throw new Error('Incorrect model sizes');
        }
    }

    checkWinner() {
        return this._checkWinInRows()
            || this._checkWinInCols()
            || this._checkWinInUpDiagonalies()
            || this._checkWinInDownDiagonalies();
    }

    _checkWinIn(cellGroups, minCellCount) {
        const lngFilter = _.filter(cellGroups, group => group.length >= minCellCount);
        const strGroupsArr = WinSearchHelper.mapCellGroupsToStringGroups(lngFilter);

        return WinSearchHelper.searchWinnerInStringGroups(strGroupsArr, CellState.TIC, minCellCount)
            || WinSearchHelper.searchWinnerInStringGroups(strGroupsArr, CellState.TOE, minCellCount);
    }

    _checkWinInRows() {
        const winner = this._checkWinIn(this._model.getRows(), this._options.winCellsCount);
        return winner ? _.extend(winner, { type: WinType.ROW }) : null;
    }

    _checkWinInCols() {
        const winner = this._checkWinIn(this._model.getColumns(), this._options.winCellsCount);
        return winner ? _.extend(winner, { type: WinType.COLUMN }) : null;
    }

    _checkWinInUpDiagonalies() {
        const winner = this._checkWinIn(this._model.getUpDiagonalies(), this._options.winCellsCount);
        return winner ? _.extend(winner, { type: WinType.UP_DIAGONAL }) : null;
    }

    _checkWinInDownDiagonalies() {
        const winner = this._checkWinIn(this._model.getDownDiagonalies(), this._options.winCellsCount);
        return winner ? _.extend(winner, { type: WinType.DOWN_DIAGONAL }) : null;
    }
}

class WinSearchHelper {
    static searchWinnerInStringGroups(cellStringGroups, searchState, winCellCount) {
        const arr = Array(winCellCount + 1).join(searchState.toString());
        for (let i = 0; i < cellStringGroups.length; i++) {
            const obj = cellStringGroups[i];
            const indx = obj.str.indexOf(arr);
            if (indx !== -1) {
                return {
                    cells: obj.group.slice(indx, winCellCount + indx),
                    state: searchState
                };
            }
        }
    }

    static mapCellGroupsToStringGroups(cellGroups) {
        return cellGroups.map(group => {
            return {
                str: WinSearchHelper.mapCellsToString(group),
                group: group
            };
        });
    }

    static mapCellsToString(cells) {
        return cells.map(cell => {
            return cell.state !== CellState.NONE ? cell.state.toString() : '-';
        }).join('');
    }
}