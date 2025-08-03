<template>
  <div>
    <section class="main-section">
      <div class="d-flex" style="flex-direction: column;">
        <div class=" m-5 d-flex">
          <div class="carousel-area ">
            <div class="left-angle-area " v-show="data.imgs && currentImageIndex != 0" @click="changeImage(-1)">
              <button class="left-angle-btn" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                <i class="fa-solid fa-angle-left left-angle"></i>
              </button>
            </div>
            <div id="carouselExample" class="carousel slide carousel-img-area"
              style="width: 70em;height: 100%;position: absolute;">
              <div class="carousel-inner" style="height: 100%;">
                <div class="carousel-item active" style="height: 100%;">
                  <img class="carousel-img d-block" v-if="data.imgs && data.imgs.length > 0"
                    :src="`${this.$store.state.apiUrl}` + data.imgs[currentImageIndex].saveName"
                    @click="showImg(`${this.$store.state.apiUrl}` + data.imgs[currentImageIndex].saveName)"
                    style="object-fit:  cover;height: 100%;">
                </div>
              </div>
            </div>

            <div class="right-angle-area" v-show="data.imgs && currentImageIndex < data.imgs.length - 1"
              @click="changeImage(+1)">
              <button class="right-angle-btn" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                <i class="fa-solid fa-angle-right right-angle"></i>
              </button>
            </div>

          </div>
          <div class="intro-section" style="position: relative;">
            <div class="title p-1">{{ data.name }}</div>
            <div style="position: absolute;right: 0;bottom: 0;" class="info mt-3 p-5">
              <span style="font-size: 2em;">價格:</span>
              <span style="color: red;font-size: 2em;">{{ data.price }}</span>
            </div>
          </div>

        </div>

        <hr>
        <div class="intro ">
          <b class="p-5" style="font-size: 3em;">商品介紹</b>

          <p class="p-5" style="font-size: 2em;min-height: 30em;word-wrap: break-word;">
            {{ data.intro }}
          </p>
        </div>
      </div>

    </section>
    <ZoomView :imgUrl="imgUrl" :zoomVisible="zoomVisible" @hideZoomView="hideZoom()"></ZoomView>
  </div>
</template>

<script>
import ZoomView from '@/components/ZoomView.vue';
import axios from 'axios';
export default {
  components: {
    ZoomView
  },
  data() {
    return {
      tryCount: 0,
      zoomVisible: false,
      imgUrl: "",
      currentImageIndex: 0,
      data: {
        id: null,
        name: "",
        price: null,
        intro: "",
        imgs: []
      }
    }
  },
  methods: {
    getData() {

      const url = `${this.$store.state.apiUrl}/Product/${this.$route.params.id}`;
    axios.get(url)
        .then((res) => {
            this.data = res.data; 
        })
        .catch((ex) => {
            alert("錯誤: " + ex.message);
            this.goBackAfterTimeout(); 
        });
      
      // axios.get(url, {
      //   headers: {
      //     authorization: "Bearer " + this.$store.state.accessToken
      //   }
      // }
      // ).then(res => {
      //   this.data = res.data
      //   this.tryCount = 0
      // }).catch(() => {
      //   if (this.tryCount == 0) {
      //     this.tryCount++
      //     this.$store.dispatch("getAccessToken").then(
      //       () => { this.getData() }
      //     ).catch(() => { alert("錯誤請登入"), this.goBackAfterTimeout() })
      //   }
      //   else {
      //     alert("錯誤請登入")
      //     this.goBackAfterTimeout()
      //   }
      // })
    },
    changeImage(i) {
      this.currentImageIndex += i
    },
    showImg(imgUrl) {
      this.imgUrl = imgUrl;
      this.zoomVisible = true
    },
    hideZoom() {
      this.zoomVisible = false
    },
    goBackAfterTimeout() {
      setTimeout(() => {
        this.$router.go(-1);
      }, 100);
    }
  },
  mounted() {

    this.getData()

  }
}

</script>

<style>
.main-section {
  min-height: 100vh;
  min-width: 140em;
}
.carousel-area {
  width: 82em;
  position: relative;
  height: 50em;
  
}

.carousel-img {
  width: 80em;
  height: 100%;
  object-fit: cover;
}


.left-angle-area,
.right-angle-area {
  width: 6em;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  position: absolute;
}

.left-angle-btn,
.right-angle-btn {
  background-color: transparent;
  border: none;
  width: 100%;
  height: 100%;
}

.left-angle,
.right-angle {
  opacity: 0.5;
  color: rgb(145, 140, 140);
  font-size: 7em;
}

.left-angle-btn:hover .left-angle,
.right-angle-btn:hover .right-angle {
  opacity: 1;
}

.carousel-img-area {
  left: 6em;
  position: absolute;
}

.left-angle-area {
  left: 0;
}

.right-angle-area {
  right: 0;
}

.intro-section {
  vertical-align: top;
  margin-left: 7em;
  display: inline-block;
  min-width: 40em;
  height: 60em;



}

.title {
  font-weight: 1000;
  font-size: 4em;
  overflow-wrap: break-word;

}


.intro {
  margin: 5em 10em;
}
</style>