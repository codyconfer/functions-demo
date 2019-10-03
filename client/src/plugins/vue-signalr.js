import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import Vue from 'vue';
const axios = require("axios");

Vue.prototype.$connect = async (store) => {
    let connectionInfo = await axios.get(
        "https:functions-demo-notify.azurewebsites.net/api/negotiate"
    )
    let options = {
        accessTokenFactory: () => connectionInfo.data.accessToken,
    };
    const connection = new HubConnectionBuilder()
        .withUrl(`${connectionInfo.data.url}`, options)
        .configureLogging(LogLevel.Information)
        .build();
    connection.on('addMessage', (message) => {
        store.commit("room/addMessage", message)
    });
    connection.onclose(() => console.log('disconnected'));
    console.log('connecting...');
    connection.start()
        .then(() => console.log("connected"))
        .catch(console.error);
}