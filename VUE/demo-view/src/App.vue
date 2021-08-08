<template>
  <div id="app">
      <EmployeeList/>
  </div>
</template>

<script>
import EmployeeList from "./view/employee/EmployeeList.vue"

export default {
  name: 'App',
  components: {
    EmployeeList
  }
}
</script>

<style>
  @import "./css/main.css";
  @import "./css/common/common.css";
</style>
