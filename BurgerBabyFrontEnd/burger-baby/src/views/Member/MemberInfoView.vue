<template>
    <div v-if="this.$store.state.isLoggedIn">
        <div class="p-5">
            <table class=" " style="width: 100%;">
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">會員名稱:</th>
                    <td v-if="!isEditName" class="" style="font-size: 3em; ">{{ data.name }}<a href="#"
                            @click.prevent="toggleMemberInfo('name')"><i class="fa-solid fa-pen-to-square fa-2xs ms-3"
                                style="color: #000;"></i></a></td>
                    <td v-else style="font-size: 3em;"><input type="text" v-model="data.name"> <button
                            @click="changeMemberInfo('name')">確認修改</button></td>
                </tr>
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">Email:</th>
                    <td class="" style="font-size: 3em; ">{{ data.email }}</td>
                </tr>
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">密碼:</th>
                    <td v-if="isEditPassword" class="" style="font-size: 3em; ">
                        <input v-model="password" type="password" placeholder="輸入新密碼">
                        <input v-model="confirmPassword" type="password" placeholder="確認新密碼">
                        <button @click="changeMemberInfo('password')">確認修改</button>
                    </td>
                    <td v-else class="" style="font-size: 3em; "><button
                            @click="toggleMemberInfo('password')">修改密碼</button>
                    </td>
                </tr>
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">電話:</th>
                    <td v-if="!isEditPhone" class="" style="font-size: 3em; ">{{ data.phone }}<a href="#"
                            @click.prevent="toggleMemberInfo('phone')"><i class="fa-solid fa-pen-to-square fa-2xs ms-3"
                                style="color: #000;"></i></a></td>
                    <td v-else style="font-size: 3em;"><input type="text" v-model="data.phone"> <button
                            @click="changeMemberInfo('phone')">確認修改</button></td>
                </tr>
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">地址:</th>
                    <td v-if="!isEditAddress" class="" style="font-size: 3em; ">{{ data.address }}<a href="#"
                            @click.prevent="toggleMemberInfo('address')"><i
                                class="fa-solid fa-pen-to-square fa-2xs ms-3" style="color: #000;"></i></a></td>
                    <td v-else style="font-size: 3em;"><input type="text" v-model="data.address"> <button
                            @click="changeMemberInfo('address')">確認修改</button></td>
                </tr>

            </table>
        </div>
    </div>
</template>
<script>
import axios from 'axios';
export default {
    data() {
        return {
            isEditName: false,
            isEditPhone: false,
            isEditAddress: false,
            isEditPassword: false,
            password: "",
            confirmPassword: "",
            tryCount: 0,
            data: {}
        }
    },
    methods: {

        toggleMemberInfo(toggleField) {
            switch (toggleField) {
                case 'name':
                    this.isEditName = true
                    break;
                case 'phone':
                    this.isEditPhone = true
                    break;
                case 'address':
                    this.isEditAddress = true
                    break;
                case 'password':
                    this.isEditPassword = true
                    break;
            }

        },
        async changeMemberInfo(changeField) {
            const url = `https://localhost:7266/change-memberinfo/`
            switch (changeField) {
                case 'name':
                    try {

                        if (this.data.name.length < 2) {
                            throw new Error("新名稱至少要2字以上");
                        }

                        const res = await axios.post(url, { name: this.name }, {
                            headers: {
                                authorization: "Bearer " + this.$store.state.accessToken
                            }, withCredentials: true
                        });
                        this.isEditName = false
                        alert(res.data)
                    }
                    catch (error) {
                        alert(error.message)
                    }
                    break;
                case 'phone':
                    try {

                        if (this.data.phone.length < 10) {
                            throw new Error("新電話號碼要10字以上");
                        }
                        const res = await axios.post(url, { phone: this.data.phone }, {
                            headers: {
                                authorization: "Bearer " + this.$store.state.accessToken
                            }, withCredentials: true
                        });
                        this.isEditPhone = false
                        alert(res.data)
                    }
                    catch (error) {
                        alert(error.message)
                    }
                    break;
                case 'address':
                    try {
                        const res = await axios.post(url, { address: this.data.address }, {
                            headers: {
                                authorization: "Bearer " + this.$store.state.accessToken
                            }, withCredentials: true
                        });
                        this.isEditAddress = false
                        alert(res.data)
                    }
                    catch (error) {
                        alert(error.message)
                    }
                    break;
                case 'password':
                    try {
                        if (this.password != this.confirmPassword) {
                            throw new Error("新密碼與確認密碼不同請重新輸入");
                        }
                        if (this.password.length < 2) {
                            throw new Error("新密碼至少要2字以上");
                        }

                        const res = await axios.post(url, { password: this.password }, {
                            headers: {
                                authorization: "Bearer " + this.$store.state.accessToken
                            }, withCredentials: true
                        });
                        this.isEditPassword = false
                        this.password = ""
                        this.confirmPassword = ""
                        alert(res.data)
                    }
                    catch (error) {
                        alert(error.message)
                    }
                    break;
            }

        }
        ,
        async changePassword() {
            try {
                if (this.password != this.confirmPassword) {
                    throw new Error("新密碼與確認密碼不同請重新輸入");
                }
                if (this.password.length < 2) {
                    throw new Error("新密碼至少要2字以上");
                }
                const url = `https://localhost:7266/change-password/`
                const res = await axios.post(url, { password: this.password }, {
                    headers: {
                        authorization: "Bearer " + this.$store.state.accessToken
                    }, withCredentials: true
                });
                this.isEditPassword = false
                this.password = ""
                this.confirmPassword = ""
                alert(res.data)
            }
            catch (error) {
                alert(error.message)
            }
        },
        getMember() {
            //更改測試網址
            const url = `https://localhost:7266/member`
            console.log(this.$store.state.memberInfo)
            axios.get(url, {
                headers: {
                    authorization: "Bearer " + this.$store.state.accessToken
                }
            }).then(res => {
                this.data = res.data
                this.tryCount = 0
                    
            }).catch(() => {
                if (this.tryCount == 0) {
                    this.tryCount++
                    this.$store.dispatch("getAccessToken").then(
                        () => { this.getMember() }
                    ).catch(() => { alert("錯誤請登入1"), this.goBackAfterTimeout() })
                }
                else {
                    alert("錯誤請登入")
                    this.goBackAfterTimeout()
                }
            })
        },
        goBackAfterTimeout() {
            setTimeout(() => {
                this.$router.go(-1);
            }, 100);
        }
    },
    watch:{
        '$store.state.isLoggedIn'(newVal){
            if(newVal==false)
            this.goBackAfterTimeout() 
        }
    },
    created() {
        this.getMember();
    },
    mounted() {

    }
}
</script>
<style scoped></style>