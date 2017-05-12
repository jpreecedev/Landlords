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
            <v-autocomplete :items="filteredUsers" placeholder="Enter name or email" v-model="selectedUser" :auto-select-one-item="false" :get-label="getLabel" :component-item='template' @change="change"></v-autocomplete>
          </div>
        </div>
        <div class="row mt-4">
          <div class="col">
            <div class="form-group row">
              <label class="col-12 col-form-label" for="permission">Available Permissions</label>
              <div class="col-12">
                <select v-model="selectedPermissions" class="form-control" id="permission" name="permission" size="20" multiple>
                  <optgroup v-for="group in allPermissions" v-bind:label="group.key">
                    <option v-for="permission in group.items" v-bind:value="permission.permissionId">{{ permission.description }}</option>
                  </optgroup>
                </select>
              </div>
            </div>
          </div>
          <div class="col-auto">
            <div class="h-100 d-flex align-items-center">
              <div>
                <button @click="removePermission" type="button" class="btn btn-secondary d-block mb-3 w-100">&#10007;</button>
                <button @click="addPermission" type="button" class="btn btn-secondary d-block w-100">&rarr;</button>
              </div>
            </div>
          </div>
          <div class="col">
            <div class="form-group row">
              <label class="col-12 col-form-label" for="userPermission">Allocated Permissions</label>
              <div class="col-12">
                <select v-model="allocatedPermissions" class="form-control" id="userPermission" name="userPermission" size="20" multiple>
                  <template v-if="selectedUser && selectedUser.permissions">
                    <optgroup v-for="group in selectedUser.permissions" v-bind:label="group.key">
                      <option v-for="userPermission in group.items" v-bind:value="userPermission.id">{{ userPermission.description }}</option>
                    </optgroup>
                  </template>
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
      selectedPermissions: [],
      allocatedPermissions: [],
      saved: false,
      selectedUser: null,
      users: [],
      filteredUsers: [],
      template: UserTemplate
    }
  },
  created () {
    this.$http.get(`/api/permissions/all`).then(response => {
      this.allPermissions = response.data
    })
    this.$http.get('/api/permissions/users').then(response => {
      this.users = this.filteredUsers = response.data
    })
  },
  methods: {
    getSelectedPermissions: function (collection) {
      if (!collection) {
        return []
      }
      var result = []
      collection.forEach(permission => {
        this.allPermissions.forEach(allPermission => {
          allPermission.items.forEach(item => {
            console.log(item, permission)
            if (item.permissionId === permission) {
              result.push(item)
            }
          })
        })
      })
      return result
    },
    addPermission: function () {
      var selectedPermissions = this.getSelectedPermissions(this.selectedPermissions)
      if (selectedPermissions) {
        // TODO
      }
    },
    removePermission: function () {
      var selectedPermissions = this.getSelectedPermissions(this.allocatedPermissions)
      if (selectedPermissions) {
        console.log(selectedPermissions)
        selectedPermissions.forEach(permission => {
          this.$http.delete(`/api/permissions`, {
            params: {
              userId: this.selectedUser.id,
              permissionId: permission.permissionId
            }
          })
        })
      }
    },
    getLabel (user) {
      return user.name
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
    },
    change (text) {
      if (!text) {
        this.filteredUsers = this.users
        return
      }

      var searchText = text.toLowerCase()
      this.filteredUsers = this.users.filter(item => {
        return item.name.toLowerCase().indexOf(searchText) > -1 || item.emailAddress.toLowerCase().indexOf(searchText) > -1
      })
    }
  },
  watch: {
    selectedUser: function (user) {
      if (!user || user.permissions) {
        return
      }

      this.$http.get(`/api/permissions/${user.id}`).then(response => {
        user.permissions = response.data
      })
    }
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
