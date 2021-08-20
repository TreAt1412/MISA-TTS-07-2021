<template>
  <div
    id="popup"
    :employeeId="employeeId"
    :formMode="formMode"
    Item="EmployeeCode"
    :class="['z-index-2', { 'd-none': dnone }]"
  >
    <div class="head">
      <div class="head-text">THÔNG TIN NHÂN VIÊN</div>
      <div class="head-close" @click="btnDialogCancelOnClick"></div>
    </div>
    <div class="main">
      <div class="avatar">
        <div class="image"></div>
        <div class="ta-center">(Vui lòng chọn ảnh có định</div>
        <div class="ta-center">dạng</div>
        <div class="ta-center">.jqg,.jepg,.png,.gif.)</div>
        <input class="d-none" type="file" name="" id="myFile" />
      </div>
      <div class="info">
        <div class="A">
          <div>A. THÔNG TIN CHUNG:</div>
          <div class="line"></div>
          <div class="row mt-8">
            <div class="col">
              <Input
                labelText="Mã nhân viên"
                tabindex="0"
                type="text"
                FieldName="EmployeeCode"
                obligate="true"
                autoFocus="true"
                ref="txtrequiredEmployeeCode"
                :reFocus="reFocus"
                v-model="employee.EmployeeCode"
                :initValue="employee.EmployeeCode"
              />
            </div>
            <div class="col">
              <Input
                labelText="Họ và tên"
                tabindex="0"
                type="text"
                FieldName="FullName"
                ref="txtrequiredFullName"
                obligate="true"
                v-model="employee.FullName"
                :initValue="employee.FullName"
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <Input
                labelText="Ngày sinh"
                tabindex="0"
                type="date"
                FieldName="DateOfBirth"
                obligate="false"
                ref="txtDateOfBirth"
                checkDate="true"
                v-model="employee.DateOfBirth"
                :initValue="employee.DateOfBirth"
              />
            </div>
            <div class="col">
              <Dropdown
                labelText="Giới tính"
                className="column"
                tabindex="0"
                defaultName=""
                itemId="Gender"
                ref="DropdownGender"
                itemName="GenderName"
                :selectedId="employee.Gender + ''"
                @chooseDropdownItem="chooseDropdownItem"
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <Input
                labelText="Số CMTND/ Căn cước"
                tabindex="0"
                type="text"
                placeholder="9-12 chữ số"
                onlyHasNumber="true"
                FieldName="txtIdentityNumber"
                obligate="true"
                ref="txtrequiredIdentityNumber"
                v-model="employee.IdentityNumber"
                :initValue="employee.IdentityNumber"
              />
            </div>
            <div class="col">
              <Input
                labelText="Ngày cấp"
                tabindex="0"
                type="date"
                FieldName="txtIdentityDate"
                obligate="false"
                checkDate="true"
                ref="IdentityDate"
                v-model="employee.IdentityDate"
                :initValue="employee.IdentityDate"
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <Input
                labelText="Nơi cấp"
                tabindex="0"
                type="text"
                FieldName="txtIdentityPlace"
                obligate="false"
                v-model="employee.IdentityPlace"
                :initValue="employee.IdentityPlace"
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <Input
                labelText="Email"
                tabindex="0"
                type="text"
                placeholder="youremail@gmail.com"
                FieldName="Email"
                obligate="true"
                ref="txtrequiredEmail"
                v-model="employee.Email"
                :initValue="employee.Email"
              />
            </div>
            <div class="col">
              <Input
                labelText="Số điện thoại"
                tabindex="0"
                type="text"
                placeholder="10 chữ số"
                onlyHasNumber="true"
                FieldName="PhoneNumber"
                obligate="true"
                ref="txtrequiredPhoneNumber"
                v-model="employee.PhoneNumber"
                :initValue="employee.PhoneNumber"
              />
            </div>
          </div>
        </div>
        <div class="B">
          <div>B.THÔNG TIN CÔNG VIỆC</div>
          <div class="line"></div>
          <div class="row mt-8">
            <div class="col">
              <Dropdown
                labelText="Vị trí"
                tabindex="0"
                :defaultName="employee.PositionName"
                dd_dropdown="dd-Position"
                Url="v1/Positions"
                itemId="PositionId"
                ref="DropdownPosition"
                :selectedId="employee.PositionId + ''"
                itemName="PositionName"
                @chooseDropdownItem="chooseDropdownItem"
              />
            </div>
            <div class="col">
              <Dropdown
                labelText="Phòng ban"
                tabindex="0"
                :defaultName="employee.DepartmentName"
                dd_dropdown="dd-Department"
                Url="api/Department"
                ref="DropdownDepartment"
                :selectedId="employee.DepartmentId + ''"
                itemId="DepartmentId"
                itemName="DepartmentName"
                @chooseDropdownItem="chooseDropdownItem"
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <Input
                labelText="Mã số thuế cá nhân"
                tabindex="0"
                type="text"
                FieldName="PersonalTaxCode"
                obligate="false"
                onlyHasNumber="true"
                ref="txtPersonalTaxCodeNumber"
                v-model="employee.PersonalTaxCode"
                :initValue="employee.PersonalTaxCode"
              />
            </div>
            <div class="col">
              <Input
                labelText="Mức lương cơ bản"
                tabindex="0"
                type="text"
                onlyHasNumber="true"
                FieldName="Salary"
                obligate="false"
                ref="txtSalaryNumber"
                subClass="ta-r pd-19"
                v-model="employee.Salary"
                :initValue="employee.Salary"
                @convertMoney="convertMoney"
              />
              <div class="currency">(VNĐ)</div>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <Input
                labelText="Ngày gia nhập công ty"
                tabindex="0"
                type="date"
                FieldName="txtJoinDate"
                obligate="false"
                checkDate="true"
                ref="JoinDate"
                v-model="employee.JoinDate"
                :initValue="employee.JoinDate"
              />
            </div>
            <div class="col">
              <Dropdown
                labelText="Tình trạng công việc"
                tabindex="0"
                defaultName=""
                itemId="WorkStatus"
                ref="DropdownWorkStatus"
                itemName="WorkStatusName"
                :selectedId="employee.WorkStatus + ''"
                @chooseDropdownItem="chooseDropdownItem"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="foot">
      <ButtonIcon
        tabindex="0"
        @btn-click="btnSaveOnClick"
        buttonText="Lưu"
        id="btnSave"
        subClass="save d-flex"
      />

      <Button
        tabindex="0"
        @btn-click="btnDialogCancelOnClick"
        buttonText="Hủy"
        id="btnCancel"
        subClass="cancel"
      />
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { CommonFn } from "../../js/mixins.js";

import ButtonIcon from "../../components/base/BaseButtonIcon.vue";
import Button from "../../components/base/BaseButton.vue";
import Input from "../../components/base/BaseInput.vue";
import Dropdown from "../../components/base/BaseDropdown.vue";

export default {
  mixins: [CommonFn],
  name: "EmployeeDetail",
  components: {
    Dropdown,
    ButtonIcon,
    Button,
    Input,
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
    formMode: Number,
    reopen: Boolean,
    response: String,
  },

  data() {
    return {
      employee: {},
      defaultName: "",
      selectedId: "",
      initValue: "",
      reFocus: false,
    };
  },

  watch: {
    reopen: function () {
      let me = this;
      //Gọi Api lấy thông tin chi tiết:
      if (me.formMode == 1) {
        axios
          .get(`http://cukcuk.manhnv.net/v1/Employees/${me.employeeId}`)
          .then((res) => {
            me.employee = res.data;
            me.employee.Salary = me.formatMoney(res.data.Salary);
            me.employee.DateOfBirth = me.convertDate(res.data.DateOfBirth);
            me.employee.JoinDate = me.convertDate(res.data.JoinDate);
            me.employee.IdentityDate = me.convertDate(res.data.IdentityDate);
          })
          .catch((err) => {
            console.log(err);
          });
      }
    },
    formMode: function () {
      if (this.formMode == 0) {
        this.resetPopup();
      }
    },
    dnone: function () {
      this.reFocus = !this.reFocus;
    },
    response: function () {
      let me = this;
      let responseMess = me.response.split("__")[0];
      if (responseMess == "Lưu") {
        me.employee.Salary = me.formatNumber(me.employee.Salary);
        //Thêm nhân viên mới
        if (me.formMode == 0) {
          axios
            .post(`http://cukcuk.manhnv.net/v1/Employees/`, me.employee)
            .then(() => {
              me.$emit(
                "callToastMessage",
                "Thêm dữ liệu thành công",
                "message-green"
              );
              me.$emit("resetAfterSaveData");
            })
            .catch((err) => {
              console.log(err);
            });
        }
        // sửa nhân viên
        else if (me.formMode == 1) {
          axios
            .put(
              `http://cukcuk.manhnv.net/v1/Employees/${me.employeeId}`,
              me.employee
            )
            .then(() => {
              me.$emit(
                "callToastMessage",
                "Sửa dữ liệu thành công",
                "message-green"
              );
              me.$emit("resetAfterSaveData");
            })
            .catch((err) => {
              console.log(err);
            });
        }
      }
    },
  },

  methods: {
    /**
     * Hàm reset form thêm
     * Dvanh 10/8/2021
     */
    resetPopup() {
      // cho object nhân viên về rỗng
      let newEntity = {};
      newEntity.DepartmentId = "";
      newEntity.PositionId = "";
      newEntity.Gender = -1;
      this.employee = newEntity;
      // gọi hàm lấy mã nhân viên mới
      this.getNewCode();
      // bỏ các tooltip cảnh báo nếu đang lỗi
      this.removeError();
    },

    /**
     * Hàm loại bỏ các tooltip cảnh báo
     * Dvanh 10/8/2021
     */
    removeError() {
      let me = this;
      for (let [key] of Object.entries(me.$refs)) {
        if (key.includes("txt")) me.$refs[key].removeError();
      }
    },

    /**
     * Hàm đóng popup
     * Dvanh 29/07/2021
     */
    btnDialogCancelOnClick() {
      let me = this;
      me.$emit("btnDialogCancelOnClick");
    },

    /**
     * Sự kiện bấm nút lưu
     * Dvanh 29/07/2021
     */
    btnSaveOnClick() {
      let me = this;

      if (me.validateForm()) {
        me.$emit("btnSaveOnClick", me.employee.FullName);
      }
    },

    /**
     * Lấy mã nhân viên mới
     * Dvanh 1/8/2021
     */
    getNewCode() {
      let me = this;
      axios
        .get(`http://cukcuk.manhnv.net/v1/Employees/NewEmployeeCode`)
        .then((res) => {
          let newEmployee = {};
          newEmployee.EmployeeCode = res.data;
          me.employee = newEmployee;
        })
        .catch((err) => {
          console.log(err);
        });
    },

    /**
     * Hàm kiển tra dữ liệu
     * Dvanh 2-6-2021
     */
    validateForm() {
      let me = this,
        isValid = true;

      if (isValid) {
        isValid = me.isRequired();
      }

      if (isValid) {
        isValid = me.validateDropdown();
      }

      if (isValid) {
        isValid = me.validateFieldNumber();
      }

      if (isValid) {
        isValid = me.validateFieldDate();
      }

      // if (isValid) {
      //     isValid = me.validateCode(formMode, itemId);
      // }

      if (isValid) {
        isValid = me.validateEmail();
      }

      if (isValid) {
        isValid = me.validatePhoneNumber();
      }

      return isValid;
    },

    /**
     * Hàm kiểm tra bắt buộc nhập các dữ liệu bắt buộc
     * Dvanh 8/8/2021
     */
    isRequired() {
      let me = this;
      let isValid = true;

      for (let [key] of Object.entries(me.$refs)) {
        if (key.includes("required")) {
          let valRes = me.$refs[key].isRequired();

          if (valRes == false) {
            me.$emit(
              "callToastMessage",
              "Không được để trống các trường bắt buộc",
              "message-red"
            );
            isValid = false;
          }
        }
      }

      return isValid;
    },

    validateDropdown() {
      let me = this;
      let isValid = true;

      for (let [key] of Object.entries(me.$refs)) {
        if (key.includes("Dropdown")) {
          let valRes = me.$refs[key].validateDropdown();
          //console.log(key, "|", valRes);
          if (valRes == false) {
            me.$emit(
              "callToastMessage",
              "Vui lòng chọn giá trị ở các ô có lựa chọn",
              "message-red"
            );
            isValid = false;
          }
        }
      }

      return isValid;
    },

    /**
     * Hàm kiểm tra chỉ chứa chữ số
     * Dvanh 9/8/2021
     */
    validateFieldNumber() {
      let me = this;
      let isValid = true;

      for (let [key] of Object.entries(me.$refs)) {
        if (key.includes("Number")) {
          let valRes = me.$refs[key].ContainNumber();

          if (valRes == false) {
            me.$emit(
              "callToastMessage",
              "Các ô sau chỉ chứa chữ số nhé",
              "message-red"
            );
            isValid = false;
          }
        }
      }

      return isValid;
    },

    /**
     * Hàm kiểm tra xem ngày tháng đúng định dạng không
     * Dvanh 9/8/2021
     */
    validateFieldDate() {
      let me = this,
        isValid = true;

      for (let [key] of Object.entries(me.$refs)) {
        if (key.includes("Date")) {
          let valRes = me.$refs[key].validateFieldDate();

          if (valRes == false) {
            me.$emit(
              "callToastMessage",
              "Ngày tháng không đúng định dạng",
              "message-red"
            );
            isValid = false;
          }
        }
      }

      return isValid;
    },

    /**
     * Hàm kiểm tra Email
     * Dvanh 8/8/2021
     */
    validateEmail() {
      let me = this,
        isValid = true,
        valRes = me.$refs.txtrequiredEmail.validateEmail();

      if (valRes == false) {
        me.$emit(
          "callToastMessage",
          "Email không đúng định dạng",
          "message-red"
        );
        isValid = false;
      }

      return isValid;
    },

    /**
     * Hàm kiểm tra số điện thoại
     * Dvanh 8/8/2021
     */
    validatePhoneNumber() {
      let me = this,
        isValid = true,
        valRes = me.$refs.txtrequiredPhoneNumber.validatePhoneNumber();

      if (valRes == false) {
        me.$emit(
          "callToastMessage",
          "Số điện thoại không đúng định dạng",
          "message-red"
        );
        isValid = false;
      }

      return isValid;
    },

    /**
     * Hàm tự động format tiền lương được nhập
     * Dvanh 2/8/2021
     */
    convertMoney() {
      let me = this;
      let val = me.employee.Salary;
      let val1 = me.formatNumber(val);
      me.employee.Salary = me.formatMoney(val1);
    },
    /**
     * Hàm lấy id của dropdown được chọn gán vào obj employee
     * Dvanh 31/07/2021
     */
    chooseDropdownItem(itemValue, itemID) {
      this.employee[itemID] = itemValue;
    },

    /**
     * Hàm format số tiền
     * Dvanh 18/7/2021
     */
    formatMoney(money) {
      if (money && !isNaN(money)) {
        return money.toString().replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.");
      } else {
        return money;
      }
    },

    /**
     * Chuyển đổi ngày tháng
     * Dvanh 18/7/2021
     */
    convertDate(dateSrc) {
      let dateString = "";
      if (dateSrc) {
        let date = new Date(dateSrc),
          year = date.getFullYear().toString(),
          month = (date.getMonth() + 1).toString().padStart(2, "0"),
          day = date.getDate().toString().padStart(2, "0");

        dateString = `${year}-${month}-${day}`;
      } else {
        dateString = "123";
      }
      return dateString;
    },

    /**
     * Hàm format số tiền về số bth
     * Dvanh 18/7/2021
     */
    formatNumber(money) {
      let salary = null;
      if (money != null) {
        salary = money.replaceAll(".", "");
      }

      return salary;
    },
  },
};
</script>

<style scoped>
</style>