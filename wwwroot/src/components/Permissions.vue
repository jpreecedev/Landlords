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

export default {
  name: 'permissions',
  data () {
    return {
      permissions: this.$store.state.permissions,
      allPermissions: [],
      saved: false
    }
  },
  created () {
    this.$http.get(`/api/permissions/all`).then(response => {
      this.allPermissions = response.data
    })
  },
  methods: {
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
  
<style lang="scss" scoped>



</style>
