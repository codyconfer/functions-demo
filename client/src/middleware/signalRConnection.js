export default function ({ store }) {
  const apiBaseUrl = "";
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${apiBaseUrl}/api`)
    .configureLogging(signalR.LogLevel.Information)
    .build();
  connection.on('newMessage', addMessage);
  connection.onclose(() => console.log('disconnected'));
  console.log('connecting...');
  connection.start()
    .then(() => data.ready = true)
    .catch(console.error);
}
//<script src="https://cdn.jsdelivr.net/npm/@aspnet/signalr@1.1.2/dist/browser/signalr.js"></script>
function addMessage(message) {
  this.$store.commit("room/addMessage", message);
}