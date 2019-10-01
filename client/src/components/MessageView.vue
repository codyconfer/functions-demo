<template>
    <section class="message-view">
        <div class="wrapper">
            <div v-for="(message, index) in messages" v-bind:key="index" class="message-group">
                <span
                    class="username"
                    id="username"
                    v-bind:style="{ color: colors[message.colorId] }"
                >{{message.username}}</span>
                <span class="message">{{message.body}}</span>
            </div>
        </div>
    </section>
</template>

<script>
export default {
    computed: {
        messages() {
            return this.$store.state.room.messages;
        },
        colors() {
            return this.$store.state.room.colors;
        },
        color(colorId) {
            return this.$store.state.room.colors[colorId];
        }
    }
};
</script>

<style scoped lang="scss">
.message-view {
    width: 100%;
    display: grid;
    min-height: 50px;
    margin: 20px auto;
}
.wrapper {
    min-height: 30px;
}
#username {
    font-weight: bold;
}
.username {
    font-size: 15px;
}
.message {
    font-size: 14px;
}
</style>