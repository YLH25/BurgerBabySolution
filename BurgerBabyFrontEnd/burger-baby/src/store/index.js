import { createStore } from "vuex";
import axios from "axios";
import router from "@/router";

export default createStore({
  state() {
    return {
      accessToken: "",
      isLoggedIn: false,
      memberInfo: {},
      apiUrl: "https://localhost:7266",
    };
  },
  getters: {},
  mutations: {
    changeAccessToken(state, accessToken) {
      state.accessToken = accessToken;
    },
    changeLoginStatus(state, status) {
      state.isLoggedIn = status;
    },
    changeMemberInfo(state, member) {
      if (Object.keys(member).length !== 0) {
        state.memberInfo = member;
      } else {
        state.memberInfo = {};
      }
    },
    addTryCount(state) {
      state.tryCount++;
    },
    resetTryCount(state) {
      state.tryCount = 0;
    },
  },
  actions: {
    async getAccessToken(store) {
      const url = `${store.state.apiUrl}/accessToken`;
      try {
        const res = await axios.post(url, {}, { withCredentials: true });
        const accessToken = res.data;
        store.commit("changeAccessToken", accessToken);
        return true;
      } catch  {
        return false;
      }
    },
    async getMember(store) {
      const url = `${store.state.apiUrl}/member`;
      try {
        const res = await axios.get(url, {
          headers: {
            authorization: "Bearer " + store.state.accessToken,
          },
        });
        store.commit("changeMemberInfo", res.data);
        return true;
      } catch {
      return false;
      }
    },
    async goToHomePage() {
      alert("錯誤請登入");
      setTimeout(() => {
        router.push("/");
      }, 100);
    },
  },
  modules: {},
});
