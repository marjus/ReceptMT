using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ReceptMT.API.Util;

public class InputParser
{
    public struct IngredientResult
    {
        public bool isValid;
        public float? amount;
        public string unit;
        public string ingredient;
    }

    public struct ShoppingListItemResult
    {
        public float? amount { get; set; }
        public string unit { get; set; }
        public string product { get; set; }

    }

    private static string[] units = { "g", "ml", "cl", "dl", "hl", "l", "kg", "msk", "tsk", "krm", "stk", "st", "msk", "tesked", "matsked", "knippe", "burk", "paket", "påse", "pkt" };

    private static bool IsKnownUnit(string input)
    {
        var unit = units.SingleOrDefault(u => u.ToLower() == input.ToLower());

        if (unit != null)
            return true;

        return false;
    }

    private static float? GetAmount(string[] input)
    {
        if (input.Length <= 1)
            return null;

        var firstPart = input[0].Trim().Replace(',', '.').Replace("½", ".5");

        float amt;

        if (float.TryParse(firstPart, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out amt))
        {
            if (amt > 0)
                return amt;
        }

        return null;
    }

    private static string GetUnit(string[] input)
    {
        if (input.Length > 0)
        {
            if (IsKnownUnit(input[0]))
                return input[0];
        }
        return "";
    }

    public static List<IngredientResult> ParseIngredientList(string ingredientList)
    {
        if (string.IsNullOrEmpty(ingredientList))
            return null;

        ingredientList = ingredientList.Replace('\t', ' ');
        var ings = ingredientList.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        var listOfIngredients = new List<IngredientResult>();

        foreach(var ing in ings)
        {
            if(!string.IsNullOrEmpty(ing.Trim()))
                listOfIngredients.Add(ParseIngredient(ing.Trim()));
        }
        return listOfIngredients;
    }

    public static IngredientResult ParseIngredient(string newIngredient)
    {
        IngredientResult r = new IngredientResult { isValid = true };

        if (string.IsNullOrEmpty(newIngredient))
            return r;

        var ingParts = newIngredient.Split(' ');

        var amt = GetAmount(ingParts);

        if (!amt.HasValue)
        { 
            r.isValid = false;
        }
        else
        {
            r.amount = amt.Value;

            ingParts = ingParts.Skip(1).ToArray();
            newIngredient = newIngredient.Substring(r.amount.ToString().Length).Trim();
        }

        r.unit = GetUnit(ingParts);

        if(r.unit != null)
        {
            r.ingredient = newIngredient.Substring(r.unit.Length).Trim();
        }

        return r;
    }

    public static ShoppingListItemResult parseProduct(string newProduct)
    {
        var product = newProduct.ToLower();

        ShoppingListItemResult r = new ShoppingListItemResult();

        if (string.IsNullOrEmpty(newProduct))
            return r;

        var prodParts = product.Split(' ');
        if (prodParts.Length == 1)
        { 
            r.product = product;
            return r;
        }

        // assuming newProduct is not null
        float amt;

        if(float.TryParse(prodParts[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out amt))
        {
            // first symbol was a float, use it as amount
            r.amount = amt;

            // ProductParts counter = 1 if there is no unit, set to 2 if there is a known unit
            var i = 1;
            // 
            if (IsKnownUnit(prodParts[1]))
            { 
                r.unit = prodParts[1];
                i = 2;
            }

            for (; i < prodParts.Length; i++)
            {
                r.product += prodParts[i] + " ";
            }
            r.product = r.product.Trim();
        }
        else
        {
            r.product = product;
        }

        return r;
    }
}