<template>
  <div>
    <header>
      <h1 class="headline primary--text">Permissions</h1>
      <p class="display-2 grey--text text--darken-1">Manage permissions for individual users and groups</p>
    </header>
    <v-alert success :value="saved">
      Permissions have been updated
    </v-alert>
    <form role="form" novalidate>
      <div class="row">
        <label class="col-xs-12" for="searchForUser">Search for user</label>
        <div class="col-xs-12 col-md-5">
          <v-autocomplete :items="filteredUsers"
                          :auto-select-one-item="false"
                          :get-label="getLabel"
                          :component-item='template'
                          placeholder="Enter name or email"
                          v-model="selectedUser"
                          @change="change">
          </v-autocomplete>
        </div>
      </div>
      <div class="row mt-4">
        <div class="col-xs-5">
          <div class="row">
            <label class="col-xs-12" for="permission">Available Permissions</label>
            <div class="col-xs-12">
              <select v-model="selectedPermissions" id="permission" name="permission" size="20" multiple>
                <optgroup v-for="(group, index) in allPermissions" :label="group.key" :key="index">
                  <option v-for="(permission, permissionIndex) in group.items" :value="permission.permissionId" :key="permissionIndex">{{ permission.description }}</option>
                </optgroup>
              </select>
            </div>
          </div>
        </div>
        <div class="col-xs">
          <div class="h-100 d-flex align-items-center justify-content-center">
            <div>
              <v-btn outline @click="removePermission" :disabled="!selectedUser || !allocatedPermissions.length" type="button" class="d-block mb-3 w-100">&#10007;</v-btn>
              <v-btn outline @click="addPermission" :disabled="!selectedUser || !selectedPermissions.length" type="button" class="d-block w-100">&rarr;</v-btn>
            </div>
          </div>
        </div>
        <div class="col-xs-5">
          <div class="row">
            <label class="col-xs-12" for="userPermission">Allocated Permissions</label>
            <div class="col-xs-12">
              <select v-model="allocatedPermissions" id="userPermission" name="userPermission" size="20" multiple>
                <template v-if="selectedUser && selectedUser.permissions">
                  <optgroup v-for="(group, groupIndex) in selectedUser.permissions" :label="group.key" :key="groupIndex">
                    <option v-for="(userPermission, userPermissionIndex) in group.items" :value="userPermission.permissionId" :key="userPermissionIndex">{{ userPermission.description }}</option>
                  </optgroup>
                </template>
              </select>
            </div>
          </div>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import UserTemplate from './UserTemplate.vue'

export default {
  name: 'permissions',
  data () {
    return {
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
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  },
  mounted () {
    this.$http.get(`/api/permissions/all`)
      .then(response => {
        this.allPermissions = response.data
      })
    this.$http.get('/api/permissions/users')
      .then(response => {
        this.users = this.filteredUsers = response.data
      })
  },
  methods: {
    getSelectedPermissions (collection) {
      if (!collection) {
        return []
      }
      let result = []
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
    addPermission () {
      let selectedPermissions = this.getSelectedPermissions(this.selectedPermissions)
      if (selectedPermissions) {
        selectedPermissions.forEach(selectedPermission => {
          let key = selectedPermission.routeId.substring(0, selectedPermission.routeId.indexOf('_'))
          let group = this.selectedUser.permissions.find(x => {
            return x.key === key
          })
          if (group) {
            let existingPermission = group.items.find(item => {
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
    removePermission () {
      let selectedPermissions = this.getSelectedPermissions(this.allocatedPermissions)
      if (selectedPermissions) {
        selectedPermissions.forEach(selectedPermission => {
          this.selectedUser.permissions.forEach(userPermission => {
            userPermission.items.forEach(item => {
              if (item.permissionId === selectedPermission.permissionId) {
                let index = userPermission.items.indexOf(item)
                userPermission.items.splice(index, 1)
              }
            })
            if (userPermission.items.length === 0) {
              let index = this.selectedUser.permissions.indexOf(userPermission)
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

      let searchText = text.toLowerCase()
      this.filteredUsers = this.users.filter(item => {
        return item.name.toLowerCase().indexOf(searchText) > -1 || item.emailAddress.toLowerCase().indexOf(searchText) > -1
      })
    }
  },
  watch: {
    selectedUser (user) {
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
  width: 100%;
  display: block;
  padding: .5rem .75rem;
  font-size: 1rem;
  line-height: 1.25;
  color: #464a4c;
  background-color: #fff;
  border: 1px solid rgba(0, 0, 0, 0.15);
  border-radius: .25rem;
  transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
  margin: 0;
}

.v-autocomplete {
	.v-autocomplete-input-group {
		input {
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
