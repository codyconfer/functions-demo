<template>
    <section class="messenger">
        <MessageView ref="messageview" class="message-view"></MessageView>
        <MessageCompose class="message-compose"></MessageCompose>
    </section>
</template>

<script>
import MessageView from "~/components/MessageView.vue";
import MessageCompose from "~/components/MessageCompose.vue";
const axios = require("axios");

export default {
    async fetch({ store, params }) {
        let { data } = await axios.get(
            "https://functions-demo-read.azurewebsites.net/api/room/messages?roomId=0"
        );
        store.commit("room/setMessages", data);
    },
    layout: "default",
    components: {
        MessageView,
        MessageCompose
    },
    head() {
        return {
            title: "Functions Demo"
        };
    },
    mounted: function() {
        this.$connect(this.$store);
    }
};
</script>

<style lang="scss">
.messenger {
    display: grid;
    grid-template-rows: auto 1fr;
    min-height: 100%;
}
.message-view {
    display: grid;
}
.message-compose {
    max-height: 80px;
}
@media only screen and (max-width: 600px) {
    .message-compose {
        max-height: 120px;
    }
}
</style>