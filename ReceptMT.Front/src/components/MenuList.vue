<template>
    <h1>Menus</h1>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-lg-4 mt-2 recipecard" v-for="(recipe) in menuStore.menuRecipes" :key="recipe.id">
            <MenuListItem :recipe="recipe"></MenuListItem>
        </div>
    </div>
    <div class="row">
        <button @click="createShoppingList(menu)">Skapa shopping lista</button>
    </div>
</template>

<script setup>

import { useMenuStore } from "@/stores/menu";

import MenuListItem from "@/components/MenuListItem.vue";

const menuStore = useMenuStore();

const createShoppingList = (menu) => {
  menuStore.createShoppingList(menu)
}

</script>