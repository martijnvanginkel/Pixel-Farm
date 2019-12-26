using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Ingredient
{
    public ObjectData Item;
    public int Amount;
}

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public string RecipeName;
    public string Description;

    public List<Ingredient> Ingredients = new List<Ingredient>();
    public ObjectData ResultObject;

    //private List<DigitalItem> m_UsedIngredients = new List<DigitalItem>();

    public CraftingRecipe CanCraftRecipe(List<DigitalItem> craftingSlots)
    {
        int index = 0;
        int amount = Ingredients.Count;

       // m_UsedIngredients.Clear();
        foreach (Ingredient ingredient in Ingredients)
        {
            foreach (DigitalItem slot in craftingSlots)
            {
                if (ingredient.Item == slot.ObjectData)
                {
                    if (slot.SlotAmount >= ingredient.Amount)
                    {
                        //m_UsedIngredients.Add(slot);
                        index++;
                        continue;
                    }
                }
            }
        }
        if (index >= amount)
        {
            return this;
        }
        //m_UsedIngredients.Clear();
        return null;
    }

    public List<Ingredient> GetUsedIngredients()
    {
        return Ingredients;
    }
}
