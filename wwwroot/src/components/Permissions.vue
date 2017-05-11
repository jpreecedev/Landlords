<template>
  <main class="container">
    <div> 
      <h1>Permissions</h1>
      <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Mollitia, quam minus alias. Veritatis error dolore ex dignissimos enim laudantium repellendus illo in nulla ratione! Saepe, minus asperiores consequuntur incidunt sint!</p>
    </div>
    <div id="errorMessage" class="alert alert-danger" v-show="errors.any()">
      <span v-show="!errors.has('GenericError')">Please fix validation errors highlighted in red and try and submit the form again</span>
      <span v-show="errors.has('GenericError')">{{ errors.first('GenericError') }}</span>
    </div>
    <div class="alert alert-success" v-if="saved">
      Permissions have been updated
    </div> 
    <form @submit.prevent="updatePermissions" role="form" enctype="multipart/form-data" novalidate>
      <fieldset>
        <div class="form-group row">
          <label class="col-12 col-form-label" for="searchForUser">Search for user</label>
          <div class="col-12">
            <v-autocomplete :items="items" v-model="item" :get-label="getLabel" :component-item='template'></v-autocomplete>
          </div>
        </div>
        <div class="row mt-4">
          <div class="col">
            <div class="form-group row">
              <label class="col-12 col-form-label" for="permission">Available Permissions</label>
              <div class="col-12">
                <select class="form-control" id="permission" name="permission" size="20" multiple>
                  <optgroup v-for="group in allPermissions" v-bind:label="group.key">
                    <option v-for="permission in group.items" v-bind:value="permission.PermissionId">{{ permission.description }}</option>
                  </optgroup>
                </select>
              </div>
            </div>
          </div>
          <div class="col-auto">
            <div class="h-100 d-flex align-items-center">
              <div>
                <button type="button" class="btn btn-secondary d-block mb-3 w-100">&#10007;</button>
                <button type="button" class="btn btn-secondary d-block w-100">&rarr;</button>
              </div>
            </div>
          </div>
          <div class="col">
            <div class="form-group row">
              <label class="col-12 col-form-label" for="permission">Allocated Permissions</label>
              <div class="col-12">
                <select class="form-control" id="permission" name="permission" size="20" multiple>
                  <optgroup v-for="group in allPermissions" v-bind:label="group.key">
                    <option v-for="permission in group.items" v-bind:value="permission.PermissionId">{{ permission.description }}</option>
                  </optgroup>
                </select>
              </div>
            </div>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col">
            <button v-if="permissions.PE_Update" type="submit" class="btn btn-primary">Save</button>
          </div>
        </div>
      </fieldset>
    </form>
  </main>
</template>

<script>
import { ErrorBag } from 'vee-validate'
import utils from '@/utils'
import UserTemplate from './UserTemplate.vue'

export default {
  name: 'permissions',
  data () {
    return {
      permissions: this.$store.state.permissions,
      allPermissions: [],
      saved: false,
      item: null,
      items: [],
      template: UserTemplate
    }
  },
  created () {
    this.$http.get(`/api/permissions/all`).then(response => {
      this.allPermissions = response.data
    })
    this.$http.get('/api/permissions/users').then(response => {
      this.items = response.data
    })
  },
  methods: {
    getLabel (item) {
      return item.name
    },
    updatePermissions: function () {
      var bag = new ErrorBag()
      this.$http.post(`/api/permissions`, {})
      .then(() => {
        this.saved = true
      })
      .catch(response => {
        var validationResult = utils.getFormValidationErrors(response)
        validationResult.errors.forEach(validationError => {
          bag.add(validationError.key, validationError.messages[0], 'required')
        })
        if (validationResult.status) {
          bag.add('GenericError', validationResult.status, 'generic')
        }
      })
    }
  },
  watch: {
    '$route' (to, from) {}
  }
}
</script>
  
<style lang="scss">

.v-autocomplete {
	.v-autocomplete-input-group {
		.v-autocomplete-input {
      display: block;
      width: 100%;
      padding: 0.5rem 0.75rem;
      font-size: 1rem;
      line-height: 1.25;
      color: #464a4c;
      background-color: #fff;
      border: 1px solid rgba(0, 0, 0, 0.15);
      border-radius: 0.25rem;
		}
	}
	.v-autocomplete-list {
		width: 100%;
		text-align: left;
		border: none;
		border-top: none;
		max-height: 400px;
		overflow-y: auto;
		border-bottom: 1px solid rgb(77, 144, 254);
    z-index: 2;
		.v-autocomplete-list-item {
			cursor: pointer;
			background-color: #fff;
			padding: 10px;
			border-bottom: 1px solid rgb(77, 144, 254);
			border-left: 1px solid rgb(77, 144, 254);
			border-right: 1px solid rgb(77, 144, 254);
			&:last-child {
				border-bottom: none;
			}
			&:hover {
				background-color: #eaeaea;
			}
			abbr {
				opacity: 0.8;
				font-size: 0.8em;
				display: block;
				font-family: sans-serif;
			}
		}
	}
}

</style>
