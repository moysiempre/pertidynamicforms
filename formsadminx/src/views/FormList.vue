<template>
  <div class="container-fluid h-100">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <div class="card border-light helix">
          <div class="card-header">
            <h6 class="mt-2">FORMULARIOS</h6>
            <a
              class="btn btn-link"
              :class="{'visibility-hidden': action !== 'read'}"
              @click="onEmitAct({}, 'create')"
            >
              <i class="pe-7s-close pe-rotate-45" style="font-size:1.5rem"></i>
              <br>
            </a>
          </div>
          <div class="card-body p-0">
            <div class="input-group mb-0 search-by">
              <input
                type="text"
                class="form-control"
                v-model="searchby"
                placeholder="filtrar por landing page"
              >
              <div class="input-group-append">
                <span class="input-group-text" id="basic-addon2">
                  <i class="pe-7s-search"></i>
                </span>
              </div>
            </div>
            <ul class="list-group" v-if="formHds">
              <li class="list-group-item" v-for="item in filterSearch" :key="item.id">
                <div @click="onEmitAct(item, 'update')">
                  <div>
                    <h6 class="mb-0">{{item.name}}</h6>
                    <p class="mb-0" style="color:#75818b">
                      <small>{{item.title}}</small>
                    </p>
                  </div>
                  <div class="c-wait" hidden>
                    <h6 class="mb-0">{{item.name}}</h6>
                    <p class="mb-0" style="color:#75818b">
                      <small>{{item.title}}</small>
                    </p>
                  </div>
                </div>
                <div :class="{'c-wait': action !== 'read'}" v-if="action !== 'read'" hidden>
                  <div>
                    <h6 class="mb-0">{{item.name}}</h6>
                    <p class="mb-0" style="color:#75818b">
                      <small>{{item.title}}</small>
                    </p>
                  </div>
                  <div class="c-wait" hidden>
                    <h6 class="mb-0">{{item.name}}</h6>
                    <p class="mb-0" style="color:#75818b">
                      <small>{{item.title}}</small>
                    </p>
                  </div>
                </div>
              </li>
            </ul>
            <div class="alert alert-warning" v-if="!formHds">no hay registro</div>
          </div>

          <!-- <pre class="language-json"><code>{{ options  }}</code></pre> -->
        </div>
      </div>
      <div class="col-12 col-sm-6 col-md-7 col-lg-5" v-if="action !== 'read'">
        <create-update-form :title="title"/>
      </div>

      <div class="col-12 col-sm-6 col-md-7 col-lg-3"></div>
    </div>
  </div>
</template>

<script>
import CreateUpdateForm from "@/components/CreateUpdateForm.vue";
import { mapState } from "vuex";

export default {
  components: { CreateUpdateForm },
  data() {
    return {
      title: "",
      searchby: ""
    };
  },
  created() {
    this.$store.dispatch("loadFormHds");
    this.$store.dispatch("loadBaseDetails");
    this.$store.dispatch("loadOptions");
    this.$store.dispatch("loadTemplates");
    this.$store.commit("SET_ACTION", "read");
  },
  computed: {
    ...mapState({
      formHds: state => state.forms.formHds,
      action: state => state.forms.action,
      baseDetails: state => state.forms.baseDetails,
      options: state => state.forms.options
    }),
    filterSearch() {
      return this.formHds.filter(item => {
        return (
          !this.searchby ||
          item.name.toLowerCase().indexOf(this.searchby.toLowerCase()) > -1
        );
      });
    }
  },
  mounted() {},
  methods: {
    onEmitAct(item, action) {
      this.title =
        action === "create" ? "ALTA FORMULARIO" : "MODIFICAR FORMULARIO";

      if (action === "create") {
        item = {
          id: "",
          isActive: true,
          formDetails: this.baseDetails
        };
        this.$store.commit("SET_VALUES", []);
        this.$store.dispatch("loadUniqOptions");
      }

      if (action == "update") {
        let _values = [];
        if (item && item.landingPages && item.landingPages.length) {
          item.landingPages.forEach(element => {
            var landing = this.options.find(
              x => x.id == element.id && element.isActive
            );
            if (landing) {
              _values.push(landing);
            }
          });
        }
        this.$store.commit("SET_VALUES", _values);
        this.$store.dispatch("loadOptions");
        console.log("onEmitAct", this.$store.state.options);
      }

      this.$store.commit("SET_ACTION", action);
      this.$store.commit("SET_FORM_HD", item);
    }
  }
};
</script>

 
 