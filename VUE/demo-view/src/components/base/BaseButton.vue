<template >
    <div @click="$emit('btn-click')" :class="['button', subClass] " :id = "id">
      <div :class="['button-icon', iconName]"></div>
      <div class="button-text">{{ buttonText }}</div>
    </div>
</template>

<script>
export default {
  name: "BaseButton",
 
  props: {
    id: String,
    subClass: String,
    iconName: String,
    buttonText: String,
  },

  methods: {
    
  },
};
</script>

<style >
</style>