export const CellState = {
    NONE: null,
    TOE: 0,
    TIC: 1
};


export class Cell {
    constructor(x, y, state) {
        this._x = x;
        this._y = y;
        this.state = state || CellState.NONE;
    };

    get x() {
        return this._x;
    }

    get y() {
        return this._y;
    }
}

export class GridModel {

    constructor(height, width) {
        this._rows = [];
        this._height = 0;
        this._width = 0;

        this._init(height, width);
    }

    get width() {
        return this._width;
    }

    get height() {
        return this._height;
    }

    getCell(x, y) {
        if (!this._isValidCell(x, y)) {
            throw new Error('Incorrect position');
        }
        return this._getCell(x, y);
    }

    getRows() {
        return this._rows;
    }

    getColumns() {
        let columns = [];
        for (let i = 0; i < this.width; i++) {
            let col = [];
            for (let j = 0; j < this.height; j++) {
                col.push(this._getCell(i, j));
            }
            columns.push(col);
        }
        return columns;
    }

    getUpDiagonalies() {
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
    }

    getDownDiagonalies() {
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
    }

    reset() {
        for (let i = 0; i < this.width; i++) {
            for (let j = 0; j < this.height; j++) {
                this._getCell(i, j).state = CellState.NONE;
            }
        }
    }

    _init(height, width) {
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
    }

    _getCell(x, y) {
        return this._rows[y][x];
    }

    _isValidCell(x, y) {
        return !((x < 0 || x >= this.width) || (y < 0 || y >= this.height));
    }
}