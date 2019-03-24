<template>
  <div class="container-fluid h-100 landing">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <div class="card border-light helix">
          <div class="card-header">
            <h6 class="mt-2">LANDING PAGES</h6>
            <a class="btn btn-link" @click="onEmitAct({}, 'create')">
              <i class="pe-7s-close pe-rotate-45" style="font-size:1.5rem"></i>
              <br>
            </a>
          </div>
          <div class="card-body p-0">
            <ul class="list-group">
              <li
                v-for="(item, index) in landingPages"
                :key="index"
                class="list-group-item d-flex justify-content-between"
                @click="onEmitAct(item, 'update')"
              >
                <landing-item :landing="item"/>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="col-12 col-sm-6 col-md-7 col-lg-5 mt-3 mt-sm-0">
        <landing-create-update :title="title" v-if="action !=='read'"/>
      </div>
    </div>
  </div>
</template>

<script>
import LandingItem from "@/components/LandingItem.vue";
import LandingCreateUpdate from "@/components/LandingCreateUpdate.vue";
import { mapState } from "vuex";
export default {
  components: { LandingItem, LandingCreateUpdate },
  data() {
    return {
      title: ""
    };
  },
  computed: {
    ...mapState({
      landingPages: state => state.landings.landingPages,
      action: state => state.landings.action
    })
  },
  created() {
    this.$store.dispatch("loadLandings");
  },
  methods: {
    onEmitAct(item, action) {       
      this.title = "ALTA LANDING PAGE ";
      if (action === "update") {
         this.title = "MODIFICAR LANDING PAGE ";
      }
      this.$store.commit("SET_ACTION", action);
      this.$store.commit("SET_LANDING", item);
    }
  }
};

</script>
