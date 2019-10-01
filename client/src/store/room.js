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
    state.messages.push({
      username: message.username,
      message: message.message,
      messageId: state.nextMessageId,
      colorId: state.users.indexOf(message.username) % state.colors.length
    });
    state.nextMessageId++;
  },
  fetch(roomId) {
    return state.messages;
  },
  clear() {
    state.messages = [];
  }
};

export const getters = {
  messages: state => state.messages,
  users: state => state.users,
  colors: state => state.colors,
  colorId: state => state.colorId,
  roomId: state => state.roomId
};
