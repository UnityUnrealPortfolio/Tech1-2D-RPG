using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item_",menuName ="Items/Item")]
public class InventoryItemSO :ScriptableObject
{
    [Header("Item Properties")]
    public string ID;
    public string name;
    public string description;
    public Sprite icon;
    public ItemType itemType;
    public bool isConsumable;
    public bool isStackable;
    public int maxStack;

    [HideInInspector]public int CurrentQuantity;


    public InventoryItemSO CopyItem()
    {
        InventoryItemSO instance = Instantiate(this);
        return instance;
    }

    #region Virtual API
    public virtual bool UseItem()
    {
        return true;
    }

    public virtual void EquipItem()
    {

    }

    public virtual void RemoveItem()
    {

    }
    #endregion
}
public enum ItemType
{
    Weapon,
    Potion,
    Scroll,
    Ingredients,
    Treasure
}
