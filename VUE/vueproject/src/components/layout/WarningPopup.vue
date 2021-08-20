<template>

    <div :id="idPopup" :employeeId="employeeId" :class="[' z-index-12', { 'd-none': dnone }]">
      <div class="head">
        <div class="head-text">{{ warning }}</div>
        <div class="head-close" @click="btnCancelOnClick"></div>
      </div>
      <div class="main">
        <div class="warning"></div>
        <div class="text" v-html="warningText"></div>
      </div>
      <div class="foot">
        <Button
          tabindex="0"
          @btn-click="btnConfirmOnClick"
          :buttonText="btnConfirmText"
          id="btnConfirm"
          subClass="confirm"
          autoFocus = "true"
          :reFocus = "reFocus"
        />

        <Button
          tabindex="0"
          @btn-click="btnCancelOnClick"
          :buttonText="btnCancelText"
          id="btnCancel"
          subClass="cancel"
          :class="subClass"
        />
      </div>
    </div>
 
</template>

<script>
import Button from "../base/BaseButton.vue";

export default {
  name: "WarningPopup",
  components: {
    Button,
  },

  props: {
    dnone: {
      type: Boolean,
      default: true,
      require: true,
    },
    employeeId: {
      type: String,
      default: "",
      require: true,
    },
    warning: String,
    warningText: String,
    idPopup: String,
    btnCancelText: String,
    btnConfirmText: String,
    subClass: String,
  },

  data() {
    return {
      employee: {},
      reFocus: false,
    };
  },
  methods: {
    /**
     * Hàm đóng popup
     * Ngọc 29/07/2021
     */
    btnCancelOnClick() {
      let me = this;
      me.$emit("btnCancelOnClick");
    },
    /**
     * Hàm bấm nút xác nhận
     * Ngọc 29/07/2021
     */
    btnConfirmOnClick() {
      this.$emit("btnConfirmOnClick", this.idPopup);
    },

  },

  watch: {
    dnone: function () {
      this.reFocus = !this.reFocus;
    },
  },
};
</script>

<style scoped></style>
