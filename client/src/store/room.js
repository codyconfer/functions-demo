const axios = require("axios");

export const state = () => ({
  messages: [],
  users: [],
  colors: [
    "#fbed80",
    "#44cf7a",
    "#dd2e31",
    "#1accff",
    "#ab307e",
    "#a9bb44",
    "#f96620",
    "#7575dd",
    "#007fff"
  ],
  roomId: 0,
  nextMessageId: 0
});

export const mutations = {
  addMessage(state, message) {
    if (!state.users.includes(message.username)) {
      state.users.push(message.username);
    }
    console.log(message);
    let entry = {
      username: message.Username,
      body: message.Body,
      messageId: state.nextMessageId,
      colorId: state.users.indexOf(message.username) % state.colors.length
    };
    console.log(entry);
    state.messages.push(entry);
    state.nextMessageId++;
  },
  fetch(roomId) {
    return state.messages;
  },
  clear() {
    state.messages = [];
  },
  setMessages(state, response) {
    state.messages = response.messages;
    state.roomId = response.roomId;
    state.nextMessageId = response.messages.length;
  }
};

export const actions = {
  async postMessageAction({state}, message) {
    let entry = {
      username: message.username,
      body: message.body,
      messageId: 0,
      colorId: 0
    };
    await axios.post(
      "https://functions-demo-mutate.azurewebsites.net/api/room/message",
      entry
    );
    state.nextMessageId++;
  },
}

export const getters = {
  messages: state => state.messages,
  users: state => state.users,
  colors: state => state.colors,
  colorId: state => state.colorId,
  roomId: state => state.roomId
};
