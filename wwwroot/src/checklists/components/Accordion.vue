<template>
  <div class="accordion" v-if="checklist && checklist.checklistItems && checklist.checklistItems.length">
    <div class="tab" v-for="(item, index) in checklist.checklistItems">
      <input :id="'tab-' + index" type="radio" name="tabs2">
      <label v-bind:for="'tab-' + index">
        <md-checkbox v-if="permissions.CI_ToggleCompleted" v-model="item.isCompleted" @click.native="toggleCompleted(item)"></md-checkbox>
        <span v-if="!item.isCompleted">{{ item.displayText }}</span>
        <del class="text-muted" v-if="item.isCompleted">{{ item.displayText }}</del>           
      </label>
      <div class="tab-content">
        <document-upload v-if="item.template === 'DocumentUpload'" :checklistId="checklist.id" :checklistItem="item" />
        <comments-date-of-action v-else-if="item.template === 'CommentsAndDateOfAction'" :checklistId="checklist.id" :checklistItem="item" />
        <comments-only v-else-if="item.template === 'CommentsOnly'" :checklistId="checklist.id" :checklistItem="item" />
        <date-of-action v-else-if="item.template === 'DateOfAction'" :checklistId="checklist.id" :checklistItem="item" />
      </div>
    </div>
  </div>
</template>

<script>
import DocumentUpload from './DocumentUpload'
import CommentsDateOfAction from './CommentsAndDateOfAction'
import CommentsOnly from './CommentsOnly'
import DateOfAction from './DateOfAction'

export default {
  name: 'accordion',
  components: { DocumentUpload, CommentsDateOfAction, CommentsOnly, DateOfAction },
  props: {
    'checklist': {
      type: Object,
      default: null
    }
  },
  data () {
    return {
      permissions: this.$store.state.permissions
    }
  },
  methods: {
    expand: function (item) {
      var expanded = item.isExpanded
      this.$emit('collapseAll')
      item.isExpanded = !expanded
      this.$emit('toggleExpanded', item)
    },
    toggleCompleted: function (item) {
      this.$emit('toggleCompleted', item)
    },
    move: function (direction, item) {
      this.$emit('move', { direction, item })
    }
  }
}
</script>
  
<style lang="scss" scoped>
  .tab {
    position: relative;
    margin-bottom: 1px;
    width: 100%;
    overflow: hidden;
  }

  input {
    position: absolute;
    opacity: 0;
    z-index: -1;
  }

  label {
    position: relative;
    display: block;
    padding: 0 0 0 1em;
    line-height: 3;
    background-color: #3f51b5;
    color: #fff;
    cursor: pointer;
  }

  .tab-content {
    max-height: 0;
    overflow: hidden;
    transition: max-height .35s;
  }

  .tab-content p {
    margin: 1em;
  }

  input:checked ~ .tab-content {
    max-height: 30rem;
    height: auto;
    padding: 30px 60px;
    background-color: #E8EAF6;
  }

  label::after {
    position: absolute;
    right: 0;
    top: 0;
    display: block;
    width: 3em;
    height: 3em;
    line-height: 3;
    text-align: center;
    -webkit-transition: all .35s;
    -o-transition: all .35s;
    transition: all .35s;
  }

  input {
    &[type=checkbox] + label::after {
      content: "+";
    }
    &[type=radio] + label::after {
      content: "\25BC";
    }
    &[type=checkbox]:checked + label::after {
      transform: rotate(315deg);
    }
    &[type=radio]:checked + label::after {
      transform: rotateX(180deg);
    }
  }

</style>
