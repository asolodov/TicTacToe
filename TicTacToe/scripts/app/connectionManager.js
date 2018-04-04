const signalR = require('@aspnet/signalr-client');

class ConnectionManager {

    constructor(options) {
        var defaultOptions = {
            hubUrl: '',
            onConnected: function () { },
            onGameStarted: function () { },
            OnPlayerStepMessage: function () { }
        }

        this._options = _.extend({}, defaultOptions, options);
        this._connection = new signalR.HubConnection(this._options.hubUrl);
        this._connection.on('GameStarted', this._options.onGameStarted.bind(this));
        this._connection.on('onPlayerStep', this._options.OnPlayerStepMessage.bind(this));

        this._connection.start().then(this._options.onConnected);
    }

    connect(userName){
        this._connection.invoke('connect', userName);
    }

    sendUserStep(position) {
        this._connection.invoke('OnUserStep', position);
    }
}

module.exports = ConnectionManager;