 Vue.component('containerEncapsulated', {
     props: ['pageid'],
     data() {
         return {
             baseUrl: "http://localhost:60829/landing/",
             formData: {
                 id: ""
             },
             fields: [],
             isloading: false
         }
     },
     created() {
         this.load();
     },
     methods: {
         load() {
             axios.get(this.baseUrl + 'api-forms/landingPageId/' + this.pageid).then((response) => {
                 this.fields = response.data;
                 console.log(response.data);
             }).catch(error => {
                 console.log(error);
             });;
         },
         onSubmit: function () {
             this.$validator.validateAll().then(isValid => {
                 if (isValid) {
                     this.isloading = true;
                     let infoRequestData = []
                     let infoRequest = {
                         landingPageId: this.pageid,
                         fileName: this.fields.filePath,
                         formHdId: this.fields.id
                     }

                     console.log("formDetails: ", this.fields.formDetails)
                     this.fields.formDetails.forEach(element => {
                         var infodata = {
                             fieldLabel: element.fieldLabel,
                             data: element.data,
                             order: element.order,
                             fieldTypeId: element.fieldTypeId,
                         };
                        
                         switch (element.fieldTypeId) {
                             case 'email':
                                 infodata.email = element.data;
                                 infoRequest.email = element.data;
                                 console.log("element.data: ", element.data)
                                 break;
                             case 'phone':
                                 infodata.phone = element.data;
                                 infoRequest.phone = element.data;
                                 break;
                             case 'name':
                                 infodata.name = element.data;
                                 infoRequest.name = element.data;
                                 break;
                         }

                         infoRequestData.push(infodata);
                     });

                     infoRequest.infoRequestData = JSON.stringify(infoRequestData);
                     console.log('api-inforequest', infoRequest);
                     axios.post(this.baseUrl + 'api-inforequest', infoRequest)
                         .then((response) => {
                             this.isloading = false;
                             this.clearFormData();
                             this.downloadPdf(this.fields.filePath);

                         })
                         .catch(function (error) {
                             this.isloading = false;
                             console.log('Error: ' + error);
                         });

                     // console.log(infoRequest);

                 } else {
                     console.log(isValid);
                 }
             });

         },
         clearFormData() {
             this.fields.formDetails.forEach(element => {
                 element.data = ""
             });
         },
         getErrMsg(errors, field) {
             var message = "";
             var error = errors.items.find(x => x.field == field);
             if (error) {
                 switch (error.rule) {
                     case 'required':
                         message = `El campo ${field} es obligatorio.`
                         break;
                     case 'email':
                         message = `El campo ${field} debe ser un correo electrónico válido.`
                         break;
                     default:
                         break;
                 }
             } else {
                 message = "";
             }

             return message
         },
         downloadPdf(fileName) {
             axios({
                 url: this.baseUrl + 'api-filemanager/download/' + fileName,
                 method: 'GET',
                 responseType: 'blob', // important
             }).then((response) => {
                 console.log(response.data.size)
                 if (response.status === 200 && response.data.size > 0) {
                     const url = window.URL.createObjectURL(new Blob([response.data]));
                     const link = document.createElement('a');
                     link.href = url;
                     link.setAttribute('download', fileName); //or any other extension
                     document.body.appendChild(link);
                     link.click();
                 } else {
                     console.log("SORRY NO FILE", response.data.size);
                 }

             });
         },
         setLabel(name) {
             return name;
         }
     },
     template: `<form id="contactForm">
     <div class="row">
         <div class="col-md-12">
             <div class="card">
                 <div class="card-header bg-white p-0">
                 <div class="text-center alert alert-success mb-0" hidden style="border-radius: 0">{{fields.title}}</div>
                 <h5 class="text-center my-3">{{fields.title}}</h5>
                 </div>
                 <div class="card-body bg-light p-2">
                     <div class="container-fluid bg-white pt-3 pb-3" style="min-height: 200px;" >
                        <ball-beat :isloading="true"  v-if="!fields.title"/>
                        <form v-if="fields.title">
                             <div v-for="(field, index) in fields.formDetails" :key="index">

                                 <div class="form-group" v-if="field.fieldTypeId === 'name'">
                                     <label class="mb-0" :for="field.name">{{field.fieldLabel}}</label>
                                     <input v-validate="'required'" :name="field.name" :id="field.name"
                                         type="text" class="form-control" :placeholder="field.fieldLabel"
                                         v-model="field.data">
                                     <span class="text-danger">{{ getErrMsg(errors, field.name) }}</span>

                                 </div>
                                 <div class="form-group" v-if="field.fieldTypeId === 'text'">
                                     <label class="mb-0" :for="field.name">{{field.fieldLabel}}</label>
                                     <input v-validate="'required'" :name="field.name" type="text"
                                         class="form-control" :placeholder="field.fieldLabel"
                                         v-model="field.data">
                                     <span class="text-danger">{{ getErrMsg(errors, field.name) }}</span>
                                 </div>
                                 <div class="form-group" v-if="field.fieldTypeId === 'email'">
                                     <label class="mb-0" :for="field.name">{{field.fieldLabel}}</label>
                                     <input v-validate="'required|email'" :name="field.name" type="text"
                                         class="form-control" :placeholder="field.fieldLabel"
                                         v-model="field.data">
                                     <span class="text-danger">{{ getErrMsg(errors, field.name) }}</span>

                                 </div>
                                 <div class="form-group" v-if="field.fieldTypeId === 'phone'">
                                     <label class="mb-0" :for="field.name">{{field.fieldLabel}}</label>
                                     <input v-validate="'required|numeric|min:10'" :name="field.name"
                                         maxlength="10" type="text" class="form-control"
                                         :placeholder="field.fieldLabel" v-model="field.data">
                                     <span class="text-danger">{{ errors.first(field.name) }}</span>
                                 </div>

                                 <div class="form-group" v-if="field.fieldTypeId === 'textarea'">
                                     <label class="mb-0" :for="field.name">{{field.fieldLabel}}</label>
                                     <textarea :name="field.name" class="form-control"
                                         :placeholder="field.fieldLabel" maxlength="450"
                                         v-model="field.data"></textarea>
                                 </div>

                                 <div class="form-group" v-if="field.fieldTypeId === 'select'">
                                     <label class="mb-0" :for="field.name">{{field.fieldLabel}}</label>
                                     <select v-validate="'required'" :name="field.name" class="custom-select"
                                         :placeholder="field.fieldLabel" v-model="field.data">

                                         <option v-for="(item, index) in field.ddlCatalogs" :key="index"
                                             :value="item.id">
                                             {{item.name}}</option>
                                     </select>
                                     <span class="text-danger">{{ errors.first(field.name) }}</span>
                                 </div>

                                 <div class="form-group text-center" v-if="field.fieldTypeId === 'submit'">
                                     <button class="btn btn-info mt-3 w-75" :name="field.name"
                                         v-on:click.prevent="onSubmit()" :disabled="isloading">
                                         <span>{{field.fieldLabel}}</span>
                                         <btn-loader :isloading="isloading"/>
                                         </button>
                                 </div>
                             </div>
                         </form>
                     </div>
                 </div>
             </div>
         </div>
         <hr>

     </div>

 </form>
`
 });

 Vue.component('btnLoader', {
     name: "btnLoader",
     props: ["isloading"],
     template: `<span class="spinners white" v-if="isloading"></span>`
 });
 Vue.component('ballBeat', {
     name: "ballBeat",
     template: `<div class="cu-loader-box"><div></div><div></div><div></div></div>`
 });

 Vue.use(VeeValidate);
 new Vue({
     el: '#app',
     data() {
         return {
             title: "LANDING PAGE",
             baseUrl: "http://localhost:60829/landing/",
         }
     },
     methods: {
         downloadPdf(formId) {
             //  alert(2)
             axios({
                 url: this.baseUrl + 'api-filemanager/download',
                 method: 'GET',
                 responseType: 'blob', // important
             }).then((response) => {
                 const url = window.URL.createObjectURL(new Blob([response.data]));
                 const link = document.createElement('a');
                 link.href = url;
                 link.setAttribute('download', 'file.txt'); //or any other extension
                 document.body.appendChild(link);
                 link.click();
             });
         }
     }
 });