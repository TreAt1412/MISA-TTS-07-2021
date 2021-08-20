<template>
  <div
    :tabindex="tabindex"
    @click="$emit('btn-click')"
    :class="['button', subClass]"
    :id="id"
    v-on:keyup.enter="$emit('btn-click')"
    v-on:keydown="keydownOnSelect($event)"
  >
    <div :class="['button-icon', iconName]"></div>
    <div class="button-text">{{ buttonText }}</div>
  </div>
</template>

<script>
export default {
  name: "BaseButton",

  props: {
    id: String,
    subClass: String,
    iconName: String,
    buttonText: String,
    tabindex: String,
  },

  methods: {
     /**
     * Hàm ngăn chặn sự kiện mặc định của nút tab, dùng thì mới thực hiện được nút enter
     * Dvanh 4/8/2021
     */
    keydownOnSelect(event) {
      if (event.Code == "Tab") {
        event.preventDefault();
      }
    },
  },
};
</script>

<style scoped>
  @import url("../../css/base/button-icon.css");
</style>
