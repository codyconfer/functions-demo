<template>
    <section class="messenger">
        <MessageView class="message-view"></MessageView>
        <MessageCompose class="message-compose"></MessageCompose>
    </section>
</template>

<script lang="ts">
import MessageView from "~/components/MessageView.vue";
import MessageCompose from "~/components/MessageCompose.vue";

export default {
    layout: "default",
    components: {
        MessageView,
        MessageCompose
    },
    head() {
        return {
            title: "Functions Demo",
        };
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