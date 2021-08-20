import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import SlideUpDown from 'vue-slide-up-down';

Vue.use(VueAxios, axios)
Vue.config.productionTip = false
Vue.component('slide-up-down', SlideUpDown)

new Vue({
    render: h => h(App),
}).$mount('#app')