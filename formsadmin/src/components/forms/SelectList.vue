<template>
  <div class="form-group">
    <!-- <label>{{label}}</label> -->
    <select :multiple="multi" class="custom-select"
            :value="value"
            @input="$emit('input',$event.target.value)">
            <option v-for="option in options"
                  :key="option.key">
            {{option.value}}
      </option>
    </select>
  </div>
</template>
<script>
export default {
  name: 'SelectList',
  props: ['multi', 'options', 'name', 'label', 'value'],
 
}
</script>