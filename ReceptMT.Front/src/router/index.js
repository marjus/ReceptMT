import { createRouter, createWebHistory } from "vue-router";

import RecipeList from "@/components/RecipeList.vue";
import MenuList from "@/components/MenuList.vue";
import ShoppingList from "@/components/ShoppingList.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/recipes",
      name: "recipes",
      component: RecipeList,
    },
    {
      path: "/shopping",
      name: "shopping",
      component: ShoppingList,
    },
    {
      path: "/menus",
      name: "Menus",
      component: MenuList,
    },
  ],
});

export default router;
