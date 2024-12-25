<template>
    <form @submit.prevent="register" >
        <div class="d-flex  "
        style="justify-content:space-evenly; min-height: 100vh;align-items:center;flex-direction: column;">
        <div class="" style="display: flex;min-width: 100%;;font-size: 4rem;margin-top: 1rem;margin-bottom: 0.5rem;">
            <label for="member-name"
                style="font-weight: 900; flex: 1;text-align: end;padding-right: 0.3rem;">會員名稱:</label>
            <div style="flex: 2;"><input v-model="name" id="member-name" style="width: 60rem;" autocomplete="off" type="text" /></div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4rem;margin: 0.5rem;">
            <label for="member-email"
                style="font-weight: 900; flex: 1;text-align: end;padding-right:  0.3rem;">Email:</label>
            <div style="flex: 2;"><input v-model="email" id="member-email" style="width: 60rem;"  autocomplete="off" type="email" /></div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4rem;margin: 0.5rem;">
            <label for="member-password"
                style="font-weight: 900; flex: 1;text-align: end;padding-right:  0.3rem;">密碼:</label>
            <div style="flex: 2;"><input v-model="password" id="member-password" style="width: 60rem;" autocomplete="off" type="password" />
            </div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4em;margin: 0.5rem;">
            <label for="member-confirm-password"
                style="font-weight: 900; flex: 1;text-align: end;padding-right:  0.3rem;">確認密碼:</label>
            <div style="flex: 2;"><input v-model="confirmpassword" id="member-confirm-password" style="width: 60rem;"
                    type="password" autocomplete="off" /></div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4rem;margin: 0.5rem;">
            <label for="member-phone"
                style="font-weight: 900; flex: 1;text-align: end;padding-right: 0.3rem;">電話:</label>
            <div style="flex: 2;"><input v-model="phone" id="member-phone" style="width: 60rem;" autocomplete="off" type="text" /></div>
        </div>
        <div class="" style="display: flex;min-width: 100%;;font-size: 4rem;margin: 0.5rem;">
            <label for="member-address"
                style="font-weight: 900; flex: 1;text-align: end;padding-right:  0.3rem;">地址:</label>
            <div style="flex: 2;"><textarea v-model="address" name="" id="member-address" rows="2" cols="30"></textarea>
            </div>
        </div>
        <div class=""
            style="display: flex;min-width: 100%;;font-size: 4rem;align-items: center;justify-content: center;">
            <button class="btn btn-success" type="submit" style="font-size: 2rem;">註冊</button>
        </div>
    </div>
    </form>
   
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
            var valid=this.validMemberInfo()
            if (!valid) {
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
                alert(valid)
            }
        },
        validMemberInfo(){
            if(this.name.length<1){
                return"請輸入姓名"
            }
            //todo 正則
            if(this.email.indexOf('@')==-1)
            {
                return"請輸入正確Email"
            }
            if(this.password<1)
            {
                return "請輸入密碼"
            }
            if (!this.password == this.confirmpassword){
                return "請確認密碼與密碼是否相同"
            }
            if(!this.phone){
                return "請輸入電話"
            }
            if(!this.address)   
            {
                return "請輸入地址"
            }
            

        }
    }
}
</script>

<style></style>