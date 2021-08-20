<template>
  <div
    :tabindex="tabindex"
    @click="$emit('btn-click')"
    :class="['button', subClass]"
    :id="id"
    ref="btnREF"
    v-on:keyup.enter="$emit('btn-click')"
    v-on:keydown="keydownOnSelect($event)"
  >
    {{ buttonText }}
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
    autoFocus: String,
    reFocus: Boolean,
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

  watch: {
    reFocus: function () {
      if (this.autoFocus == "true") {
        this.$refs.btnREF.focus();
      }
    },
  },
};
</script>

<style scoped>
@import url("../../css/base/button.css");
</style>
