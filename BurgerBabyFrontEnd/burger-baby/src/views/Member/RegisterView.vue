<template>
    <div class="d-flex  "
        style="justify-content:space-evenly; min-height: 100vh;align-items:center;flex-direction: column;">
        <div class="" style="display: flex;min-width: 100%;;font-size: 4em;margin-top: 1em;margin-bottom: 0.5em;">
            <label for="member-name"
                style="font-weight: 900; flex: 1;text-align: end;padding-right: 0.3em;">會員名稱:</label>
            <div style="flex: 2;"><input v-model="name" id="member-name" style="width: 15em;" autocomplete="off" type="text" /></div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4em;margin: 0.5em;">
            <label for="member-email"
                style="font-weight: 900; flex: 1;text-align: end;padding-right:  0.3em;">Email:</label>
            <div style="flex: 2;"><input v-model="email" id="member-email" style="width: 15em;"  autocomplete="off" type="email" /></div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4em;margin: 0.5em;">
            <label for="member-password"
                style="font-weight: 900; flex: 1;text-align: end;padding-right:  0.3em;">密碼:</label>
            <div style="flex: 2;"><input v-model="password" id="member-password" style="width: 15em;" autocomplete="off" type="password" />
            </div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4em;margin: 0.5em;">
            <label for="member-confirm-password"
                style="font-weight: 900; flex: 1;text-align: end;padding-right:  0.3em;">確認密碼:</label>
            <div style="flex: 2;"><input v-model="confirmpassword" id="member-confirm-password" style="width: 15em;"
                    type="password" autocomplete="off" /></div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4em;margin: 0.5em;">
            <label for="member-phone"
                style="font-weight: 900; flex: 1;text-align: end;padding-right: 0.3em;">電話:</label>
            <div style="flex: 2;"><input v-model="phone" id="member-phone" style="width: 15em;" autocomplete="off" type="text" /></div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4em;margin: 0.5em;">
            <label for="member-address"
                style="font-weight: 900; flex: 1;text-align: end;padding-right:  0.3em;">地址:</label>
            <div style="flex: 2;"><textarea v-model="address" name="" id="member-address" rows="2" cols="30"></textarea>
            </div>
        </div>
        <div class=""
            style="display: flex;min-width: 100%;;font-size: 4em;margin: 0.5em;align-items: center;justify-content: center;">
            <button class="btn btn-success" @click="register" style="font-size: 0.5em;">註冊</button>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
            name: "",
            email: "",
            password: "",
            confirmpassword: "",
            phone: "",
            address: ""
        }
    },
    methods: {

        register() {
            if (this.password == this.confirmpassword) {
                var url = "https://localhost:7266/register";
                 axios.post(url, {
                    name: this.name,
                    email: this.email,
                    password: this.password,
                    phone: this.phone,
                    address: this.address
                },{}).then(res => { alert(res.data),this.$router.push("/") }).catch(
                    err=>{alert(err.response.data)}  
                )
            }
            else {
                alert("請確認密碼與密碼是否相同")
            }
        }
    }
}
</script>

<style></style>