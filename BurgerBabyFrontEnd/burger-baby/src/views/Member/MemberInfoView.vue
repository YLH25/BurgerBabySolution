<template>
    <div v-if="this.$store.state.isLoggedIn">
        <div class="p-5">
            <table class=" " style="width: 100%;">
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">會員名稱:</th>
                    <td class="col" style="font-size: 3em; ">{{ data.name }}</td>
                </tr>
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">Email:</th>
                    <td class="col" style="font-size: 3em; ">{{ data.email }}</td>
                </tr>
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">密碼:</th>
                    <td v-if="isEditPassword" class="col" style="font-size: 3em; ">
                        <input v-model="password" type="password" placeholder="輸入新密碼">
                        <input v-model="confirmPassword" type="password" placeholder="確認新密碼">
                        <button @click="changePassword">確認修改</button>
                    </td>
                    <td v-else class="col" style="font-size: 3em; "><button @click="changeisEditPassword">修改密碼</button>
                    </td>
                </tr>
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">電話:</th>
                    <td class="col" style="font-size: 3em; ">{{ data.phone }}</td>
                </tr>
                <tr>
                    <th style="width:10em;font-size: 3em;text-align:end;padding: 1em;">地址:</th>
                    <td class="col" style="font-size: 3em; ">{{ data.address }}</td>
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
            isEditPassword: false,
            password: "",
            confirmPassword: "",
            tryCount: 0,
            data: {}
        }
    },
    methods: {
        changeisEditPassword() {
            this.isEditPassword = true
        },
        async changePassword() {
            try {
                if (this.password != this.confirmPassword) {
                    throw new Error("新密碼與確認密碼不同請重新輸入");
                }
                const url = `https://localhost:7266/change-password/`
                const res =await axios.post(url,{newPassword:this.confirmPassword}, {
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

            // const url = `https://localhost:7266/Member/Index/${this.$route.params.id}`
            const url = `https://localhost:7266/member/1`
            axios.get(url, {
                headers: {
                    authorization: "Bearer " + this.$store.state.accessToken
                }
            }).then(res => {
                this.data = res.data
                this.tryCount = 0,
                    console.log(this.data)
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
    mounted() {
        this.getMember();
    }
}
</script>
<style scoped></style>