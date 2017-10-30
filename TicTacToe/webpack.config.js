"use strict";

{
    let path = require('path');

    const CleanWebpackPlugin = require('clean-webpack-plugin');
    const bundleFolder = "wwwroot/bundle/";

    module.exports = {
        entry: "./scripts/app/main.js",

        output: {
            filename: 'app.js',
            path: path.resolve(__dirname, bundleFolder),
            library: 'TicTacToe'
        },
        plugins: [
            new CleanWebpackPlugin([bundleFolder])
        ],
        module: {
            loaders: []
        }
    };
};