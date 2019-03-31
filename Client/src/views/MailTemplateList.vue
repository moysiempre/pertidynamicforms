<template>
  <div class="container-fluid h-100">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <div class="card border-light helix">
          <div class="card-header">
            <h6 class="mt-2">TEMPLATES</h6>
            <a class="btn btn-link" @click="onEmitAct({ isActive: true}, 'create')">
              <i class="pe-7s-close pe-rotate-45" style="font-size:1.5rem"></i>
              <br>
            </a>
          </div>
          <div class="card-body p-0">
            <div class="input-group search-by mb-0">
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

            <ul class="list-group" v-if="mailtemplates">
              <li class="list-group-item" v-for="item in filterSearch" :key="item.id">
                <div @click="onEmitAct(item, 'update')">
                  <div>
                    <h6 class="mb-0">{{item.name}}</h6>
                    <p class="mb-0" style="color:#75818b">
                      <small>{{item.title}}</small>
                    </p>
                  </div>
                </div>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="col-12 col-sm-6 col-md-7 col-lg-6" v-if="action !== 'read'">
        <create-update-mail-template :title="title"/>
      </div>
    </div>
  </div>
</template>

<script>
import CreateUpdateMailTemplate from "@/components/CreateUpdateMailTemplate.vue";
import { mapState } from "vuex";
export default {
  components: { CreateUpdateMailTemplate },
  data() {
    return {
      title: "",
      searchby: ""
    };
  },
  created() {
    this.$store.dispatch("loadTemplates");
    this.$store.commit("SET_ACTION", "read");
  },
  computed: {
    ...mapState({
      mailtemplates: state => state.templates.mailtemplates,
      action: state => state.templates.action
    }),
    filterSearch() {
      return this.mailtemplates.filter(item => {
        return (
          !this.searchby ||
          item.name.toLowerCase().indexOf(this.searchby.toLowerCase()) > -1
        );
      });
    }
  },
  methods: {
    onEmitAct(item, action) {
      this.title =
        action === "create"
          ? "ALTA MAIL TEMPLATE PAGE"
          : "MODIFICAR MAIL TEMPLATE PAGE";
      this.$store.commit("SET_ACTION", action);
      this.$store.commit("SET_TEMPLATE", item);
    }
  }
};
</script>

<style scoped>
</style>