<template>
    <div id="app">
        <div class="top">
            <AppHeader class="header"></AppHeader>
            <div class="content">
                <nuxt />
            </div>
        </div>
        <AppFooter class="footer"></AppFooter>
    </div>
</template>

<script>
import AppHeader from "~/components/AppHeader.vue";
import AppFooter from "~/components/AppFooter.vue";

export default {
    components: {
        AppHeader,
        AppFooter
    },
    created() {
        this.$store.subscribe((mutation, state) => {
            console.log(mutation.type);
            if (mutation.type === "room/addMessage") {
                window.scroll(0, this.$el.scrollHeight);
            }
        });
    }
};
</script>

<style lang="scss">
html {
    height: 100%;
    background: #2a2a2e;
}
body {
    height: 100%;
    min-height: 100%;
    background: #2a2a2e;
}
header,
footer,
section {
    display: grid;
}
#__nuxt {
    min-height: 100%;
    height: 100%;
}
#__layout {
    min-height: 100%;
    height: 100%;
}
.top {
    display: grid;
    grid-template-rows: 1fr auto;
}
.wrapper {
    max-width: 1440px;
    width: 95%;
    align-self: center;
    justify-self: center;
}
header {
    min-height: 65px;
    max-height: 65px;
}
.header {
    width: 100%;
    border-bottom: 1px solid #4c4c51;
    max-height: 65px;
}
#app {
    min-height: 100%;
    display: grid;
    grid-template-rows: 1fr auto;
    font-family: Roboto;
    font-weight: 400;
    color: #cbcbcd;
}
.content {
    max-height: 60%;
}
footer {
    min-height: 50px;
}
.footer {
    border-top: 1px solid #4c4c51;
    grid-row-start: 2;
    grid-row-end: 3;
}
@media only screen and (max-width: 600px) {
    .wrapper {
        width: 96%;
    }
}
</style>