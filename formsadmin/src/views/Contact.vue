<template>
  <div id="acontact" class="container-fluid">
    <h5>{{title}}</h5>
    <div class="row mb-1">
      <div class="col-12 col-sm-4 col-md-6"></div>
      <div class="col-12 col-sm-8 col-md-6">
        <div class="row no-gutters">
          <div class="col-sm-6">
            <select class="custom-select mr-sm-2" id="ddllandings" @change="onChange($event)">
              <option value>Seleccione...</option>
              <option v-for="item in landingPages" :key="item.id" :value="item.id">{{item.name}}</option>
            </select>
          </div>
          <div class="col-sm-3">
            <date-picker
              v-model="startDate"
              :first-day-of-week="1"
              format="DD/MM/YYYY"
              :not-after="endDate"
              :lang="lang"
              @change="onChangeStartDate"
            ></date-picker>
          </div>
          <div class="col-sm-3">
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
          :paginationPageSize="5"
        ></ag-grid-vue>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import DatePicker from "vue2-datepicker";
import { AgGridVue } from "ag-grid-vue";

export default {
  name: "acontact",
  components: { DatePicker, AgGridVue },
  data() {
    return {
      title: "SOLICITUDES",
      rowData: [],
      gridOptions: { rowHeight: 50 },
      landingPages: [],
      columnDefs: null,
      startDate: new Date(),
      endDate: new Date(),
      lang: {
        days: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        months: [
          "Jan",
          "Feb",
          "Mar",
          "Apr",
          "May",
          "Jun",
          "Jul",
          "Aug",
          "Sep",
          "Oct",
          "Nov",
          "Dec"
        ],
        pickers: [
          "next 7 days",
          "next 30 days",
          "previous 7 days",
          "previous 30 days"
        ],
        placeholder: {
          date: "Select Date",
          dateRange: "Select Date Range"
        }
      }
    };
  },
  beforeMount() {
    this.loadLandingPages();
    this.columnDefs = [
      {
        headerName: "ID",
        field: "id",
        sortable: true,
        filter: true,
        resizable: true
      },
      {
        headerName: "landingPageId",
        field: "landingPageId",
        resizable: true,
        sort: "asc"
      },
      { headerName: "requestDate", field: "requestDate", resizable: true }
    ];
  },

  mounted() {
    this.load({});
  },
  methods: {
    load(request) {
      axios
        .get("api-inforequest/getby", {
          params: request
        })
        .then(response => {
          this.rowData = response.data;
          // this.rowData.forEach(element => {
          //   element.objData = JSON.parse(element.infoRequestData);
          // });
          console.log("response.data ", response.data);
        });
    },
    loadLandingPages() {
      axios.get("api-landingpage").then(response => {
        this.landingPages = response.data;
      });
    },
    onFirstDataRendered(params) {
      params.api.sizeColumnsToFit();
    },
    onChange(event) {
      this.load({
        landingPageId: event.target.value
      });
    },
    onChangeStartDate(date) {
      const eDate = `${date.getFullYear()}-${date.getMonth() +
        1}-${date.getDate()}`;
      this.load({
        startDate: eDate
      });
    },
    onChangeEndDate(date) {
      const eDate = `${date.getFullYear()}-${date.getMonth() +
        1}-${date.getDate()}`;
      this.load({
        endDate: eDate
      });
    }
  }
};
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
 