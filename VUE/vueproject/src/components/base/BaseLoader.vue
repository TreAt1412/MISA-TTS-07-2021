<template>
   <div :class="['gears' , {'d-none' : HideLoader}]">
        <img src="../../assets/icon/biggear.png" alt="gear" class="big">

        <img src="../../assets/icon/smallgear.png" alt="gear" class="small">
        <h1>Chờ một chút...</h1> 
    </div>
</template>

<script>
export default {
  name: "BaseLoader",

  props: {
    HideLoader: Boolean
  },

  methods: {
 
  },

  watch: {
   
  },
};
</script>

<style scoped>

</style>
