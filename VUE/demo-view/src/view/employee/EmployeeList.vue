<template>
  <div>
    <Header />
    <Menu />
    <div class="content">
      <div class="content-header">
        <div class="title">Danh sách nhân viên</div>
        <div class="d-flex">
          <Button
            @btn-click="btnDeleteonClick"
            iconName="icon-delete"
            buttonText="Xóa nhân viên"
            id="btnDelete"
          />
          <Button
            @btn-click="btnAddonClick"
            iconName="icon-add"
            buttonText="Thêm nhân viên"
            id="btnAdd"
          />
        </div>
      </div>
      <div class="filter">
        <div class="d-flex">
          <FieldInputIcon />
          <div class="department">
            <Dropdown DropdownText="Tất cả phòng ban" />
            <DropdownDetail
              dd_dropdown="dd-Department"
              Url="api/Department"
              itemId="DepartmentId"
              itemName="DepartmentName"
            />
          </div>
          <div class="position">
            <Dropdown DropdownText="Tất cả vị trí" />
            <DropdownDetail
              dd_dropdown="dd-Position"
              Url="v1/Positions"
              itemId="PositionId"
              itemName="PositionName"
            />
          </div>
        </div>
        <div class="refresh"></div>
      </div>
      <div id="gridEntity">
        <table>
          <thead>
            <tr>
              <th>Mã nhân viên</th>
              <th>Họ và tên</th>
              <th>Giới tính</th>
              <th>Ngày sinh</th>
              <th>Điện thoại</th>
              <th>Email</th>
              <th>Chức vụ</th>
              <th>Phòng ban</th>
              <th>Mức lương cơ bản</th>
              <th>Tình trạng công việc</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(employee, index) in employees"
              :key="employee.EmployeeId"
              @dblclick="onDoubleClick(employee.EmployeeId)"
              @click="trOnClick(index)"
              :class="{ 'tr-select': isSelected[index] }"
            >
              <td>{{ employee.EmployeeCode }}</td>
              <td>{{ employee.FullName }}</td>
              <td>{{ employee.GenderName }}</td>
              <td>{{ convertDate(employee.DateOfBirth) }}</td>
              <td>{{ employee.PhoneNumber }}</td>
              <td>{{ employee.Email }}</td>
              <td>{{ employee.PositionName }}</td>
              <td>{{ employee.DepartmentName }}</td>
              <td>{{ formatMoney(employee.Salary) }}</td>
              <td>{{ employee.WorkStatus }}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="page-navigator">
        <!-- <div class="ml-10" id="div1-paging"></div> -->
        <div class="ml-10">Hiển thị 1-10/1000 nhân viên</div>
        <div class="paging">
          <div class="btn common-page first-page"></div>
          <div class="btn common-page prev-page"></div>
          <div class="btn page-number">1</div>
          <div class="btn page-number">2</div>
          <div class="btn page-number">3</div>
          <div class="btn page-number">4</div>
          <div class="btn common-page next-page"></div>
          <div class="btn common-page last-page"></div>
        </div>
        <div class="mr-10" id="div2-paging">10 nhân viên/trang</div>
      </div>
    </div>
    <EmployeeDetail
      v-bind:dnone="DialogHasDnone"
      :employeeId="employeeId"
      :formMode="formMode"
      @btnCancelonClick="btnCancelonClick"
    />
    <WarningPopup
      v-bind:dnone="WarningHasDnone"
      :employeeId="employeeId"
      @btnCancelonClick="btnCancelonClick"
      :warning="warning"
      :warningText="warningText"
      @btnConfirmonClick="btnConfirmonClick"
    />
  </div>
</template>

<script>
import axios from "axios";
import {CommonFn} from "../../js/mixins.js";

import Header from "../../components/layout/TheHeader.vue";
import Menu from "../../components/layout/TheMenu.vue";
import Button from "../../components/base/BaseButton.vue";
import FieldInputIcon from "../../components/base/BaseFieldInputIcon.vue";
import Dropdown from "../../components/base/BaseDropdown.vue";
import DropdownDetail from "../../components/base/BaseDropdownDetail.vue";

import EmployeeDetail from "./EmployeeDetail.vue";
import WarningPopup from "./WarningPopup.vue";
export default {
  mixins : [CommonFn],
  name: "EmployeeList",
  components: {
    Header,
    Menu,
    Button,
    Dropdown,
    DropdownDetail,
    FieldInputIcon,
    EmployeeDetail,
    WarningPopup,
  },
  data() {
    return {
      employees: [],
      DialogHasDnone: true,
      employeeId: null,
      formMode: 0,
      isSelected: [],
      WarningHasDnone: true,
      warning: "",
      warningText: "",
    };
  },
  methods: {
    /**
     * Hàm mở popup
     * Dvanh 29/07/2021
     */
    btnAddonClick() {
      let me = this;
      me.DialogHasDnone = false;
      this.formMode = 0;
    },
    /**
     * Hàm đóng popup
     * Dvanh 29/07/2021
     */
    btnCancelonClick() {
      let me = this;
      me.DialogHasDnone = true;
      me.WarningHasDnone = true;
    },

    /**
     * Hàm mở popup để sửa
     * Dvanh 29/07/2021
     */
    onDoubleClick(employeeId) {
      this.DialogHasDnone = false;
      this.employeeId = employeeId;
      this.formMode = 1;
    },

    /**
     * Hàm chọn tr
     * Dvanh 29/07/2021
     */
    trOnClick(index) {
      this.$set(this.isSelected, index, !this.isSelected[index]);
    },

    btnDeleteonClick() {
      let me = this;
      var count = 0;
      this.isSelected.forEach((selected) => {
        if (selected) {
          count = count + 1;
        }
      });
      if (count > 1) {
        me.warning = `Xóa thông tin của ${count} nhân viên`;
        me.warningText = `Xác nhận xóa thông tin của ${count} nhân viên này không`;
      } else {
        let fullname = "";
        this.isSelected.forEach((selected, index) => {
          if (selected) {
            fullname = me.employees[index].FullName;
          }
        });
        me.warning = `Xóa thông tin của nhân  viên ${fullname}`;
        me.warningText = `Xác nhận xóa thông tin của nhân viên ${fullname} `;
      }
      me.WarningHasDnone = false;
    },
    btnConfirmonClick() {
      let me = this;
      this.isSelected.forEach((selected, index) => {
        if (selected) {
          me.employeeId = me.employees[index]["EmployeeId"];
          axios
            .delete(`http://cukcuk.manhnv.net/v1/Employees/${me.employeeId}`)
            .then(() => {})
            .catch((err) => {
              console.log(err);
            });
        }
      });
      alert("Xóa thành công");
      me.WarningHasDnone = true;
    },
  },

  created() {
    let me = this;
    axios
      .get("http://cukcuk.manhnv.net/v1/Employees")
      .then((res) => {
        me.employees = res.data;
        me.employees.forEach(() => {
          me.isSelected.push(false);
          
        });
        console.log(me.isSelected);
      })
      .catch((res) => {
        console.log(res);
      });
  },
};
</script>

<style>
</style>
