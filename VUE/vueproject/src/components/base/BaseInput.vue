<template>
  <div class="p-relative" >
    <div v-if="labelText.length > 0">
      <label for=""
        >{{ labelText }}
        <span v-if="obligate == 'true'"
          >(<span class="cl-red">*</span>)</span
        ></label
      ><br />
    </div>
    <ToolTip :hideToolTip="hideToolTip" :ToolTipText="ToolTipText" />
    <input
      :tabindex="tabindex"
      :type="type"
      :placeholder="placeholder"
      :FieldName="FieldName"
      :class="['textbox-default', subClass, { notValid: hasNotValid }]"
      ref="inputREF"
      :obligate="obligate"
      :onlyHasNumber="onlyHasNumber"
      :checkDate="checkDate"
      v-model="inputValue"
      @input="onInput($event.target.value)"
      @blur="onBlur($event.target.value)"
    />
    <div
      v-if="type == 'text'"
      :class="['input-clear-icon', { 'v-hidden': isHide }]"
      @click="clearInput"
    ></div>
  </div>
</template>

<script>
import { mixin as clickaway } from "vue-clickaway";

import ToolTip from "./BaseToolTip.vue";

export default {
  mixins: [clickaway],
  name: "BaseInput",
  components: {
    ToolTip,
  },

  props: {
    labelText: String,
    tabindex: String,
    type: String,
    placeholder: String,
    FieldName: String,
    initValue: String,
    subClass: String,
    // xác định tự động cho con trỏ chuột vào ô mã nhân viên
    autoFocus: String,
    reFocus: Boolean,
    // Xác định các trường chỉ chứa số
    onlyHasNumber: String,
    // xác định các trường bắt buộc
    obligate: String,
    // Xác định các trường ngày tháng
    checkDate: String,
  },
  data() {
    return {
      // Giá trị/Nội dung hiện tại
      inputValue: "",
      // Ẩn/hiện viền đỏ
      hasNotValid: false,
      // Ẩn/hiện tooltip
      hideToolTip: true,
      // Nội dung tooltip
      ToolTipText: "",
      // Ẩn/hiện icon X
      isHide: true,
    };
  },

  methods: {
    /**
     * Hàm xóa dữ liệu được nhập trong 1 ô
     * Dvanh 11/8/2021
     */
    clearInput() {
      // Xóa giá trị hiện tại
      this.inputValue = "";
      // Ẩn nút X
      this.isHide = true;
    },

    /**
     * Hàm bind dữ liệu vào input
     * Dvanh 7/8/2021
     */
    onInput(inputValue) {
      let me = this;
      //emit thắng vào v-model của cha
      me.$emit("input", inputValue);
      // Hiện icon X
      me.isHide = false;

      //format ô input tiền lương
      if (me.FieldName == "Salary") {
        me.$emit("convertMoney");
      }

      // Nếu các ô bắt buộc nhập đã có dữ liệu thì bỏ viền đỏ
      if (inputValue != "" && me.obligate == "true") {
        me.hasNotValid = false;
        me.hideToolTip = true;
      }

      // Nếu ô ngày tháng đã đúng thì bỏ viền đỏ
      if (
        inputValue != "" &&
        this.isDateFormat(inputValue) &&
        me.checkDate == "true"
      ) {
        me.hasNotValid = false;
        me.hideToolTip = true;
      }

      // Nếu các ô chỉ chứa chữ số đúng định dạng rồi thì bỏ viền đỏ
      if (
        !me.isNumber(me.formatNumber(inputValue)) &&
        me.onlyHasNumber == "true"
      ) {
        me.hasNotValid = false;
        me.hideToolTip = true;
      }

      //Nếu Email nhập đúng định dạng thì bỏ viền đỏ
      if (me.FieldName == "Email") {
        var at = inputValue.indexOf("@");
        var dot = inputValue.lastIndexOf(".");
        var space = inputValue.indexOf(" ");
        if (
          at != -1 && //có ký tự @
          at != 0 && //ký tự @ không nằm ở vị trí đầu
          dot != -1 && //có ký tự .
          dot > at + 1 &&
          dot < inputValue.length - 1 && //phải có ký tự nằm giữa @ và . cuối cùng
          space == -1 //không có khoẳng trắng
        ) {
          me.hasNotValid = false;
          me.hideToolTip = true;
        }
      }

      //Nếu số điện thoại đúng định dạng thì bỏ viền đỏ
      if (me.FieldName == "PhoneNumber") {
        if (inputValue.length == 10) {
          me.hasNotValid = false;
          me.hideToolTip = true;
        }
      }
    },

    /**
     * Hàm kiểm tra khi vừa nhập liệu xong
     * Dvanh 10/8/2021
     */
    onBlur(inputValue) {
      let me = this;
      // Nếu các ô bắt buộc không đã có dữ liệu thì hiện viền đỏ và tooltip
      if (inputValue == "" && me.obligate == "true") {
        me.hasNotValid = true;
        me.hideToolTip = false;
        me.ToolTipText = "Vui lòng điền vào trường này";
      }

      // Nếu ô ngày tháng không đúng thì hiện viền đỏ và tooltip
      if (
        inputValue != "" &&
        !this.isDateFormat(inputValue) &&
        me.checkDate == "true"
      ) {
        me.hasNotValid = true;
        me.hideToolTip = false;
        me.ToolTipText = "Ngày tháng chưa đúng định dạng";
      }

      // Nếu các ô chỉ chứa chữ số đúng định dạng rồi thì bỏ viền đỏ
      if (
        !me.isNumber(me.formatNumber(inputValue)) &&
        me.onlyHasNumber == "true"
      ) {
        me.hasNotValid = true;
        me.hideToolTip = false;
        me.ToolTipText = "Ô này chỉ chứa chữ só";
      }

      //Nếu Email nhập đúng định dạng thì bỏ viền đỏ
      if (me.FieldName == "Email") {
        var at = inputValue.indexOf("@");
        var dot = inputValue.lastIndexOf(".");
        var space = inputValue.indexOf(" ");
        if (
          at == -1 || //không có ký tự @
          at == 0 || //ký tự @ nằm ở vị trí đầu
          dot == -1 || //không có ký tự .
          (dot == at + 1 && dot < inputValue.length - 1) || // @ và . cuối cùng cạnh nhau
          space != -1 // có khoẳng trắng
        ) {
          me.hasNotValid = true;
          me.hideToolTip = false;
          me.ToolTipText = "Email không đúng định dạng";
        }
      }

      //Nếu số điện thoại đúng định dạng thì bỏ viền đỏ
      if (me.FieldName == "PhoneNumber") {
        if (!inputValue.length == 10) {
          me.hasNotValid = true;
          me.hideToolTip = false;
          me.ToolTipText = "Số điện thoại phải có 10 kí tự";
        }
      }
    },

    /**
     * Hàm kiểm tra bắt buộc nhập các dữ liệu bắt buộc
     * Dvanh 8/8/2021
     */
    isRequired() {
      let me = this,
        val = me.$refs.inputREF.value;

      if (me.obligate == "true" && val == "") {
        // Thêm border đỏ
        me.hasNotValid = true;
        // hiện tooltip
        me.hideToolTip = false;
        me.ToolTipText = "Vui lòng điền vào trường này";
        return false;
      }
      //bỏ boder đỏ
      me.hasNotValid = false;

      return true;
    },

    /**
     * Hàm kiểm tra các trường chỉ chứa chữ số
     * Dvanh 8/8/2021
     */
    ContainNumber() {
      let me = this,
        number = me.$refs.inputREF.value;
      let val = me.isNumber(me.formatNumber(number));

      if (!val && me.onlyHasNumber == "true") {
        //thêm border đỏ
        me.hasNotValid = true;
        // hiện tooltip
        me.hideToolTip = false;
        me.ToolTipText = "ô này chỉ chứa chữ số";
        return false;
      }
      //bỏ boder đỏ
      me.hasNotValid = false;

      return true;
    },

    validateFieldDate() {
      let me = this,
        date = me.$refs.inputREF.value;
      if (!date) return true;

      if (!me.isDateFormat(date)) {
        //thêm border đỏ
        me.hasNotValid = true;
        // hiện tooltip
        me.hideToolTip = false;
        me.ToolTipText = "Ngày tháng sai định dạng";
        return false;
      }
      //bỏ boder đỏ
      me.hasNotValid = false;

      return true;
    },

    /**
     * Hàm kiểm tra Email
     * Dvanh 8/8/2021
     */
    validateEmail() {
      let me = this;

      if (me.FieldName == "Email") {
        let value = this.$refs.inputREF.value;
        var at = value.indexOf("@");
        var dot = value.lastIndexOf(".");
        var space = value.indexOf(" ");
        if (
          at != -1 && //có ký tự @
          at != 0 && //ký tự @ không nằm ở vị trí đầu
          dot != -1 && //có ký tự .
          dot > at + 1 &&
          dot < value.length - 1 && //phải có ký tự nằm giữa @ và . cuối cùng
          space == -1 //không có khoẳng trắng
        ) {
          me.hasNotValid = false;
        } else {
          me.hasNotValid = true;
          // hiện tooltip
          me.hideToolTip = false;
          me.ToolTipText = "Email sai định dạng";
          return false;
        }
      }

      return true;
    },

    /**
     * Hàm kiểm tra số điện thoại
     * Dvanh 8/8/2021
     */
    validatePhoneNumber() {
      let me = this;

      if (me.FieldName == "PhoneNumber") {
        let value = this.$refs.inputREF.value;

        if (value.length != 10) {
          me.hasNotValid = true;
          // hiện tooltip
          me.hideToolTip = false;
          me.ToolTipText = "Số điện thoại phải có 10 kí tự";
          return false;
        }
      }
      //bỏ boder đỏ
      this.hasNotValid = false;

      return true;
    },

    /**
     * Hàm loại bỏ lỗi và tooltip cảnh báo khi mở lại form
     * Dvanh 10/8/2021
     */
    removeError() {
      let me = this;
      me.hasNotValid = false;
      me.hideToolTip = true;
      me.isHide = true;
    },

    /**
     * Hàm kiểm tra chuổi chỉ chứa chữ số
     * Dvanh 24/07/2021
     */
    isNumber(number) {
      let part = /[^0-9]/g,
        //mảng res chứa các phần tử thừa
        res = number.match(part);

      //Nếu res null (tức là không có phần tử thừa) => number truyền vào chỉ chứa số=> true
      if (!res) {
        return true;
      }
      return false;
    },

    /**
     * Hàm format số tiền về số bth(bỏ hết dấu '.')
     * Dvanh 18/7/2021
     */
    formatNumber(money) {
      let salary = money.replaceAll(".", "");
      return salary;
    },

    /**
     * Kiểm tra xem có phải dạng date không
     * Dvanh 18/7/2021
     */
    isDateFormat(date) {
      let regex = new RegExp(
        "(19[0-9]{2}|20[0-9]{2}[-](0[1-9]|1[0-2])[-]([0-2]{1}[0-9]{1}|3[0-1]{1})|([0-2]{1}[0-9]{1}|3[0-1]{1})[/](0[1-9]|1[0-2])[/][0-9]{4})"
      );
      return regex.test(date);
    },
  },
  watch: {
    initValue: function () {
      this.inputValue = this.initValue;
     
    },

    reFocus: function () {
      if (this.autoFocus == "true") {
        this.$refs.inputREF.focus();
      }
       
    },
  },
};
</script>

<style scoped>
</style>
