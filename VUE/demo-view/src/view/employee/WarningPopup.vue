<template>
  <div
    id="warning-popup"
    :class="{ 'd-none': dnone }"
    :employeeId= "employeeId"
  >
    <div class="head">
      <div class="head-text">{{warning}}</div>
      <div class="head-close" @click="btnCancelonClick"></div>
    </div>
    <div class="main">
      <div class="warning"></div>
      <div class="text">{{warningText}}</div>
    </div>
    <div class="foot">
      <Button
        @btn-click="btnConfirmonClick"
        buttonText="Xóa"
        id="btnConfirm"
        subClass="confirm d-flex"
      />

      <Button
        @btn-click="btnCancelonClick"
        buttonText="Hủy"
        id="btnCancel"
        subClass="cancel d-flex"
      />
    </div>
  </div>
</template>

<script>

import Button from "../../components/base/BaseButton.vue";

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
  },

  data() {
    return {
      employee: {},
    };
  },
  methods: {
    /**
     * Hàm đóng popup
     * Dvanh 29/07/2021
     */
    btnCancelonClick() {
      let me = this;
      me.$emit("btnCancelonClick");
         //me.dnone = true;
    },
    /**
     * Hàm bấm nút xác nhận xóa
     * Dvanh 29/07/2021
     */
    btnConfirmonClick() {
     this.$emit("btnConfirmonClick");
    },
  },
};
</script>

<style scoped>
</style>