<template>
  <div class="card border-light helix">
    <div class="card-header">
      <h6 class="mt-2">FORMULARIOS</h6>
      <a
        class="btn btn-link"
        :class="{'visibility-hidden': fAction !== 'read'}"
        @click="setAction('create', {})"
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
          <div @click="setAction('update', item)">
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
        </li>
      </ul>
      <div class="alert alert-warning" v-if="!formHds">no hay registro</div>
    </div>

    <!-- <pre class="language-json"><code>{{ options  }}</code></pre> -->
  </div>
</template>
<script>
import axios from "axios";
import { mapState, mapGetters } from "vuex";
export default {
  data() {
    return {
      searchby: ""
    };
  },
  created() {
    this.$store.dispatch("loadFormHds");
    this.$store.dispatch("loadOptions");
  },
  computed: {
    ...mapState(["baseDetails"]),
    ...mapGetters(["options", "formHds", "fAction"]),
    filterSearch() {
      return this.formHds.filter(item => {
        return (
          !this.searchby ||
          item.name.toLowerCase().indexOf(this.searchby.toLowerCase()) > -1
        );
      });
    }
  },
  methods: {
    setAction(action, item) {
      this.$store.commit("setfAction", action);
      if (action == "create") {
        let formHd = {
          id: "",
          isActive: true,
          formDetails: this.baseDetails
        };

        this.$store.commit("setFormHd", formHd);
        this.$store.commit("updateValues", []);
      }

      if (action == "update") {
        this.$store.commit("setFormHd", item);
        let _values = [];

        if (item && item.formHdLandingPage && item.formHdLandingPage.length) {
          item.formHdLandingPage.forEach(element => {
            var landing = this.options.find(
              x => x.id == element.landingPageId
            );
            if (landing) {
              _values.push(landing);
            }
          });
        }
        this.$store.commit("updateValues", _values);
      }
    }
  }
};
</script>

