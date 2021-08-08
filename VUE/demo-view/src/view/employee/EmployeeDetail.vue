<template>
  <div
    id="popup"
    :class="{ 'd-none': dnone }"
    :employeeId="employeeId"
    :formMode="formMode"
    Item="EmployeeCode"
  >
    <div class="head">
      <div class="head-text">THÔNG TIN NHÂN VIÊN</div>
      <div class="head-close" @click="btnCancelonClick"></div>
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
              <label for="">Mã nhân viên<span class="cl-red">(*)</span></label
              ><br />
              <input
                type="text"
                FieldName="EmployeeCode"
                class="textbox-default"
                id="txtEmployeeCode"
                required
                v-model="employee.EmployeeCode"
              />
            </div>
            <div class="col">
              <label for="">Họ và tên<span class="cl-red">(*)</span></label
              ><br />
              <input
                type="text"
                FieldName="FullName"
                class="textbox-default"
                id="txtFullName"
                v-model="employee.FullName"
                required
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <label for="">Ngày sinh</label><br />
              <input
                type="date"
                FieldName="DateOfBirth"
                DataType="Date"
                class="textbox-default"
                id="txtDateOfBirth"
                v-model="employee.DateOfBirth"
              />
            </div>
            <div class="col">
              <label for="">Giới tính</label><br />
              <div class="select">
                <div
                  FieldName="Gender"
                  DataType="Enum"
                  EnumName="Gender"
                  class="inp"
                  id="txtGender"
                  Value="-1"
                >
                  Chưa chọn
                </div>
                <div class="X"></div>
                <div class="select-arrow rot-0"></div>
              </div>
              <div class="dropdown d-none">
                <div class="dropdown-item" Value="1">
                  <div class="dropdown-icon"></div>
                  <div class="dropdown-text">Nam</div>
                </div>
                <div class="dropdown-item" Value="0">
                  <div class="dropdown-icon"></div>
                  <div class="dropdown-text">Nữ</div>
                </div>
                <div class="dropdown-item" Value="2">
                  <div class="dropdown-icon"></div>
                  <div class="dropdown-text">Khác</div>
                </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <label for=""
                >Số CMTND/ Căn cước<span class="cl-red">(*)</span></label
              ><br />
              <input
                type="text"
                FieldName="IdentityNumber"
                DataType="OnlyNumber"
                class="textbox-default"
                id="txtIdentityNumber"
                required
                  v-model="employee.IdentityNumber"
              />
            </div>
            <div class="col">
              <label for="">Ngày cấp</label><br />
              <input
                type="date"
                FieldName="IdentityDate"
                DataType="Date"
                class="textbox-default"
                id="txtIdentityDate"
                  v-model="employee.IdentityDate"
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <label for="">Nơi cấp</label><br />
              <input
                type="text"
                FieldName="IdentityPlace"
                class="textbox-default"
                id="txtIdentityPlace"
                  v-model="employee.IdentityPlace"
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <label for="">Email<span class="cl-red">(*)</span></label
              ><br />
              <input
                type="text"
                FieldName="Email"
                class="textbox-default"
                id="txtEmail"
                required
                v-model="employee.Email"
              />
            </div>
            <div class="col">
              <label for="">Số điện thoại<span class="cl-red">(*)</span></label
              ><br />
              <input
                type="text"
                FieldName="PhoneNumber"
                DataType="OnlyNumber"
                class="textbox-default"
                id="txtPhoneNumber"
                required
                  v-model="employee.PhoneNumber"
              />
            </div>
          </div>
        </div>
        <div class="B">
          <div>B.THÔNG TIN CÔNG VIỆC</div>
          <div class="line"></div>
          <div class="row mt-8">
            <div class="col">
              <label for="">Vị trí</label><br />
              <Dropdown DropdownText="Tất cả vị trí" />
              <DropdownDetail
                dd_dropdown="dd-Position"
                Url="v1/Positions"
                itemId="PositionId"
                itemName="PositionName"
              />
            </div>
            <div class="col">
              <label for="">Phòng ban</label><br />
              <Dropdown DropdownText="Tất cả phòng ban" />
              <DropdownDetail
                dd_dropdown="dd-Department"
                Url="api/Department"
                itemId="DepartmentId"
                itemName="DepartmentName"
              />
            </div>
          </div>
          <div class="row">
            <div class="col">
              <label for="">Mã số thuế cá nhân</label><br />
              <input
                type="text"
                FieldName="PersonalTaxCode"
                DataType="OnlyNumber"
                class="textbox-default"
                id="txtPersonalTaxCode"
                 v-model="employee.PersonalTaxCode"
              />
            </div>
            <div class="col">
              <label for="">Mức lương cơ bản</label><br />
              <input
                type="text"
                FieldName="Salary"
                DataType="Number"
                id="txtSalary"
                class="textbox-default ta-r pd-19"
                maxlength="15"
                v-model= "employee.Salary"
              />
              <div class="currency">(VNĐ)</div>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <label for="">Ngày gia nhập công ty</label><br />
              <input
                type="date"
                FieldName="JoinDate"
                DataType="Date"
                class="textbox-default"
                id="txtJoinDate"
                 v-model="employee.JoinDate"
              />
            </div>
            <div class="col">
              <label for="">Tình trạng công việc</label><br />
              <div class="select">
                <div
                  FieldName="WorkStatus"
                  DataType="Enum"
                  EnumName="WorkStatus"
                  class="inp"
                  id="txtWorkStatus"
                  Value="-1"
                >
                  Chưa chọn
                </div>
                <div class="X"></div>
                <div class="select-arrow rot-0"></div>
              </div>
              <div class="dropdown d-none">
                <div class="dropdown-item" Value="0">
                  <div class="dropdown-icon"></div>
                  <div class="dropdown-text">Đang làm việc</div>
                </div>
                <div class="dropdown-item" Value="1">
                  <div class="dropdown-icon"></div>
                  <div class="dropdown-text">Nghỉ có phép</div>
                </div>
                <div class="dropdown-item">
                  <div class="dropdown-icon" Value="2"></div>
                  <div class="dropdown-text">Bị đuổi việc</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="foot">
      <Button
        @btn-click="btnSaveonClick"
        buttonText="Lưu"
        id="btnSave"
        subClass="save d-flex"
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
import axios from "axios";
import {CommonFn} from "../../js/mixins.js";

import Button from "../../components/base/BaseButton.vue";
import Dropdown from "../../components/base/BaseDropdown.vue";
import DropdownDetail from "../../components/base/BaseDropdownDetail.vue";
export default {
  mixins : [CommonFn],
  name: "EmployeeDetail",
  components: {
    Dropdown,
    DropdownDetail,
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
    formMode: Number,
  },

  data() {
    return {
      employee: {},
    };
  },
  watch: {
    employeeId: function (value) {
      let me = this;
      //Gọi Api lấy thông tin chi tiết:
      axios
        .get(`http://cukcuk.manhnv.net/v1/Employees/${value}`)
        .then((res) => {
          me.employee = res.data;
          me.employee["Salary"] = CommonFn.formatMoney(me.employee["Salary"]);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    formMode: function(){
      this.employee = {};
    }
  },

  methods: {
    /**
     * Hàm đóng popup
     * Dvanh 29/07/2021
     */
    btnCancelonClick() {
      let me = this;
      me.$emit("btnCancelonClick");
    },
    
    /**
     * Sự kiện bấm nút lưu
     * Dvanh 29/07/2021
     */
    btnSaveonClick() {
      if (this.formMode == 0) {
        axios
          .post(`http://cukcuk.manhnv.net/v1/Employees/` , this.employee)
          .then(() => {
            alert("Thêm thành công")
          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        axios
          .put(`http://cukcuk.manhnv.net/v1/Employees/${this.employeeId}` , this.employee)
          .then(() => {
            alert("Sửa thành công")
          })
          .catch((err) => {
            console.log(err);
          });
      }
    },
  },
};
</script>

<style scoped>
</style>