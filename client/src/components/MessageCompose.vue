<template>
    <section class="message-compose" v-on:keyup.enter="send">
        <div class="wrapper">
            <span class="username-group">
                <label id="username-label" class="username" for="username">
                    <font-awesome-icon :icon="['fas', 'user']" />
                </label>
                <input
                    id="username"
                    class="username"
                    type="text"
                    placeholder="username"
                    v-model="username"
                />
            </span>
            <span class="message-group">
                <label id="message-label" class="message" for="message">
                    <font-awesome-icon :icon="['fas', 'envelope']" />
                </label>
                <input
                    id="message"
                    class="message"
                    type="text"
                    placeholder="message..."
                    v-model="body"
                />
            </span>
            <span class="send-group">
                <button class="btn btn-info" id="send" v-on:click="send" v-on:keyup.enter="send">
                    <font-awesome-icon :icon="['fas', 'paper-plane']" />
                </button>
            </span>
        </div>
    </section>
</template>

<script>
export default {
    data: function() {
        return {
            body: "",
            username: ""
        };
    },
    methods: {
        send: function() {
            this.$store.commit("room/addMessage", {
                username: this.username,
                body: this.body
            });
        }
    }
};
</script>

<style scoped lang="scss">
.message-compose {
    border-top: 1px solid #4c4c51;
}
.wrapper {
    min-height: 50px;
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    grid-template-rows: 1fr;
}
input {
    height: 38px;
    border-radius: 0.25rem;
    padding: 0.375rem 0.75rem;
    box-shadow: none;
    border: none;
    color: #2f3030;
}
.btn,
.btn-info {
    border: none;
    background: #fff;
    border: 1px solid #17a2b8;
    color: #17a2b8;
    &:hover,
    &:active {
        background: #17a2b8;
        color: #17a2b8;
        border: 1px solid #17a2b8;
        box-shadow: none;
        outline: none;
    }
    &:focus {
        background: #fff;
        border: 1px solid #17a2b8;
        color: #17a2b8;
        box-shadow: none;
        outline: none;
    }
}
.username-group {
    display: grid;
    width: 100%;
    text-align: left;
    grid-template-columns: 20px auto;
    margin: auto;
    grid-column-start: 1;
    grid-column-end: 2;
}
.username {
    margin: auto;
    color: #cbcbcd;
}
#username {
    width: 90%;
    margin: auto;
    background: #2a2a2e;
}
.message-group {
    display: grid;
    width: 100%;
    text-align: center;
    grid-template-columns: 20px auto;
    margin: auto;
    grid-column-start: 2;
    grid-column-end: 4;
}
.message {
    margin: auto;
    color: #cbcbcd;
}
#message {
    width: 95%;
    margin: auto;
    background: #2a2a2e;
}
.send-group {
    display: grid;
    text-align: right;
    margin: auto;
    width: 100%;
    height: 100%;
    grid-column-start: 4;
    grid-column-end: 4;
}
#send {
    width: 100%;
    height: 80%;
    margin: auto;
    background: #2a2a2e;
}
@media only screen and (max-width: 600px) {
    .wrapper {
        display: grid;
        grid-template-rows: 1fr 1fr;
    }
    .username-group {
        grid-column-start: 1;
        grid-column-end: 2;
        grid-row-start: 1;
        grid-row-end: 1;
    }
    .message-group {
        grid-column-start: 1;
        grid-column-end: 3;
        grid-row-start: 2;
        grid-row-end: 2;
    }
    .send-group {
        grid-column-start: 4;
        grid-column-end: 4;
        grid-row-start: 2;
        grid-row-end: 2;
        min-height: 45px;
    }
}
</style>