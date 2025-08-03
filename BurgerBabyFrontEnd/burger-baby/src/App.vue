<template>
  <div>
    <nav class="navbar navbar-expand-md navbar-warning fixed-top bg-warning ">
      <router-link class="navbar-brand" to="/">
        <img id="food-icon" :style="{ width: '4em', height: '' }" src="../src//assets/foodicon.png" alt="">
      </router-link>

      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarCollapse"
        aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse justify-content-between" id="navbarCollapse">
        <ul class="navbar-nav mb-2 mb-md-0">
          <li style="" class="nav-item">
            <router-link style="font-size: 2em;" class="nav-link " to="/" active-class="active">主頁</router-link>
          </li>
          <li class="nav-item">
            <router-link style="font-size: 2em;" class="nav-link " to="/Products"
              active-class="active">商品</router-link>
          </li>
          <li v-if="this.$store.state.isLoggedIn" class="nav-item">
            <router-link style="font-size: 2em;" class="nav-link " to="/Member/1"
              active-class="active">會員</router-link>
          </li>
          <li v-else class="nav-item">
            <router-link style="font-size: 2em;" class="nav-link " to="/Register"
              active-class="active">註冊</router-link>
          </li>
        </ul>
        <form style="width: 50vw;" class="d-flex" role="search">
          <input v-model="searchString" class="form-control me-2 " type="search" placeholder="搜尋商品" aria-label="Search">
          <button style="width: 5em;" @click="search" class="btn btn-outline-success" type="submit">搜尋</button>
        </form>
        <div class="memberInfo me-5 p-1">
          <div v-if="$store.state.isLoggedIn" class="d-flex "> <span class="me-5 fs-1">{{
            'Hi,' +$store.state.memberInfo.name}}</span>
            <span class="d-flex align-items-center justufy-content-center"><button type="button" @click="logout" class="btn btn-success">會員登出</button></span>
          </div>
          <div v-else class="d-flex align-items-center justufy-content-center">
            <span class="d-flex me-5" >
              <div class="d-flex" style="flex-direction: column;">
                <div class="p-1">
                  <label style="width: 4.5em;" for="email">電子郵件:</label> <input v-model="email" id="email" type="email" placeholder="請輸入電子郵件">
                </div>
                <div class="p-1">
                <label style="width: 4.5em;" for="password">密碼:</label>   <input v-model="password" id="password" type="password" placeholder="請輸入密碼">
               </div>
              </div>
            </span>
            <span class="d-flex align-items-center justufy-content-center"><button type="button" class="btn btn-success" @click="login">會員登入</button></span>
          </div>
        </div>

      </div>

    </nav>
    <main>
      <router-view></router-view>
    </main>
    <footer class="bg-warning d-flex justify-content-center align-items-center">
      非商業用途
    </footer>
  </div>
</template>

<style>
main {
  margin-top: 6rem;
  min-height: 100vh;
}

body {
  min-height: 100vh;
  background-color: antiquewhite;


}

#food-icon {
  background-color: transparent;
}

footer {

  height: 8rem;
  bottom: 0;
}
</style>
<script>
import axios from 'axios';

export default {

  data() {
    return {
      email: "",
      password: "",
      searchString: "",
      data: {
      }
    }
  },
  methods: {
    search(event) {
      event.preventDefault();
      this.$router.push({
        path: `/Products`,
        query: {
          searchString: this.searchString,
          pageIndex: 1,
          pageSize: 1
        }
      }) 
    },
    getAccessToken(){
      this.$store.dispatch("getAccessToken").catch(err=>{console.log(err.response)})
    },
   
    login() {
      const url = `${this.$store.state.apiUrl}/login`;
      axios.post(url, {
        email: this.email,
        password: this.password
      },{withCredentials: true})
      .then(res => { 
        if(res.status==200)
      {
          this.email="",

          
        this.password=""
        this.getAccessToken()
      }

      }).catch(err => { console.log(err.response ? err.response.status : "error"), this.data = {} ,alert("錯誤請重新登入")});

    },
    logout(){
      const url = `${this.$store.state.apiUrl}/logout`
      axios.post(url,{

      },{withCredentials:true}).then(this.$store.commit("changeLoginStatus",false),this.$store.commit("changeAccessToken",""));
    }
  },
  computed:{
    
  },
  watch:{
    
  },

  mounted() {
    this.getAccessToken();
   
  }
}


</script>
