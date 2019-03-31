<template>
 
</template>
<script>
// import axios from "axios";
import { mapState, mapGetters } from "vuex";
export default {
  data() {
    return {
      searchby: "",
      title: ""
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
      console.log("setfAction", action);
      console.log("fAction", this.$store.getters.fAction);

      if (action == "create") {
        let formHd = {
          id: "",
          isActive: true,
          formDetails: this.baseDetails
        };

        this.$store.commit("setFormHd", formHd);
        this.$store.commit("SET_VALUES", []);
      }

      if (action == "update") {
        this.$store.commit("setFormHd", item);
        let _values = [];

        if (item && item.formHdLandingPage && item.formHdLandingPage.length) {
          item.formHdLandingPage.forEach(element => {
            var landing = this.options.find(x => x.id == element.landingPageId);
            if (landing) {
              _values.push(landing);
            }
          });
        }
        this.$store.commit("SET_VALUES", _values);
      }
    }
  }
};
</script>

