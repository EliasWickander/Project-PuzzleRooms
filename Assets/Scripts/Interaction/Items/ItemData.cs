using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "ScriptableObjects/InventorySystem/ItemData")]
public class ItemData : ScriptableObject
{
    public string m_name = "Item";
    public Sprite m_iconSprite = null;
}
