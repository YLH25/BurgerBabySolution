<template>
    <div :style="zoomViewAttr" @click="hideZoomView">
        <img :src="imgUrl" alt="" srcset="">
    </div>
</template>

<script>
export default {
    props: {
        imgUrl: {
            default:"",
            type:String
        },
        zoomVisible: {
            type: Boolean
            , default: false
        }
    },
    data() {
        return {
            zoomViewAttr: {
                top: 0,
                left: 0
                , display: "flex"
                , "align-items": "center"
                , "justify-content": "center"
                , "background-color": "rgba(0, 0, 0, 0.9)"
                , height: "100vh"
                , width: "100vw"
                , visibility: "hidden"
                , position: "fixed"
                ,"z-index":1031
            },

        }
    },
    methods: {
        hideZoomView() {
            this.$emit("hideZoomView")
        }
    },
    watch: {
        zoomVisible(newVal) {   
            if (newVal) {
                this.zoomViewAttr.visibility = "visible"
            }
            else{
                 this.zoomViewAttr.visibility = "hidden"
            }
        }
    }
}
</script>

<style>

</style>