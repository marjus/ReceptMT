<template>
    <div class="row">
        filter - Menu: {{menuStore.menuRecipes.length}} recept
    </div>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-lg-4 mt-2 recipecard" v-for="(recipe) in recipeStore.recipes" :key="recipe.id">
            <RecipeItem :recipe="recipe"></RecipeItem>
        </div>
    </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { useRecipeStore } from "@/stores/recipe";
import { useMenuStore } from "@/stores/menu";

import RecipeItem from "@/components/RecipeItem.vue";

const recipeStore = useRecipeStore();
const menuStore = useMenuStore();

onMounted(() => {
  recipeStore.fetchRecipes();
});

</script>

<style scoped>
</style>