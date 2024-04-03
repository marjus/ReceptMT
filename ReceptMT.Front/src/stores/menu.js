// stores/users.js
import { defineStore } from 'pinia'
// Import axios to make HTTP requests
import axios from "axios"

export const useMenuStore = defineStore("menu",{
    state: () => ({
        menuRecipes: [],
        menus: [],
        multipliers: [0.5, 1, 1.5, 2, 3, 4],
    }),
    getters: {
        getMenus(state){
            return state.menus
          },
        getMenuRecipes(state){
            return state.menuRecipes
        },
        recipeCount(state){
            return state.menuRecipes.length
        }
    },
    actions: {
        async createShoppingList(menu){
            // create a list of recipes and multipliers
            // send list to shopping api
        },
        async addToMenu(recipe){
            recipe.currentMultiplierIndex = 1;
            recipe.multiplier = this.multipliers[recipe.currentMultiplierIndex];
            
            this.menuRecipes.push(recipe);
        },
        async less(recipe){
            
            var recipeIndex = this.menuRecipes.indexOf(recipe);
            var menuRecipe = this.menuRecipes[recipeIndex];

            var multiplierIndex = menuRecipe.currentMultiplierIndex;

            if(multiplierIndex > 0)
                multiplierIndex = multiplierIndex -1;

            menuRecipe.currentMultiplierIndex = multiplierIndex;
            menuRecipe.multiplier = this.multipliers[multiplierIndex];
            
            this.menuRecipes[recipeIndex] = menuRecipe;
        },
        async more(recipe){
            var recipeIndex = this.menuRecipes.indexOf(recipe);

            var menuRecipe = this.menuRecipes[recipeIndex];
            
            var multiplierIndex = menuRecipe.currentMultiplierIndex;

            if(multiplierIndex < this.multipliers.length -1)
                multiplierIndex = multiplierIndex +1;

            menuRecipe.currentMultiplierIndex = multiplierIndex;

            menuRecipe.multiplier = this.multipliers[multiplierIndex];

            this.menuRecipes[recipeIndex] = menuRecipe;
        },
        async fetchMenus() {
            try {
              const data = await axios.get('https://rcptapi.azurewebsites.net/menus/get')
                this.menus = JSON.parse(data.data)
              }
              catch (error) {
                alert(error)
                console.log(error)
            }
        },
        async postMenu(menu) {
            try {
              const response = await axios.post('https://rcptapi.azurewebsites.net/menus/post', menu)
                menu.id = JSON.parse(response.data.id)
              }
              catch (error) {
                alert(error)
                console.log(error)
            }
        },
    },
})

