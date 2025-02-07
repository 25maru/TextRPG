using System;

public enum ItemType
{
    Weapon,
    Shield,
    Helmet,
    Armor
}

public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ItemType Type { get; set; }
    public float AttackBonus { get; set; }
    public float DefenseBonus { get; set; }

    public int Price { get; set; }
    public bool IsEquipped { get; set; }
    public bool IsPurchased { get; set; }
    public bool CanSell { get; set; }

    public Item(string name, string description, ItemType type, float attackBonus = 0, float defenseBonus = 0, int price = 0)
    {
        Name = name;
        Description = description;
        Type = type;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
        Price = price;
        IsEquipped = false;
        IsPurchased = false;
        CanSell = true;
    }
}
