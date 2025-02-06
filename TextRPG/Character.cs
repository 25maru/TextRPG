using System;

public class Character
{
    public string Name { get; set; } = "플레이어";
    public string Class { get; } = "전사";
    public int Level { get; } = 1;
    public float BaseAttack { get; } = 10f;
    public float BaseDefense { get; } = 5f;
    public int Health { get; set; } = 100;
    public float MaxHealth { get; } = 100;
    public int Gold { get; set; } = 1500;

    public List<Item> Inventory { get; set; } = new List<Item>();

    public float TotalAttack => BaseAttack + GetTotalBonus("attack");
    public float TotalDefense => BaseDefense + GetTotalBonus("defense");

    private float GetTotalBonus(string type)
    {
        float total = 0f;
        foreach (var item in Inventory)
        {
            if (item.IsEquipped)
            {
                if (type == "attack") total += item.AttackBonus;
                if (type == "defense") total += item.DefenseBonus;
            }
        }
        return total;
    }

    // Todo: 던전 클리어를 통해 레벨업이 이루어지도록 구현
    public void LevelUp()
    {
        // BaseAttack += 0.5f;
        // BaseDefense += 1f;
    }

    public void EquipItem(Item item)
    {
        foreach (var equippedItem in Inventory)
        {
            if (equippedItem.IsEquipped && equippedItem.Type == item.Type)
            {
                equippedItem.IsEquipped = false;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"'{equippedItem.Name}'");
                Console.ResetColor();

                Console.WriteLine("아이템의 장착을 해제했습니다.");
            }
        }

        item.IsEquipped = true;

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"'{item.Name}'");
        Console.ResetColor();

        Console.WriteLine("아이템을 장착했습니다.");
    }

    public void ShowStatus()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Lv. {Level:00}");
        Console.WriteLine($"{Name} ({Class})\n");
        Console.ResetColor();

        float attackBonus = TotalAttack - BaseAttack;
        float defenseBonus = TotalDefense - BaseDefense;

        if (attackBonus > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"공격력 :  {TotalAttack} ");
            Console.ResetColor();

            Console.Write($"({BaseAttack} ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"+{attackBonus}");
            Console.ResetColor();

            Console.WriteLine(")");
        }
        else
            Console.WriteLine($"공격력 :  {TotalAttack}");

        if (defenseBonus > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"방어력 :  {TotalDefense} ");
            Console.ResetColor();

            Console.Write($"({BaseDefense} ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"+{defenseBonus}");
            Console.ResetColor();

            Console.WriteLine(")");
        }
        else
            Console.WriteLine($"방어력 :  {TotalDefense}");

        Console.WriteLine($"체력   :  {Health}");
        Console.WriteLine($"Gold   :  {Gold} G\n");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("0");
        Console.ResetColor();
        Console.WriteLine(". 나가기\n");
    }

    public void Rest()
    {
        if (Gold >= 500 && Health < 100)
        {
            Gold -= 500;
            Health = 100;
            Console.WriteLine("휴식을 완료했습니다. 체력이 모두 회복되었습니다.\n");
        }
        else if (Gold < 500)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Gold 가 부족합니다.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("이미 최대 체력입니다.");
            Console.ResetColor();
        }
    }
}
