<template>
     <div :class="['menu' ,{'expand' : isToggle}, {'narrow': !isToggle}]" >
            <div class="menu-item">
                <div class="menu-item-icon dashboard"></div>
                <div :class="['menu-item-text', {'v-hidden': !isToggle}]">Tổng quan</div>
            </div>
            <div class="menu-item">
                <div class="menu-item-icon report"></div>
                <div :class="['menu-item-text', {'v-hidden': !isToggle}]">Báo cáo</div>
            </div>
            <div class="menu-item">
                <div class="menu-item-icon buy"></div>
                <div :class="['menu-item-text', {'v-hidden': !isToggle}]">Mua hàng</div>
            </div>
            <div class="menu-item">
                <div class="menu-item-icon employee"></div>
                <div :class="['menu-item-text', {'v-hidden': !isToggle}]">Danh mục nhân viên</div>
            </div>
            <div class="menu-item">
                <div class="menu-item-icon employee"></div>
                <div :class="['menu-item-text', {'v-hidden': !isToggle}]">Danh mục khách hàng</div>
            </div>
            <div class="menu-item">
                <div class="menu-item-icon setting"></div>
                <div :class="['menu-item-text', {'v-hidden': !isToggle}]">Thiết lập hệ thống</div>
            </div>
            <div :class="['toggle-menu', isToggle ?'rot-0':'rot-180' ]" @click="$emit('toggleMenu')"></div>
    </div>
</template>

<script>
export default {
  name: 'Menu',
  props: {
    isToggle: Boolean,
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    @import url("../../css/layout/menu.css");
</style>