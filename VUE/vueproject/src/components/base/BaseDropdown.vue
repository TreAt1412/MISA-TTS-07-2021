<template>
  <div :class="['p-relative',className]">
    <div :class="{'d-none' : !labelText}">
      <label for="">{{ labelText }}</label><br />
    </div>
    <ToolTip :hideToolTip="hideToolTip" :ToolTipText="ToolTipText"/>
    <div
      :class="['select', { notValid: hasNotValid }]"
      :tabindex="tabindex"
      v-on:keyup.enter="SelectOnClick"
      v-on:keydown="keydownOnSelect($event)"
      v-on:keyup.space="chooseDropdownItemByKey($event)"
      v-on-clickaway="closeDropDown"
    >
      <div class="inp" @click="SelectOnClick">{{ currentName }}</div>
      <div :class="['X', { 'v-hidden': HideX }]" @click="clearChoice"></div>
      <div
        :class="['select-arrow', rotate ? 'rot-180' : 'rot-0']"
        @click="SelectOnClick"
      ></div>
    </div>

    <div
      :class="['dropdown', dd_dropdown, { 'd-none': dnone }]"
      :Url="Url"
      :itemId="itemId"
      :itemName="itemName"
    >
      <slide-up-down :active="active" :duration="1000">
        <div
          v-for="(item, index) in items"
          :key="item[itemId]"
          :Value="item[itemId]"
          :class="['dropdown-item', index == currentIndex ? 'bg-select' : '']"
          @click="chooseDropdownItem(item[itemId], item[itemName], index)"
        >
          <div class="dropdown-icon"></div>
          <div class="dropdown-text">{{ item[itemName] }}</div>
        </div>
      </slide-up-down>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { mixin as clickaway } from "vue-clickaway";

import ToolTip from "./BaseToolTip.vue";
export default {
  mixins: [clickaway],
  name: "BaseDropdown",

  components: {
    ToolTip,
  },

  props: {
    defaultName: String,
    className: String,
    dd_dropdown: String,
    Url: String,
    itemId: String,
    itemName: String,
    selectedId: String,
    tabindex: String,
    labelText:String,
  },

  data() {
    return {
      //các department,position
      items: [],
      // nút X
      HideX: true,
      // quay mũi tên
      rotate: false,
      // ẩn/hiện các item
      dnone: true,
      // tên hiển thị
      currentName: this.defaultName,
      currentIndex: -1,
      //slidedown/up
      active: false,
      // border đỏ
      hasNotValid: false,
      //hiện/ẩn tooltip
      hideToolTip: true,
      //nội dung tooltip
      ToolTipText: "",
    };
  },
  methods: {
    /**
     * Hàm ngăn chặn sự kiện mặc định của nút tab, dùng thì mới thực hiện được nút enter
     * Dvanh 4/8/2021
     */
    keydownOnSelect(event) {
      if (event.code == "Enter") {
        event.preventDefault();
      }
      if (event.code == "ArrowDown") {
        event.preventDefault();
        this.currentIndex = this.currentIndex < 0 ? -1 : this.currentIndex;
        this.currentIndex = (this.currentIndex + 1) % this.items.length;
      } else if (event.code == "ArrowUp") {
        event.preventDefault();
        this.currentIndex = this.currentIndex < 0 ? 0 : this.currentIndex;
        this.currentIndex =
          this.currentIndex == 0
            ? this.items.length - 1
            : this.currentIndex - 1;
      }
    },

    /**
     * Sự kiện chọn 1 lựa chọn bằng phím
     * Dvanh 31/07/2021
     */
    chooseDropdownItemByKey(event) {
      if (event.code == "Space") {
        event.preventDefault();
        let item = this.items[this.currentIndex],
          itemName = item[this.itemName],
          itemValue = item[this.itemId];
        this.chooseDropdownItem(itemValue, itemName, this.currentIndex);
      }
    },

    /**
     * Sự kiện chọn 1 lựa chọn
     * Dvanh 31/07/2021
     */
    chooseDropdownItem(itemValue, itemName, index) {
      this.currentIndex = index;
      this.HideX = false;
      this.SelectOnClick();
      this.currentName = itemName;
      this.$emit("chooseDropdownItem", itemValue, this.itemId);
    },

    /**
     * Sự kiện bấm để hiện/ ẩn dropdown
     * Dvanh 31/07/2021
     */
    SelectOnClick() {
      this.rotate = !this.rotate;
      this.dnone = !this.dnone;
      this.active = !this.active;
    },

    /**
     * Sự kiện đóng dropdown(sử dụng vue-clickaway)
     * Dvanh 8/8/2021
     */
    closeDropDown() {
      this.rotate = false;
      this.dnone = true;
      this.active = false;
    },

    /**
     * Sự kiện bấm nút X để xóa lựa chọn
     * Dvanh 31/07/2021
     */
    clearChoice() {
      this.currentIndex = -1;
      this.HideX = true;
      this.currentName = this.defaultName;
      this.dnone = true;
      this.$emit("chooseDropdownItem", "", this.itemId);

      if (this.rotate) {
        this.rotate = !this.rotate;
      }
      if (this.active) {
        this.active = !this.active;
      }
    },

    /**
     * xem hàm sửa có gọi không để lưu giá trị vào dropdown
     * Dvanh 31/07/2021
     */
    setValueDropdown() {
      let me = this;
      // nếu selectedId(PostionId,DepartmentId) mà tồn tại(tức là được Employeelist truyền vào => form sửa dùng)
      if ((me.selectedId + "").length > 0) {
        //duyệt từng items(department,postion)
        me.items.forEach(function (item, index) {
          // nếu selectedId(PostionId,DepartmentId) từ cha truyền vào mà trùng với itemId(PostionId,DepartmentId) trong list
          if (me.selectedId == item[me.itemId]) {
            // gán currentIndex = index (để hàng tương ứng được tích)
            me.currentIndex = index;
            // hiện thị tên của itemName(PostionName,DepartmentName) của hàng được chọn
            me.currentName = item[me.itemName];
          }
        });
      } else {
        // form thêm dùng
        //không hàng nào được tích
        me.currentIndex = -1;
        // không hiển thị tên gì cả
        me.currentName = " ";
        // ẩn nút X
        me.HideX = true;
      }
    },

    /**
     * Hàm kiểm tra xem dropdown có được chọn hay không
     * Dvanh 8/8/2021
     */
    validateDropdown() {
      let me = this;
      if (me.currentIndex == -1) {
        me.hasNotValid = true;
        // hiện tooltip
        this.hideToolTip = false;
        this.ToolTipText = "Vui lòng chọn giá trị cho hộp này"
        return false;
      }

      me.hasNotValid = false;
      return true;
    },
  },

  created() {
    let me = this;
    switch (me.itemName) {
      case "GenderName":
        this.items = [
          {
            Gender: 0,
            GenderName: "Nữ",
          },
          {
            Gender: 1,
            GenderName: "Nam",
          },
          {
            Gender: 2,
            GenderName: "Khác",
          },
        ];
        break;
      case "WorkStatusName":
        this.items = [
          {
            WorkStatus: 0,
            WorkStatusName: "Chờ phỏng vấn",
          },
          {
            WorkStatus: 1,
            WorkStatusName: "Thử việc",
          },
          {
            WorkStatus: 2,
            WorkStatusName: "Đang làm việc",
          },
          {
            WorkStatus: 3,
            WorkStatusName: "Bị đuổi việc",
          },
        ];
        break;

      default:
        axios
          .get(`http://cukcuk.manhnv.net/${me.Url}`)
          .then((res) => {
            me.items = res.data;
          })
          .catch((res) => {
            console.log(res);
          });
        break;
    }

    me.currentName = me.defaultName;
  },

  watch: {
    selectedId: function () {
      this.setValueDropdown();
    },

    // currentIndex: function () {
    //   if (this.currentIndex != -1) {
    //     this.hasNotValid = false;
    //     this.hideToolTip = true;
    //   }

    //   if(this.currentIndex == -1){
    //     this.hasNotValid = true;
    //     // hiện tooltip
    //     this.hideToolTip = false;
    //     this.ToolTipText = "Vui lòng chọn giá trị cho hộp này"
    //   }
    // },
  },
  
};
</script>

<style></style>
