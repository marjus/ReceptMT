<template>
  <div class="card text-bg-light mb-3" style="max-width: 18rem;">
  <div class="card-header">{{recipe.title}} 
    
  </div>
  <div class="card-body">
    <p class="card-text">{{recipe.description}}</p>

    <button class="btn btn-link" @click="addToMenu(recipe)"> 
      <i class="fa fa-utensils"></i> Lägg till meny
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

const addToMenu = (recipe) => {
  menuStore.addToMenu(recipe)
}

const props = defineProps({
  type: String,
  recipe: {}
})
</script>

<style scoped>
</style>