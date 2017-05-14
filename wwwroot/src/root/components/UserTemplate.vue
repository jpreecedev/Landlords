<template>
  <div>
    <span>{{ item.name }}</span>
    <abbr>{{ item.emailAddress }}</abbr>
  </div>
</template>

<script>
export default {
  props: { item: { required: true } }
}
</script>
