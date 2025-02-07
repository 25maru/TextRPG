using System;

public class Character
{
    public string Name { get; set; }
    public string Class { get; }
    public int Level { get; }

    public float BaseAttack { get; }
    public float BaseDefense { get; }
    public int Health { get; set; }
    public int MaxHealth { get; }
    public bool IsDead { get; set; }

    public int Gold { get; set; }
    public List<Item> Inventory { get; set; }

    public float TotalAttack => BaseAttack + GetTotalBonus("attack");
    public float TotalDefense => BaseDefense + GetTotalBonus("defense");

    public Character(string name, string job, int level, float attack, float defense, int health, int gold)
    {
        Name = name;
        Class = job;
        Level = level;
        BaseAttack = attack;
        BaseDefense = defense;
        Health = health;
        MaxHealth = health;
        Gold = gold;
        Inventory = new List<Item>();
    }
        
    // 착용한 장비의 공격력&방어력 보너스 합산
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

    // 장비 장착
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

    // 상태 보기
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

        Console.WriteLine($"체력   :  {Health}/{MaxHealth}");
        Console.WriteLine($"Gold   :  {Gold} G\n");

        GameManager.Instance.OptionText(0, "나가기");
    }

    // 휴식
    public void Rest()
    {
        if (Gold >= 500 && Health < 100)
        {
            Gold -= 500;

            while (Health < MaxHealth)
            {
                Health += 10;

                if (Health >= MaxHealth)
                    Health = 100;

                Console.Write($"\r현재 체력: {Health}/{MaxHealth}");

                Thread.Sleep(1000);
            }
            Console.WriteLine("\n휴식을 완료했습니다. 체력이 모두 회복되었습니다.");
        }
        else if (Gold < 500)
            GameManager.Instance.ErrorText("Gold 가 부족합니다.");
        else
            GameManager.Instance.ErrorText("이미 최대 체력입니다.");
    }
}
