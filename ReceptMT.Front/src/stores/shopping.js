// stores/users.js
import { defineStore } from 'pinia'
// Import axios to make HTTP requests
import axios from "axios"

export const useShoppingStore = defineStore("shopping",{
    state: () => ({
        shoppingLists: [],
    }),
    getters: {
        getShoppingLists(state){
            return state.shoppingLists
          }
    },
    actions: {
        async fetchShoppingLists() {
            try {
              const data = await axios.get('https://rcpttest.azurewebsites.net/api/shopping/get')
                this.shoppingLists = JSON.parse(data.data)
              }
              catch (error) {
                alert(error)
                console.log(error)
            }
        }
    },
})