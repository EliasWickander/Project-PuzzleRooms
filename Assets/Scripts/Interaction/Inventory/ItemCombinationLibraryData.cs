using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combine Data", menuName = "ScriptableObjects/InventorySystem/ItemCombinationLibraryData")]
public class ItemCombinationLibraryData : ScriptableObject
{
    [Serializable]
    public class ItemCombination
    {
        public ItemData m_firstPart = null;
        public ItemData m_secondPart = null;
        public ItemData m_result = null;
    }

    [SerializeField]
    private List<ItemCombination> m_itemCombinations = new List<ItemCombination>();

    public List<ItemCombination> ItemCombinations => m_itemCombinations;

    public bool CheckCombinationResult(ItemData firstItem, ItemData secondItem, out ItemData result)
    {
        result = null;
        
        //If two items can be combined, return result
        foreach (ItemCombination combination in m_itemCombinations)
        {
            if (combination.m_firstPart == firstItem)
            {
                if (combination.m_secondPart == secondItem)
                {
                    result = combination.m_result;
                    return true;
                }
            }
            else
            {
                if (combination.m_secondPart == firstItem)
                {
                    if (combination.m_firstPart == secondItem)
                    {
                        result = combination.m_result;
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
