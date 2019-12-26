using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTableUI : SlotsHolder
{
    [SerializeField] private List<CraftingRecipe> m_CraftingRecipes = new List<CraftingRecipe>();
    [SerializeField] private CraftingResultSlot m_ResultSlot;

    private bool m_Generating;
    private CraftingRecipe m_LoadedRecipe;
    [SerializeField] private float m_LoadPercentage;
    [SerializeField] private RectTransform m_LoadingBar;
    [SerializeField] private TMPro.TextMeshProUGUI m_PercentageText;

    private void OnEnable()
    {
        InventorySlot.OnInvSlotClicked += AddItemToTable;
    }

    private void OnDisable()
    {
        InventorySlot.OnInvSlotClicked -= AddItemToTable;
    }

    private void Update()
    {
        if (m_Generating)
        {
            LoadRecipe();
        }
    }

    public void RetryRecipe()
    {
        ResetRecipe();
        StartCraftingIfPossible();
    }

    private void ResetRecipe()
    {
        m_Generating = false;
        m_LoadPercentage = 0f;
        m_LoadedRecipe = null;
        m_LoadingBar.localScale = new Vector3(0f, 1f, 1f);
        m_PercentageText.text = "0";
    }

    private void DoneLoadingRecipe()
    {
        Craft(m_LoadedRecipe);
        RetryRecipe();
    }

    private void LoadRecipe()
    {
        m_LoadPercentage += Time.deltaTime * 10f;
        m_LoadingBar.localScale = new Vector3(m_LoadPercentage / 100f, 1f, 1f);
        m_PercentageText.text = Mathf.RoundToInt(m_LoadPercentage).ToString();
        if (m_LoadPercentage >= 100f)
        {
            DoneLoadingRecipe();
        }
    }

    private void StartLoadingRecipe(CraftingRecipe recipe)
    {
        m_Generating = true;
        m_LoadedRecipe = recipe;
    }

    private void RemoveIngredients(CraftingRecipe recipe)
    {
        List<Ingredient> m_UsedIngredients = recipe.GetUsedIngredients();

        foreach (DigitalItem slot in m_SlotList)
        {
            foreach (Ingredient ingredient in m_UsedIngredients)
            {
                if (slot.ObjectData == ingredient.Item)
                {
                    RemoveMultipleItems(slot, ingredient.Amount);
                    if (!slot.SlotIsTaken)
                    {
                        slot.EnableBackground(false);
                    }
                    continue;
                }
            }
        }
    }

    private void Craft(CraftingRecipe recipe)
    {
        RemoveIngredients(recipe);
        if (m_ResultSlot.SlotIsTaken)
        {
            m_ResultSlot.IncreaseAmount(1);
        }
        else
        {
            m_ResultSlot.FillSlot(recipe.ResultObject, 1);
        }
    }

    private void StartCraftingIfPossible()
    {
        foreach (CraftingRecipe recipe in m_CraftingRecipes)
        {
            CraftingRecipe result = recipe.CanCraftRecipe(m_SlotList);
            if (result != null)
            {
                StartLoadingRecipe(recipe);
                return;
            }
        }
    }

    private void AddItemToTable(DigitalItem item)
    {
        AddItem(item.ObjectData, 1);
        Inventory.Instance.RemoveSingleItem(item);
        StartCraftingIfPossible();
    }

    public void RemoveItemFromTable(DigitalItem item)
    {
        Inventory.Instance.AddItem(item.ObjectData, 1);
        RemoveSingleItem(item);
    }
}
