<template>
  <div class="d-flex p-relative">
    <input
      class="field-input-icon"
      type="text"
      placeholder= "Tìm theo mã, Tên hoặc Số điện thoại"
      v-model="valueFinding"
      @input="startFinding"
    />
    <div :class="['input-clear-icon',{'v-hidden': isHide}]" @click="clearFinding"></div>
  </div>
</template>

<script>
export default {
  name: "BaseFieldInputIcon",
  data(){
    return{
      isHide: true,
      valueFinding: "",
    }
  },

  methods: {
    startFinding(){
      this.isHide = false;
    },

    clearFinding(){
      this.isHide = true;
      this.valueFinding = "";
    }
  },
};
</script>

<style></style>
