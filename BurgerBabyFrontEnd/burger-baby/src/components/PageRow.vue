<template>
    <div style="display: flex;justify-content: center;align-items: center;">
        <div class="p-5">
            <div style="display: flex; flex-direction: row;">
                <ul v-if="data.totalPages && data.totalPages > 1" class="pagination justify-content-center ">
                    <li v-if="pageIndex == 1 && data.totalPages && data.totalPages > 1" class="page-item disabled">
                        <a class="page-link" href="#">上一頁</a>
                    </li>
                    <li v-if="pageIndex > 1 && data.totalPages > 1" class="page-item ">
                        <router-link class="page-link"
                            :to="`/Products?searchString=${searchString}&pageIndex=${pageIndex - 1}&pageSize=${pageSize}`">上一頁</router-link>
                    </li>


                    <li v-for="(ps, index) in data.pagesStringList" :key="index"
                        :class="['page-item', { active: ps == pageIndex }]">
                        <router-link v-if="ps == '1..'" class="page-link "
                            :to="`/Products?searchString=${searchString}&pageIndex=1&pageSize=${pageSize}`">{{ ps
                            }}</router-link>
                        <router-link v-else-if="ps == `..${data.totalPages}`" class="page-link "
                            :to="`/Products?searchString=${searchString}&pageIndex=${data.totalPages}&pageSize=${pageSize}`">{{
                                ps }}</router-link>
                        <router-link v-else class="page-link "
                            :to="`/Products?searchString=${searchString}&pageIndex=${ps}&pageSize=${pageSize}`">{{ ps
                            }}</router-link>
                    </li>


                    <li v-if="data.totalPages && pageIndex == data.totalPages && data.totalPages > 1"
                        class="page-item disabled">
                        <a class="page-link" href="#">下一頁</a>
                    </li>
                    <li v-if="data.totalPages && pageIndex < data.totalPages && data.totalPages > 1" class="page-item ">
                        <router-link class="page-link"
                            :to="`/Products?searchString=${searchString}&pageIndex=${pageIndex + 1}&pageSize=${pageSize}`">下一頁</router-link>
                    </li>

                </ul>
                <input v-if="data.totalPages && data.totalPages > 1" style="height: 38px;" id="pageIndexInput" type="text" placeholder="跳轉至" />
                <select style="height: 38px;" v-model="localPageSize" name="" id="">
                    <option v-for="(value,index) in pageSizeOptions" :value="value" :key="index">{{ "單頁商品顯示數量:"+value }}</option>
                </select>

            </div>
        </div>
    </div>


</template>

<script>
export default {
    name: 'PageRow',
    props: {
        searchString: {
            type: String,
            default: ""
        },
        pageSize: {
            type: Number,
            default: 1
        },
        pageIndex: {
            type: Number,
            default: 1
        },
        data: {
            type: Object,
            
        }
    },
    data() {
      return {
        localPageSize:1,
        pageSizeOptions:[1,2,5,10,100]
      }
    },
    watch: {
        localPageSize(newVal){
            this.$emit("page-size-changed",newVal)
        }
    }

}
</script>

<style>

</style>