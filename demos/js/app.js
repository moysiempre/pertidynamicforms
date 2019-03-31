const landingPageId = "5bcda8be-a293-46bd-a1da-a63cf4dcba4a";
const formId = "34dc99d3-8f30-4008-873d-31cf1547ac7a";

const dictionary = {
    en: {

    }
};
Vue.use(VeeValidate, {
    locale: 'en',
    dictionary: dictionary.en
});
Vue.component('mainContainerGp', {
    data() {
        return {
            owner: '',
            msg: 'HOLA msg',

        }
    },
    template: `<div>{{msg}}</div>`
});


var app = new Vue({
    el: '#app',
    data() {
        return {
            title: "Hello",
            baseUrl: "http://localhost:60829/landing/",
            formData: {},
            fields: []
        }
    },


    methods: {
        load(formId) {
            axios.get(this.baseUrl + 'api-forms/' + formId).then((response) => {
                console.log(response.data);
                this.fields = response.data
            });
        },
        onSubmit: function () {
            this.$validator.validateAll().then(isValid => {
                if (isValid) {
                    var infoRequestData = JSON.stringify(this.formData);
                    let infoRequest = {
                        infoRequestData: infoRequestData,
                        landingPageId: landingPageId,
                        email: this.formData.email
                    }
                    axios.post(this.baseUrl + 'api-inforequest', infoRequest)
                        .then((response) => {
                            this.formData = {};
                        })
                        .catch(function (error) {
                            console.log('Error: ' + error);
                        });
                } else {
                    console.log(isValid);
                }
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
        }
    },
    beforeMount() {
        this.load(formId);

    },
    mounted() {

    },
    beforeUpdate: function () {

    },
    updated: function () {

    }
});