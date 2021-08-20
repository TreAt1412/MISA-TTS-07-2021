<template>
  <div :class="['tooltip' ,{'d-none': hideToolTip}]">
    <div class="tooltip-text">{{ToolTipText}}</div>
    <div class="tooltip-icon"></div>
  </div>
</template>

<script>
export default {
  name: "BaseToolTip",

  props: {
    ToolTipText: String,
    hideToolTip: Boolean,
  },

  methods: {
  },

  watch: {
  
  },
};
</script>

<style scoped>

</style>