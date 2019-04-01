<template>
  <div class="container-fluid h-100 landing">
    <div class="row h-100">
      <div class="col-12 col-sm-6 col-md-5 col-lg-4">
        <div class="card border-light helix">
          <div class="card-header">
            <h6 class="mt-2">LANDING PAGES</h6>
            <a
              class="btn btn-link"
              @click="onEmitAct({ isActive: true }, 'create')"
            >
              <i class="pe-7s-close pe-rotate-45" style="font-size:1.5rem"></i>
              <br />
            </a>
          </div>
          <div class="card-body p-0">
            <div class="input-group search-by mb-0">
              <input
                type="text"
                class="form-control"
                v-model="searchby"
                placeholder="filtrar por landing page"
              />
              <div class="input-group-append">
                <span class="input-group-text" id="basic-addon2">
                  <i class="pe-7s-search"></i>
                </span>
              </div>
            </div>
            <ul
              class="list-group"
              v-if="landingPages"
              style="height: 100%;overflow: hidden;overflow-y: auto;"
            >
              <li
                class="list-group-item d-flex justify-content-between"
                v-for="item in filterSearch"
                :key="item.id"
                @click="onEmitAct(item, 'update')"
              >
                <div>
                  <h6 class="mb-0">{{ item.name }}</h6>
                  <p class="mb-0" style="color:#75818b">
                    <small>{{ item.description }}</small>
                  </p>
                </div>
              </li>
            </ul>
            <div class="alert alert-warning" v-if="!landingPages">
              no hay registro
            </div>
          </div>
        </div>
      </div>
      <div
        class="col-12 col-sm-6 col-md-7 col-lg-5 mt-3 mt-sm-0"
        v-if="action !== 'read'"
      >
        <create-update-landing :title="title" />
      </div>
    </div>
  </div>
</template>

<script>
import CreateUpdateLanding from '@/components/CreateUpdateLanding.vue'
import { mapState } from 'vuex'
export default {
  components: { CreateUpdateLanding },
  data() {
    return {
      searchby: '',
      title: ''
    }
  },
  created() {
    this.$store.dispatch('loadLandings')
    this.$store.dispatch('loadFormsForOptions')
    this.$store.commit('SET_ACTION', 'read')
  },
  computed: {
    ...mapState({
      landingPages: state => state.landings.landingPages,
      action: state => state.landings.action
    }),
    filterSearch() {
      return this.landingPages.filter(item => {
        return (
          !this.searchby ||
          item.name.toLowerCase().indexOf(this.searchby.toLowerCase()) > -1
        )
      })
    }
  },
  methods: {
    onEmitAct(item, action) {
      this.title =
        action === 'create' ? 'ALTA LANDING PAGE' : 'MODIFICAR LANDING PAGE'
      this.$store.commit('SET_ACTION', action)
      this.$store.commit('SET_LANDING', item)
    }
  }
}
</script>
<style lang="scss"></style>
