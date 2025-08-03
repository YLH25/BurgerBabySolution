import { createStore } from 'vuex'
import axios from 'axios'

export default createStore({
  state() {
    return {
      accessToken: "",
      isLoggedIn: false,
      memberInfo: {},
      apiUrl:"https://localhost:7266"
    }
  },
  getters: {


  },
  mutations: {
    changeAccessToken(state, jsonObj) {
      if(jsonObj!=""){
        let newJwt = JSON.parse(jsonObj)
        state.accessToken = newJwt;
      }
      else{
 state.accessToken=""
      }
     
    },
    changeLoginStatus(state, status) {
      state.isLoggedIn = status;
    },
    changeMemberInfo(state, jsonObj) {
      if (Object.keys(jsonObj).length !== 0) {
        let memberInfo = JSON.parse(jsonObj);
        state.memberInfo = memberInfo
      }
      else{
        state.memberInfo = {}
      }
    },
    changeMemberName(state,newName){
      if(state.memberInfo.name){
        state.memberInfo.name=newName;
      }
    }

  },
  actions: {
    async getAccessToken(store) {
      const url = `${this.state.apiUrl}/AccessToken`;
      let resData = {};
      try {
        const res = await axios.post(url, {}, { withCredentials: true });
        resData = res.data;
        store.commit("changeAccessToken", resData.jsonObjs[1]);
        store.commit("changeMemberInfo", resData.jsonObjs[0]);
        store.commit("changeLoginStatus", true); 
      } catch (err) {
        console.log(err.response ? err.response.status : "error");
        store.commit("changeLoginStatus", false);
        store.commit("changeMemberInfo", {});
      }
    }


  },
  modules: {

  }
})
