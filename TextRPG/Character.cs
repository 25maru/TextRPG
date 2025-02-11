using System.Numerics;
using Tool;

public class Character
{
    public string Name { get; set; }
    public string Class { get; set; }
    public int Level { get; set; }

    public float BaseAttack { get; set; }
    public float BaseHealth { get; set; }
    public float Health { get; set; }
    public bool IsBattle { get; set; }
    public bool IsDead { get; set; }

    public int Gold { get; set; }
    public List<Item> Inventory { get; set; }
    public List<Item> ActivePotionBuffs { get; set; }

    public float TotalAttack => BaseAttack + GetTotalBonus("attack");
    public float TotalHealth => BaseHealth + GetTotalBonus("health");

    public Character(string name, string job, int level, float attack, float health, int gold)
    {
        Name = name;
        Class = job;
        Level = level;
        BaseAttack = attack;
        BaseHealth = health;
        Health = health;
        Gold = gold;
        Inventory = new List<Item>();
        ActivePotionBuffs = new List<Item>();
    }

    /// <summary>
    /// 착용한 장비와 레벨에 따른 공격력, 체력 보너스 합산
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private float GetTotalBonus(string type)
    {
        float total = 0f;
        foreach (var item in Inventory)
        {
            if (item.IsEquipped)
            {
                if (type == "attack") total += item.AttackBonus;
                if (type == "health") total += item.HealthBonus;
            }
        }

        if (type == "attack") total += (Level - 1) * 2f;
        if (type == "health") total += (Level - 1) * 5f;

        return total;
    }

    /// <summary>
    /// TODO: 던전 클리어 횟수에 따라 레벨업 되도록 구현
    /// </summary>
    public void LevelUp()
    {
        Level += 1;
    }

    /// <summary>
    /// 장착된 같은 타입 아이템을 해제하면서 새 아이템 장착
    /// </summary>
    /// <param name="item">장착할 아이템</param>
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

    /// <summary>
    /// 플레이어의 상태 표시
    /// </summary>
    public void ShowStatus()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Lv. {Level:00}");
        Console.WriteLine($"{Name} ({Class})\n");
        Console.ResetColor();

        float attackBonus = TotalAttack - BaseAttack;
        float healthBonus = TotalHealth - BaseHealth;

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

        if (healthBonus > 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"체력   :  {Health}/{TotalHealth} ");
            Console.ResetColor();

            Console.Write($"({BaseHealth} ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"+{healthBonus}");
            Console.ResetColor();

            Console.WriteLine(")");
        }
        else
            Console.WriteLine($"체력   :  {Health}/{TotalHealth}");

        Console.WriteLine($"Gold   :  {Gold} G\n");

        Utils.OptionText(0, "나가기");
    }

    /// <summary>
    /// 포션 사용 시 호출
    /// </summary>
    /// <param name="type">포션의 종류 (회복: Heal, 강화: Power)</param>
    public void UsePotion(string type)
    {
        // 회복 포션
        if (type == "Heal")
        {
            var potions = Inventory.Where(item => item.Type == ItemType.Potion && item.HealthBonus > 0).ToList();

            // 사용 가능한 포션이 없을 경우 메시지 출력 후 종료
            if (potions.Count == 0)
            {
                Utils.ErrorText("사용할 수 있는 회복 포션이 없습니다.");
                return;
            }

            for (int i = 0; i < potions.Count; i++)
            {
                Utils.OptionText(i + 1, $"{potions[i].Name} (+{potions[i].HealthBonus} 회복)");
            }

            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, potions.Count);

            if (input == 0)
                return;

            // 선택한 포션 가져오기
            var selectedPotion = potions[input - 1];

            Health = Math.Min(Health + 50, TotalHealth);

            Inventory.Remove(selectedPotion); // 인벤토리에서 제거

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"'{selectedPotion.Name}'");
            Console.ResetColor();

            Console.Write($"을 사용하여 체력이 ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{selectedPotion.AttackBonus}");
            Console.ResetColor();

            Console.WriteLine($" 회복했습니다!");
        }

        // 강화 포션
        if (type == "Power")
        {
            if (!IsBattle)
            {
                Utils.ErrorText("강화 포션은 던전에서만 사용할 수 있습니다.");
                return;
            }

            var potions = Inventory.Where(item => item.Type == ItemType.Potion && item.AttackBonus > 0).ToList();

            // 사용 가능한 포션이 없을 경우 메시지 출력 후 종료
            if (potions.Count == 0)
            {
                Utils.ErrorText("사용할 수 있는 강화 포션이 없습니다.");
                return;
            }

            for (int i = 0; i < potions.Count; i++)
            {
                Utils.OptionText(i + 1, $"{potions[i].Name} (+{potions[i].AttackBonus} 공격력)");
            }

            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, potions.Count);

            if (input == 0)
                return;

            // 선택한 포션 가져오기
            var selectedPotion = potions[input - 1];

            // 포션을 장착하여 버프 적용
            selectedPotion.IsEquipped = true;

            // 전투 종료 시 포션을 해제할 수 있도록 등록
            ActivePotionBuffs.Add(selectedPotion);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"'{selectedPotion.Name}'");
            Console.ResetColor();

            Console.Write($"을 사용하여 공격력이 ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{selectedPotion.AttackBonus}");
            Console.ResetColor();

            Console.WriteLine($" 증가했습니다!");
        }

        // 랜덤박스
        if (type == "RandomBox")
        {
            var boxes = Inventory.Where(item => item.Type == ItemType.Potion &&  item.HealthBonus == 0 && item.AttackBonus == 0).ToList();

            if (boxes.Count == 0)
            {
                Utils.ErrorText("사용할 수 있는 랜덤박스가 없습니다.");
                return;
            }

            Console.Write("랜덤박스를 사용하시겠습니까? (남은 개수: ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(boxes.Count);
            Console.ResetColor();
            Console.WriteLine(")");

            Utils.OptionText(1, "사용하기");
            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, 1);

            if (input == 0)
                return;


            var box = boxes[0];

            Health = Math.Min(Health + 50, TotalHealth);

            Inventory.Remove(box); // 인벤토리에서 제거

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"'{box.Name}'");
            Console.ResetColor();

            Console.Write($"를 사용하여 플레이어의 상태가 다음과 같이 변경되었습니다.");

            Random rand = new Random();
            float randAttack = (rand.Next(1, 25) - 10) * 0.1f * TotalAttack;
            float randHealth = (rand.Next(1, 25) - 10) * 0.1f * TotalHealth;
            float randGold = (rand.Next(1, 25) - 10) * 0.1f * Gold;

            Console.Write($"공격력: {TotalAttack} -> ");
            BaseAttack += (int)randAttack;
            if (randAttack > 0) Console.ForegroundColor = ConsoleColor.DarkGreen;
            else if (randAttack < 0) Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(TotalAttack);
            Console.ResetColor();

            Console.Write($"체력:   {TotalHealth} -> ");
            BaseHealth += (int)randHealth;
            if (BaseHealth <= 0) BaseHealth = 1f;
            if (randAttack > 0) Console.ForegroundColor = ConsoleColor.DarkGreen;
            else if (randAttack < 0) Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(TotalHealth);
            Console.ResetColor();

            Console.Write($"Gold:   {Gold} -> ");
            Gold += (int)randGold;
            if (randAttack > 0) Console.ForegroundColor = ConsoleColor.DarkGreen;
            else if (randAttack < 0) Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(Gold);
            Console.ResetColor();
        }
    }

    /// <summary>
    /// 적용된 포션 버프들을 전투 종료 시 해제하기 위해 호출
    /// </summary>
    public void ResetPotionBuffs()
    {
        foreach (var potion in ActivePotionBuffs)
        {
            if (potion.IsEquipped)
            {
                potion.IsEquipped = false; // 포션을 장착 해제
            }

            Inventory.Remove(potion); // 인벤토리에서 제거
        }

        ActivePotionBuffs.Clear(); // 버프 목록 초기화

        Console.WriteLine("전투가 종료되어 모든 포션 효과가 사라졌습니다.");
    }

    /// <summary>
    /// 골드를 소모하여 체력 회복
    /// </summary>
    public void Rest()
    {
        if (Gold >= 500 && Health < TotalHealth)
        {
            Gold -= 500;

            while (Health < TotalHealth)
            {
                Health += 10;

                if (Health >= TotalHealth)
                    Health = TotalHealth;

                Console.Write($"\r현재 체력: {Health}/{TotalHealth}");

                Thread.Sleep(500);
            }
            Console.WriteLine("\n휴식을 완료했습니다. 체력이 모두 회복되었습니다.");
        }
        else if (Gold < 500)
            Utils.ErrorText("Gold 가 부족합니다.");
        else
            Utils.ErrorText("이미 최대 체력입니다.");
    }
}
