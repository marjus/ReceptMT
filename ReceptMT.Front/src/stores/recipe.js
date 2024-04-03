// stores/users.js
import { defineStore } from 'pinia'
// Import axios to make HTTP requests
import axios from "axios"

export const useRecipeStore = defineStore("recipe",{
    state: () => ({
        recipes: [],
    }),
    getters: {
        getRecipes(state){
            return state.recipes
          }
    },
    actions: {
        async fetchRecipes() {
            try {
              const data = await axios.get('https://rcptapi.azurewebsites.net/Recipes')
                this.recipes = data.data
              }
              catch (error) {
                alert(error)
                console.log(error)
            }
        }
    },
})