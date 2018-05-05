const signalR = require('@aspnet/signalr-client');

class ConnectionManager {

    constructor(options) {
        var defaultOptions = {
            hubUrl: '',
            onConnected: function () { },
            onGameStarted: function () { },
            onGameStopped: function () { },
            onStateUpdate: function () { }
        }

        this._options = _.extend({}, defaultOptions, options);
        this._connection = new signalR.HubConnection(this._options.hubUrl);
        this._connection.on('GameStarted', this._options.onGameStarted.bind(this));
        this._connection.on('GameStopped', this._options.onGameStopped.bind(this));
        this._connection.on('StateUpdate', this._options.onStateUpdate.bind(this));

        this._connection.start().then(this._options.onConnected);
    }

    connect(userName){
        this._connection.invoke('connect', userName);
    }

    sendUserStep(cellType, cellPosition) {

        this._connection.invoke('HandlePlayerAction', {
            CellType: cellType,
            CellPosition: {
                x: cellPosition.x,
                y: cellPosition.y
            }
        });
    }
}

module.exports = ConnectionManager;