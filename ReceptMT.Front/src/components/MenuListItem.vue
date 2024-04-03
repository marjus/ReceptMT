<template>
  <div class="card text-bg-light mb-3" style="max-width: 18rem;">
  <div class="card-header">{{recipe.title}} 
    
  </div>
  <div class="card-body">
    <p class="card-text">{{recipe.description}}</p>

    <span >
      Portioner x {{ recipe.multiplier }}
    </span>

    <button class="btn btn-danger ms-2" @click="less(recipe)"> 
      -
    </button>
    <button class="btn btn-success ms-2" @click="more(recipe)"> 
      +
    </button>
    <span v-if="recipe.category==='Huvudrätt'" class="badge bg-primary float-end">
      {{ recipe.category }}
    </span>
    <span v-else class="badge bg-secondary float-end">
      {{ recipe.category }} 
    </span>


    
  </div>
</div>

</template>

<script setup>

import { useMenuStore } from "@/stores/menu";
const menuStore = useMenuStore();

const less = (recipe) => {
  menuStore.less(recipe)
}

const more = (recipe) => {
  menuStore.more(recipe)
}

const props = defineProps({
  type: String,
  recipe: {}
})
</script>

<style scoped>
</style>