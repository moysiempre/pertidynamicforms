<template>
  <div id="acontact" class="container-fluid">
    <h5>{{ title }}</h5>
    <div class="row mb-1">
      <div class="col-12 col-sm-12 col-md-5 text-right"></div>
      <div class="col-12 col-sm-12 col-md-7 text-right">
        <div class="row no-gutters">
          <div class="col-sm-4 d-flex align-items-center">
            <select
              class="custom-select mr-sm-1"
              id="ddllandings"
              @change="onChange($event)"
            >
              <option value>Seleccione landing page</option>
              <option
                v-for="item in landingPages"
                :key="item.id"
                :value="item.id"
                >{{ item.name }}</option
              >
            </select>
          </div>
          <div class="col-sm-3 d-flex align-items-center">
            <date-picker
              class="mr-sm-1"
              v-model="startDate"
              :first-day-of-week="1"
              format="DD/MM/YYYY"
              :not-after="endDate"
              :lang="lang"
              @change="onChangeStartDate"
            ></date-picker>
          </div>
          <div class="col-sm-3 d-flex align-items-center">
            <date-picker
              v-model="endDate"
              :first-day-of-week="1"
              format="DD/MM/YYYY"
              :not-before="startDate"
              :not-after="new Date()"
              :lang="lang"
              @change="onChangeEndDate"
            ></date-picker>
          </div>
          <div class="col-sm-2 d-flex justify-content-end">
            <button
              type="button"
              class="btn btn-default btn-sm py-0"
              @click="exportarXlsx"
            >
              <p class="exlsx m-0 mt-1" v-if="!isloading">
                <img src="/assets/exportarXlsx.png" alt srcset />
              </p>
              <p class="m-0 mt-1 mr-2" style="height:37px" v-if="isloading">
                <btn-loader :isloading="isloading" />
              </p>
              <span>EXPORTAR</span>
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12">
        <ag-grid-vue
          style="width: 100%; height: 300px;"
          class="ag-theme-balham"
          :gridOptions="gridOptions"
          :columnDefs="columnDefs"
          :domLayout="'autoHeight'"
          :colResizeDefault="'shift'"
          @first-data-rendered="onFirstDataRendered"
          :rowData="rowData"
          :pagination="true"
          :paginationPageSize="8"
        ></ag-grid-vue>
      </div>
    </div>

    <!-- begin Modal -->
    <div
      class="modal fade"
      id="contactDetailModal"
      tabindex="-1"
      role="dialog"
      aria-labelledby="contactDetailModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header py-1">
            <h5 class="modal-title" id="contactDetailModalLabel">
              Detalle del formulario
            </h5>
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <contact-detail :contact="contactItem" />
          </div>
        </div>
      </div>
    </div>
    <!-- end Modal -->
  </div>
</template>
<script>
import axios from 'axios'
import DatePicker from 'vue2-datepicker'
import { AgGridVue } from 'ag-grid-vue'
import ContactDetail from '@/components/ContactDetail.vue'
import BtnLoader from '@/components/BtnLoader.vue'

export default {
  name: 'acontact',
  components: { DatePicker, AgGridVue, ContactDetail, BtnLoader },
  data() {
    return {
      title: 'SOLICITUDES',
      rowData: [],
      gridOptions: { rowHeight: 50 },
      landingPages: [],
      columnDefs: null,
      contactItem: {},
      isloading: false,
      startDate: () => {
        return new Date()
      },
      endDate: new Date(),
      lang: {
        days: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
        months: [
          'Ene',
          'Feb',
          'Mar',
          'Abr',
          'May',
          'Jun',
          'Jul',
          'Ago',
          'Sep',
          'Oct',
          'Nov',
          'Dic'
        ],
        pickers: [
          'next 7 days',
          'next 30 days',
          'previous 7 days',
          'previous 30 days'
        ],
        placeholder: {
          date: 'Selecione fecha'
        }
      }
    }
  },
  beforeMount() {
    this.loadLandingPages()
    this.columnDefs = [
      {
        headerName: 'FECHA',
        field: 'requestDateStr',
        filter: true
      },
      {
        headerName: 'NOMBRE',
        field: 'name',
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: 'EMAIL',
        field: 'email',
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: 'TELÉFONO',
        field: 'phone',
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: 'LANDING PAGE',
        field: 'landingPageName',
        resizable: true,
        filter: true,
        sort: 'asc'
      },
      {
        headerName: '',
        field: 'pepe',
        cellRenderer: params => {
          const element = document.createElement('span')
          const data = params.node.data
          element.innerHTML = `<button type="button" class="btn btn-link btn-sm">Ver Detalle</button>`
          element.addEventListener('click', () => {
            this.contactItem = data
            window.$('#contactDetailModal').modal({
              backdrop: 'static'
            })
          })
          return element
        },
        width: 180
      }
    ]
  },
  computed: {
    // startDate() {
    //   var now = new Date()
    //   var date = -15 + now.getDay()
    //   return now.setDate(date)
    // },
    // endDate() {
    //   return new Date()
    // }
  },
  mounted() {
    this.load({})
  },
  methods: {
    exportarXlsx() {
      this.isloading = true

      axios({
        url: 'api-inforequest/download',
        method: 'GET',
        responseType: 'blob',
        params: {}
      })
        .then(response => {
          this.isloading = false
          //console.log(response.data.size)
          if (response.status === 200 && response.data.size > 0) {
            const url = window.URL.createObjectURL(new Blob([response.data]))
            const link = document.createElement('a')
            link.href = url
            link.setAttribute('download', 'solicitudes.xlsx') //or any other extension
            document.body.appendChild(link)
            link.click()
          }
        })
        .catch(() => {
          this.isloading = false
        })
    },
    load(request) {
      axios
        .get('api-inforequest/getby', {
          params: request
        })
        .then(response => {
          this.rowData = response.data
          this.rowData.forEach(element => {
            element.infoRequestData = JSON.parse(element.infoRequestData)
            if (element.infoRequestData && element.infoRequestData.length) {
              element.infoRequestData.forEach(el => {
                switch (el.fieldTypeId) {
                  case 'email':
                    element.email = el.data
                    break
                  case 'phone':
                    element.phone = el.data
                    break
                  case 'name':
                    element.name = el.data
                    break
                }
              })
            }
          })
          //console.log('response.data ', response.data)
        })
    },
    loadLandingPages() {
      axios.get('api-landingpage').then(response => {
        this.landingPages = response.data
      })
    },
    onFirstDataRendered(params) {
      params.api.sizeColumnsToFit()
    },
    onChange(event) {
      this.load({
        landingPageId: event.target.value
      })
    },
    onChangeStartDate(date) {
      const eDate = `${date.getFullYear()}-${date.getMonth() +
        1}-${date.getDate()}`
      this.load({
        startDate: eDate
      })
    },
    onChangeEndDate(date) {
      const eDate = `${date.getFullYear()}-${date.getMonth() +
        1}-${date.getDate()}`
      this.load({
        endDate: eDate
      })
    }
  }
}
</script>

<style lang="scss">
.mx-datepicker {
  width: auto !important;
}
.ag-cell {
  display: flex;
  align-items: center;
}
.ag-theme-balham .ag-paging-panel {
  background: #fff;
  border-top: 1px solid #fff;
}
.ag-theme-balham .ag-header-row {
  background: #fff;
}
</style>
