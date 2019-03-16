Vue.use(window.vuelidate.default);
let vm = new Vue({
    el: '#app',
    components: [ "btnsubmit"],
    data() {
        return {
            title: '¡Tu empresa merece su app en android!',
            baseUrl: "http://localhost:60829/landing/",
            formData: {

            },
            errors: [],
        }
    },
    validations: {
        formData: {
            name: {
                required: validators.required
            },
            email: {
                required: validators.required,
                email: validators.email
            },
            phone: {
                required: validators.required,
                minLength: validators.minLength(10),
                maxLength: validators.maxLength(10)
            },
        }
    },
    methods: {

        onSubmit() {
            this.clearForm();
            var infoRequestData = JSON.stringify(this.formData);
            let infoRequest = {
                infoRequestData: infoRequestData,
                landingPageId: "5bcda8be-a293-46bd-a1da-a63cf4dcba4a",
                email: this.formData.email
            }
            axios.post(this.baseUrl + 'api-inforequest', infoRequest)
                .then( (response) => {
                    console.log("SUCCESS", response.data);
                    this.clearForm();
                })
                .catch(function (error) {
                    console.log('Error: ' + error);
                });
        },
        clearForm(){
            this.formData = {
                name: "",
                email: "",
                phone: ""
            }
            this.$v.formData.$reset();
        }
    },
    created: function () {

        axios.get(this.baseUrl + 'api-inforequest')
            .then(function (response) {
                console.log("inforequest: ", response.data);
            })
            .catch(function (error) {
                console.log('Error: ' + error);
            });
        axios.get(this.baseUrl + 'api-forms/fieldTypes')
            .then(function (response) {
                console.log(response.data);
            })
            .catch(function (error) {
                console.log('Error: ' + error);
            });
    },
})
// Define a new component called button-counter
Vue.component('btnsubmit', {
 
    data: function () {
        return {
            count: 0,
            name:"ENVIAR"
        }
    },
    template: '<button type="submit">{{ name }}</button>'
})