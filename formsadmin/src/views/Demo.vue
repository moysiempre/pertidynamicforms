<template>
  <div class="col-md-4 offset-md-4">
    <h1>{{title}}</h1>
    <div class="form-gr">
      <datepicker name="fecha"></datepicker>
    </div>
    <div class="form-group">
      <label class="mb-0" for>Ubicación del archivo a subir</label>
      <input
        type="file"
        class="w-100"
        id="file"
        ref="file"
        :disabled="isloading"
        v-on:change="handleFileUpload"
      >
    </div>
    <div class="form-group">
      <button type="button" class="btn btn-primary" v-show="hasFile" :disabled="isloading" @click="upload">
        <span class="text-transparents">Download</span>
        <BtnLoader :isloading="isloading"/>
      </button>
    </div>
 
  </div>
</template>
<script>
import axios from "axios";
import Datepicker from "vuejs-datepicker";
import BtnLoader from "@/components/BtnLoader.vue";

export default {
  components: { Datepicker, BtnLoader },
  data() {
    return {
      title: "SUBIR ARCHIVOS",
      isloading: false,
      hasFile: false,
      filesx: [],
      file: {}
    };
  },
 mounted(){
     this.filesx = this.$refs.file.files[0]
 },
  methods: {
    testLoader() {
      this.isloading = true;
      setTimeout(() => {
        this.isloading = false;
      }, 2000);
    },
    handleFileUpload() {
      this.file = this.$refs.file.files[0];
      this.hasFile = true;
    },
    upload() {
      this.isloading = true;       
      let formData = new FormData();
      formData.append("file", this.file);
      console.log("file handled", this.file.name);
      axios
        .post("api-filemanager/upload", formData, {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        })
        .then(() => {
          console.log("SUCCESS!!");
          this.isloading = false;
          this.hasFile = false;
          this.$snotify.success("this.body for snotify SUCCESS");
          $("#file").val("");
        })
        .catch(() => {
          console.log("FAILURE!!");
          this.isloading = false;
           this.hasFile = false;
          this.$snotify.error("this.body for snotify FAILURE");
        });
    },
    downloadFile() {
      axios
        .get("api-filemanager/download")
        .then(() => {
          console.log("SUCCESS!!");
          this.$snotify.success("this.body for snotify SUCCESS");
        })
        .catch(() => {
          console.log("FAILURE!!");
          this.$snotify.error("this.body for snotify FAILURE");
        });
    },
    
  }
};
</script>
