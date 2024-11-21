<template>
    <div >
        <div style="min-height:calc( 100vh - 16em );">
            <div class="album py-4" >
                <div class="container-fluid">
                    <div id="imgcontainer"
                        class="row  row-cols-sm-2 row-cols-md-3 row-cols-lg-4 row-cols-xl-5 g-3 ">
                        <div class="col" v-for="(item, index) in data.items" :key=" index">
                            <div class="card shadow-sm">
                                <img class="cardImg" :style="{ objectFit: 'cover', height: '24em' }"
                                    :src="getCover(item) ?? ''" @click="showImg(getCover(item)??'')">
                                <div class="card-body">
                                    <b style="font-size:1.5em;" v-text="item.name" class="ProductName"> </b>

                                    <p :style="{ height: '2em' }" class="card-text">{{ (item.intro ?? '').substring(0,
                                        40)
                                        }}{{ item.intro &&
                                            item.intro.length > 20 ? '...' : '' }}</p>
                                    <div class="d-flex justify-content-between align-items-center">
                                        <div class="btn-group">


                                            <router-link :to="`/Product/${item.id}`"> <button :id="item.id"
                                                    type="button" class="btn btn-sm btn-outline-secondary"> 商品詳情
                                                </button></router-link>

                                        </div>
                                        <small class="text-body-secondary">{{ "$" + item.price }}</small>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div style="width: 100%; display: flex;">
                <div style="flex: 1; justify-content: center;">
                    <PageRow :searchString="searchString" :pageSize="pageSize" :pageIndex="pageIndex" :data="data"
                        @page-size-changed="pageSizeChanged"></PageRow>
                    <router-view />
                </div>

            </div>
            <ZoomView :imgUrl="imgUrl" :zoomVisible="zoomVisible" @hideZoomView="hideZoom()"></ZoomView>
    </div>
</template>

<script>
import ZoomView from '@/components/ZoomView.vue';
import PageRow from '@/components/PageRow.vue';
import axios from 'axios';
export default {
    components: {
        PageRow,
        ZoomView
    },
   
    data() {
        return {
            zoomVisible:false,
            imgUrl:"",
            searchString: '',
            pageSize: 1,
            pageIndex: 1,
            data: {
                totalPages: 0,
                items: [
                    {
                        id: null,
                        name: '',
                        price: 0,
                        imgs: [
                            {
                                id: null,
                                productId: null,
                                imgName: null,
                                saveName: null,
                                path: null,
                                isCover: false,
                                product: null
                            }

                        ],
                        intro: null
                    }
                ],
                pagesStringList: [],
            }


        }

    },
    methods: {
        getData() {
            var url = `https://localhost:7266/Products`;
            if (this.$route.query.searchString) {
                this.searchString = this.$route.query.searchString;

            }
            else {
                this.searchString = '';

            }
            if (this.$route.query.pageSize) {
                this.pageSize = parseInt(this.$route.query.pageSize);
            }
            else {
                this.pageSize = 1;
            }
            if (this.$route.query.pageIndex) {
                this.pageIndex = parseInt(this.$route.query.pageIndex);
            }
            else {
                this.pageIndex = 1;
            }
            axios.get(url, {
                params: {
                    searchString: this.searchString,
                    pageSize: this.pageSize,
                    pageIndex: this.pageIndex
                },
               
                // withCredentials:true
            }).then(res => { this.data = res.data; console.log(this.data) })
                .catch(err => {
                    alert(err.toString());

                })
        },
        getCover(item) {
            if (item && item.imgs && item.imgs.length > 0) {
                const coverImage = item.imgs.find(x => x.isCover === true);
                if (coverImage) {
                    return `https://localhost:7266/${coverImage.saveName}`;
                }
            }
            return '';
        },
        pageSizeChanged(newVal) {
            this.$router.replace({
                path: this.$router.path,
                query: {
                    searchString: this.$route.query.searchString ?? "",
                    pageIndex: 1,
                    pageSize: newVal
                }
            })
        },
        showImg(imgUrl){
            this.imgUrl=imgUrl;
            this.zoomVisible=true
        },
        hideZoom(){
            this.zoomVisible=false
        }
    },
    computed: {
       
    },

    watch: {
        '$route.query'() {
            this.getData()
        },
    },

    updated() {
    },

    mounted() {
        this.getData()

    },

}


</script>

<style>


</style>