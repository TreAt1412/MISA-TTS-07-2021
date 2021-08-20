<template>
  <div class="page-navigator">
    <!-- <div class="ml-10" id="div1-paging"></div> -->
    <div class="ml-10">
      Hiển thị <b>{{ EntityPerPage }}/{{ totalEntity }}</b> nhân viên
    </div>
    <div class="paging">
      <div
        class="btn common-page first-page"
        @click="pageNumberOnClick('first-page')"
      ></div>
      <div
        class="btn common-page prev-page"
        @click="pageNumberOnClick('prev-page')"
      ></div>
      <div
        v-for="index in totalPageNumber"
        :key="index"
        :class="[
          'btn page-number',
          { 'd-none': index < lowerLimit || index > upperLimit },
          { 'page-selected': index == currentPageNumber },
        ]"
        @click="pageNumberOnClick(index)"
      >
        {{ index }}
      </div>
      <div
        class="btn common-page next-page"
        @click="pageNumberOnClick('next-page')"
      ></div>
      <div
        class="btn common-page last-page"
        @click="pageNumberOnClick('last-page')"
      ></div>
    </div>
    <div class="mr-10 d-flex NumberPerPage">
      <div><b>{{ EntityPerPage}}&nbsp;</b>nhân viên/trang</div>
      <div class="modifyNumber">
        <div
          class="increaseNumber"
          @click="modifyNumber('increaseNumber')"
        ></div>
        <div
          class="decreaseNumber"
          @click="modifyNumber('decreaseNumber')"
        ></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "BasePageNavigation",
  props: {
    totalEntity: String,
    totalPageNumber: Number,
 
  },

  data() {
    return {
      selectedPage: false,
      totalShow: 5,
      currentPageNumber: 1,
      EntityPerPage: 20,
      lowerLimit: 1,
      upperLimit: 5,
    };
  },

  methods: {
    /**
     * Hàm dùng để chuyển trang
     * Ngọc 12/8/2021
     */
    pageNumberOnClick(btnPage) {
      let me = this;
      switch (btnPage) {
        case "first-page":
          me.currentPageNumber = 1;
          break;
        case "prev-page":
          if (me.currentPageNumber > 1) me.currentPageNumber -= 1;
          break;
        case "next-page":
          if (me.currentPageNumber < me.totalPageNumber)
            me.currentPageNumber += 1;
          break;
        case "last-page":
          me.currentPageNumber = me.totalPageNumber;
          break;
        default:
          me.currentPageNumber = btnPage;
          break;
      }  
      me.$emit("UpdatePage", me.currentPageNumber, me.EntityPerPage);
      me.updatePageNumber();
    },

    /**
     * Hàm cập nhật trang đang được xem
     * Ngọc 12/8/2021
     */
    updatePageNumber() {
      let me = this;
      //var currentPage = lowerLimit = upperLimit = Math.min(4, totalPages));
      me.lowerLimit = me.upperLimit = me.currentPageNumber;// = 3 
      for (var b = 1; b < me.totalShow && b < me.totalPageNumber; ) {
        if (me.lowerLimit > 1) {
          me.lowerLimit--;//=2
          b++;//2
        }
        if (b < me.totalShow && me.upperLimit < me.totalPageNumber) {
          me.upperLimit++;//4
          b++;//3
        }
      }
    },

    /**
     * Hàm tăng/giảm số lượng thực thể theo trang
     * Ngọc 12/8/2021
     */
    modifyNumber(modifyStatus) {
      let me = this;
      switch (modifyStatus) {
        case "increaseNumber":
          if (me.EntityPerPage <= me.totalEntity - 10) me.EntityPerPage += 10;
          break;
        case "decreaseNumber":
          if (me.EntityPerPage > 10) me.EntityPerPage -= 10;
          break;
        default:
          break;
      }
      me.$emit("UpdatePage", me.currentPageNumber, me.EntityPerPage);
      //console.log("tăng/giảm");
    },
  },

  created() {
    this.updatePageNumber();
  },
};
</script>

<style></style>