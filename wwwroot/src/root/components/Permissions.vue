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
    <form role="form" novalidate>
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
                <button @click="removePermission" v-bind:disabled="!selectedUser || !allocatedPermissions.length" type="button" class="btn btn-secondary d-block mb-3 w-100">&#10007;</button>
                <button @click="addPermission" v-bind:disabled="!selectedUser || !selectedPermissions.length" type="button" class="btn btn-secondary d-block w-100">&rarr;</button>
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
                      <option v-for="userPermission in group.items" v-bind:value="userPermission.permissionId">{{ userPermission.description }}</option>
                    </optgroup>
                  </template>
                </select>
              </div>
            </div>
          </div>
        </div>
      </fieldset>
    </form>
  </main>
</template>

<script>
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
        selectedPermissions.forEach(selectedPermission => {
          var key = selectedPermission.routeId.substring(0, selectedPermission.routeId.indexOf('_'))
          var group = this.selectedUser.permissions.find(x => {
            return x.key === key
          })
          if (group) {
            var existingPermission = group.items.find(item => {
              return item.permissionId === selectedPermission.permissionId
            })
            if (!existingPermission) {
              group.items.push(selectedPermission)
            }
          } else {
            this.selectedUser.permissions.push({
              key: key,
              items: [selectedPermission]
            })
          }
          this.$http.post(`/api/permissions/?userId=${this.selectedUser.id}&permissionId=${selectedPermission.permissionId}`)
        })
      }
    },
    removePermission: function () {
      var selectedPermissions = this.getSelectedPermissions(this.allocatedPermissions)
      if (selectedPermissions) {
        selectedPermissions.forEach(selectedPermission => {
          this.selectedUser.permissions.forEach(userPermission => {
            userPermission.items.forEach(item => {
              if (item.permissionId === selectedPermission.permissionId) {
                var index = userPermission.items.indexOf(item)
                userPermission.items.splice(index, 1)
              }
            })
            if (userPermission.items.length === 0) {
              var index = this.selectedUser.permissions.indexOf(userPermission)
              this.selectedUser.permissions.splice(index, 1)
            }
          })
          this.$http.delete(`/api/permissions/?userId=${this.selectedUser.id}&permissionId=${selectedPermission.permissionId}`)
        })
      }
    },
    getLabel (user) {
      return user.name
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
      if (!user || user.permissions.length) {
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

select {
  max-height: 378px;
}

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