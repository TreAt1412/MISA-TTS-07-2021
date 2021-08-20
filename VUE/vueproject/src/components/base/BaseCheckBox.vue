<template>
  <div :class="['checkbox', {'checked': checked}]" @click ="$emit('clickCheckboxTd')" >
      
  </div>
</template>

<script>
export default {
  name: "BaseCheckBox",
  props: {
      checked: Boolean,
  },
};
</script>

<style></style>