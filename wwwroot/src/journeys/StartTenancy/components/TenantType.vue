<template>
  <span class="lead">
    <span v-if="tenant.isLeadTenant">
      Lead Tenant
      <span>
        <span v-if="tenant.firstName || tenant.lastName"> - </span>
        <span v-if="tenant.firstName">{{ tenant.firstName }}</span>
        <span v-if="tenant.lastName">{{ tenant.lastName }}</span>
      </span>
    </span>
    <span v-else-if="tenant.isAdult">
      Additional Adult Tenant
      <span>
        <span v-if="tenant.firstName || tenant.lastName"> - </span>
        <span v-if="tenant.firstName">{{ tenant.firstName }}</span>
        <span v-if="tenant.lastName">{{ tenant.lastName }}</span>
      </span>
    </span>
    <span v-else-if="!tenant.isAdult">
      Child Occupier
      <span>
        <span v-if="tenant.firstName || tenant.lastName"> - </span>
        <span v-if="tenant.firstName">{{ tenant.firstName }}</span>
        <span v-if="tenant.lastName">{{ tenant.lastName }}</span>
      </span>
    </span>
  </span>
</template>

<script>
  export default {
    name: 'tenant-type',
    props: {
      'tenant': {
        type: Object,
        default: null
      }
    }
  }

</script>
