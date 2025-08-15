<template>
  <div v-if="this.$store.state.isLoggedIn">
    <div class="p-5">
      <table class=" " style="width: 100%">
        <tr>
          <th
            style="width: 10em; font-size: 3em; text-align: end; padding: 1em"
          >
            會員名稱:
          </th>
          <td v-if="!isEditName" class="" style="font-size: 3em">
            {{ this.$store.state.memberInfo.name
            }}<a href="#" @click.prevent="toggleMemberInfo('name')"
              ><i
                class="fa-solid fa-pen-to-square fa-2xs ms-3"
                style="color: #000"
              ></i
            ></a>
          </td>
          <td v-else style="font-size: 3em">
            <input type="text" v-model="data.name" />
            <button @click="changeMemberInfo('name')">確認修改</button>
          </td>
        </tr>
        <tr>
          <th
            style="width: 10em; font-size: 3em; text-align: end; padding: 1em"
          >
            Email:
          </th>
          <td class="" style="font-size: 3em">
            {{ this.$store.state.memberInfo.email }}
          </td>
        </tr>
        <tr>
          <th
            style="width: 10em; font-size: 3em; text-align: end; padding: 1em"
          >
            密碼:
          </th>
          <td v-if="isEditPassword" class="" style="font-size: 3em">
            <input
              v-model="password"
              type="password"
              placeholder="輸入新密碼"
            />
            <input
              v-model="confirmPassword"
              type="password"
              placeholder="確認新密碼"
            />
            <button @click="changeMemberInfo('password')">確認修改</button>
          </td>
          <td v-else class="" style="font-size: 3em">
            <button @click="toggleMemberInfo('password')">修改密碼</button>
          </td>
        </tr>
        <tr>
          <th
            style="width: 10em; font-size: 3em; text-align: end; padding: 1em"
          >
            電話:
          </th>
          <td v-if="!isEditPhone" class="" style="font-size: 3em">
            {{ this.$store.state.memberInfo.phone
            }}<a href="#" @click.prevent="toggleMemberInfo('phone')"
              ><i
                class="fa-solid fa-pen-to-square fa-2xs ms-3"
                style="color: #000"
              ></i
            ></a>
          </td>
          <td v-else style="font-size: 3em">
            <input type="text" v-model="data.phone" />
            <button @click="changeMemberInfo('phone')">確認修改</button>
          </td>
        </tr>
        <tr>
          <th
            style="width: 10em; font-size: 3em; text-align: end; padding: 1em"
          >
            地址:
          </th>
          <td v-if="!isEditAddress" class="" style="font-size: 3em">
            {{ this.$store.state.memberInfo.address
            }}<a href="#" @click.prevent="toggleMemberInfo('address')"
              ><i
                class="fa-solid fa-pen-to-square fa-2xs ms-3"
                style="color: #000"
              ></i
            ></a>
          </td>
          <td v-else style="font-size: 3em">
            <input type="text" v-model="data.address" />
            <button @click="changeMemberInfo('address')">確認修改</button>
          </td>
        </tr>
      </table>
    </div>
  </div>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      isEditName: false,
      isEditPhone: false,
      isEditAddress: false,
      isEditPassword: false,
      password: "",
      confirmPassword: "",
      data: {
        name: "",
        phone: "",
        address: "",
        email: "",
      },
    };
  },
  methods: {
    toggleMemberInfo(toggleField) {
      switch (toggleField) {
        case "name":
          this.isEditName = true;
          break;
        case "phone":
          this.isEditPhone = true;
          break;
        case "address":
          this.isEditAddress = true;
          break;
        case "password":
          this.isEditPassword = true;
          break;
      }
    },
    async changeMemberInfo(changeField) {
      console.log(1)
      const url = `${this.$store.state.apiUrl}/member`;
      let payload = {};
      switch (changeField) {
        case "name":
          if (this.data.name.length < 2) {
            alert("新名稱至少要2字以上");
            return;
          }
          payload = { name: this.data.name };
          break;

        case "phone":
          if (this.data.phone.length < 10) {
            alert("新電話號碼要10字以上");
            return;
          }
          payload = { phone: this.data.phone };
          break;

        case "address":
          if (!this.data.address) {
            alert("地址不能為空");
            return;
          }
          payload = { address: this.data.address };
          break;
        case "password":
          if (this.password !== this.confirmPassword) {
            alert("新密碼與確認密碼不同，請重新輸入");
            return;
          }
          if (this.password.length < 2) {
            alert("新密碼至少要2字以上");
            return;
          }
          payload = { password: this.password };
          break;
      }
      try {
        console.log(2)
        const res = await this.updateMember(url, payload);
        if (changeField === "name") this.isEditName = false;
        if (changeField === "phone") this.isEditPhone = false;
        if (changeField === "address") this.isEditAddress = false;
        if (changeField === "password") {
          this.isEditPassword = false;
          this.password = "";
          this.confirmPassword = "";
        }
        
          alert(res.data);
          await this.$store.dispatch("getMember");
          this.getMemberInfo();
      } catch (err) {
        alert(err.message);
        await this.$store.commit("changeAccessToken", "");
        await this.$store.commit("changeLoginStatus", false);
      }
    },

    async updateMember(url, obj) {
      try {
        const res = await axios.patch(url, obj, {
          headers: {
            authorization: "Bearer " + this.$store.state.accessToken,
          },
          withCredentials: true,
        });
        return res;
      } catch (err) {
        if (err.response?.status == 401) {
          try {
            const accessTokenIsGottern = await this.$store.dispatch(
              "getAccessToken"
            );
            if (!accessTokenIsGottern) {
              throw new Error("取得AccessToken失敗");
            }
            const res = await axios.patch(url, obj, {
              headers: {
                authorization: "Bearer " + this.$store.state.accessToken,
              },
              withCredentials: true,
            });
            return res;
          } catch (err2) {
            throw new Error("編輯會員資料發生錯誤原因為" + err2);
          }
        } else {
          throw new Error(err);
        }
      }
    },
    getMemberInfo(){
       const member = this.$store.state.memberInfo || {};
      this.data.name = member.name || "";
      this.data.phone = member.phone || "";
      this.data.address = member.address || "";
      this.data.email = member.email || "";
      console.log(member)
    }
  },
  watch: {
    "$store.state.isLoggedIn"(newVal) {
      if (newVal == false) {
        this.$store.dispatch("goToHomePage");
      }
    },
  },
  created() {
    this.getMemberInfo();
  },
  mounted() {},
};
</script>
<style scoped></style>
