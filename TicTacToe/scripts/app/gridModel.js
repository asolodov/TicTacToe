const _ = require('lodash');

const CellState = {
    NONE: null,
    TOE: 0,
    TIC: 1
};

const Cell = (function () {
    function Cell(x, y, state) {
        this._x = x;
        this._y = y;
        this.state = state || CellState.NONE;
        const that = this;
        Object.defineProperty(this, 'x', {
            get: () => that._x
        });

        Object.defineProperty(this, 'y', {
            get: () => that._y
        });
    };

    return Cell;
}());

const GridModel = (function () {

    function GridModel(height, width) {
        this._rows = [];
        this._height = 0;
        this._width = 0;
        const that = this;
        Object.defineProperty(this, 'width', {
            get: () => that._width
        });

        Object.defineProperty(this, 'height', {
            get: () => that._height
        });

        this._init(height, width);
    }

    _.extend(GridModel.prototype,
        {
            getCell: function (x, y) {
                if (!this._isValidCell(x, y)) {
                    throw new Error('Incorrect position');
                }
                return this._getCell(x, y);
            },
            getRows: function () {
                return this._rows;
            },
            getColumns: function () {
                let columns = [];
                for (let i = 0; i < this.width; i++) {
                    let col = [];
                    for (let j = 0; j < this.height; j++) {
                        col.push(this._getCell(i, j));
                    }
                    columns.push(col);
                }
                return columns;
            },
            getUpDiagonalies: function () {
                let diagsGroup = [];

                for (let y = 0; y < this.height; y++) {
                    let j = y,
                        i = 0,
                        diag = [];

                    while (this._isValidCell(i, j)) {
                        diag.push(this._getCell(i, j))
                        j--;
                        i++;
                    }
                    diagsGroup.push(diag);
                }

                for (let x = 1; x < this.width; x++) {
                    let i = x,
                        j = this.height - 1,
                        diag = [];

                    while (this._isValidCell(i, j)) {
                        diag.push(this._getCell(i, j));
                        j--;
                        i++;
                    }
                    diagsGroup.push(diag);
                }
                return diagsGroup;
            },
            getDownDiagonalies: function () {
                let diagsGroup = [];

                for (let y = 0; y < this.height; y++) {
                    let j = y,
                        i = this.width - 1,
                        diag = [];

                    while (this._isValidCell(i, j)) {
                        diag.push(this._getCell(i, j));
                        j--;
                        i--;
                    }
                    diagsGroup.push(diag);
                }

                for (let x = this.width - 2; x >= 0; x--) {
                    let i = x,
                        j = this.height - 1,
                        diag = [];

                    while (this._isValidCell(i, j)) {
                        diag.push(this._getCell(i, j));
                        j--;
                        i--;
                    }
                    diagsGroup.push(diag);
                }
                return diagsGroup;
            },
            reset: function () {
                for (let i = 0; i < this.width; i++) {
                    for (let j = 0; j < this.height; j++) {
                        this._getCell(i, j).state = CellState.NONE;
                    }
                }
            },
            _init: function (height, width) {
                this._height = height;
                this._width = width;
                this._rows = [];
                for (let i = 0; i < height; i++) {
                    const row = [];
                    for (let j = 0; j < width; j++) {
                        row.push(new Cell(j, i));
                    }
                    this._rows[i] = row;
                }
            },
            _getCell: function (x, y) {
                return this._rows[y][x];
            },
            _isValidCell: function (x, y) {
                return !((x < 0 || x >= this.width) || (y < 0 || y >= this.height));
            }
        });

    return GridModel;
}());

module.exports = {
    GridModel,
    Cell,
    CellState
};