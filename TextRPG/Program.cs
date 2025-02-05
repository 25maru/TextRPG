using System;
using System.Collections.Generic;
using System.Linq;

class Character
{
    public string Name { get; set; } = "플레이어";
    public string Class { get; set; } = "전사";
    public int Level { get; set; } = 1;
    public float BaseAttack { get; set; } = 10;
    public float BaseDefense { get; set; } = 5;
    public int Health { get; set; } = 100;
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
        Console.WriteLine("\n========================================");
        Thread.Sleep(500);

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("\n상태 보기");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Lv. {Level:00}");
        Console.WriteLine($"{Name} ( {Class} )\n");
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

public enum ItemType
{
    Weapon,
    Shield,
    Helmet,
    Armor
}

class Item
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

class Program
{
    static Character player = new Character();
    static List<Item> shopItems = new List<Item>
    {
        new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.Weapon, attackBonus: 2f, price: 500),
        new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", ItemType.Armor, defenseBonus: 5f, price: 750),
        new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", ItemType.Weapon, attackBonus: 5f, price: 1500),
        new Item("청동 거울", "공격을 막을 수 있을까요?", ItemType.Shield, defenseBonus: 7f, price: 1500),
        new Item("청동 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Armor, defenseBonus: 9f, price: 2000),
        new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.Weapon, attackBonus: 7f, price: 5000),
        new Item("스파르타의 방패", "스파르타의 전사들이 사용했다는 전설의 방패입니다.", ItemType.Shield, defenseBonus: 15f, price: 5000),
        new Item("스파르타의 투구", "스파르타의 전사들이 사용했다는 전설의 투구입니다.", ItemType.Helmet, defenseBonus: 15f, price: 5000),
        new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.Armor, defenseBonus: 15f, price: 5000),
        new Item("WA2000", "매우 강력합니다.", ItemType.Weapon, attackBonus: 2000f, price: 2000000),
    };

    static void Main(string[] args)
    {
        GetStarterItem();

        ShowOpening();

        while (true)
        {
            ShowMainMenu();

            // 선택지 입력
            string input = Console.ReadLine();
            Console.ResetColor();

            switch (input)
            {
                case "1":
                    ShowStatus();
                    break;
                case "2":
                    ShowInventory();
                    break;
                case "3":
                    ShowShop();
                    break;
                case "4":
                    ShowDungeon();
                    break;
                case "5":
                    ShowMine();
                    break;
                case "6":
                    ShowRest();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static void GetStarterItem()
    {
        // 초보자 아이템 획득
        player.Inventory.Add(new Item("장난감 칼", "한 번 휘두르면 부러질 것 같습니다.", ItemType.Weapon, attackBonus: 0f) { IsEquipped = true, CanSell = false });
        player.Inventory.Add(new Item("낡은 가죽 갑옷", "공격을 막기에는 너무 약합니다.", ItemType.Armor, defenseBonus: 0f) { IsEquipped = true, CanSell = false });
        player.Inventory.Add(new Item("아케인셰이드 투핸드소드", "스타포스 22성 레전드리 무기입니다.", ItemType.Weapon, attackBonus: 1223f) { CanSell = false });
    }

    // 오프닝
    static void ShowOpening()
    {
        Console.WriteLine("  ____  ____   _    ____ _____  _    ");
        Console.WriteLine(" / ___||  _ \\ / \\  |  _ \\_   _|/ \\   ");
        Console.WriteLine(" \\___ \\| |_) / _ \\ | |_) || | / _ \\  ");
        Console.WriteLine("  ___) |  __/ ___ \\|  _ < | |/ ___ \\ ");
        Console.WriteLine(" |____/|_| /_/   \\_\\_| \\_\\|_/_/   \\_\\\n");

        Console.WriteLine("스파르타 마을에 오신 것을 환영합니다.");
        Thread.Sleep(1000);

        Console.WriteLine("당신의 이름을 알려주세요.");
        Thread.Sleep(1000);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(">> ");
        player.Name = Console.ReadLine();
        Console.ResetColor();
    }

    // 메인 메뉴
    static void ShowMainMenu()
    {
        Console.WriteLine("\n========================================");
        Thread.Sleep(500);

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("\n마을");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("1");
        Console.ResetColor();
        Console.WriteLine(". 상태 보기");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("2");
        Console.ResetColor();
        Console.WriteLine(". 인벤토리");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("3");
        Console.ResetColor();
        Console.WriteLine(". 상점");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("4");
        Console.ResetColor();
        Console.WriteLine(". 던전");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("5");
        Console.ResetColor();
        Console.WriteLine(". 광산");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("6");
        Console.ResetColor();
        Console.WriteLine(". 휴식하기\n");

        Console.WriteLine("원하시는 행동을 입력해주세요.");

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(">> ");
    }

    // 1. 상태 보기
    static void ShowStatus()
    {
        while (true)
        {
            player.ShowStatus();

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Console.ResetColor();
            }
        }
    }

    // 2. 인벤토리
    static void ShowInventory()
    {
        while (true)
        {
            Console.WriteLine("\n========================================");
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n인벤토리");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            if (player.Inventory.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- 보유 중인 아이템이 없습니다.\n");
                Console.ResetColor();
            }
            else
            {
                int index = 1;
                foreach (var item in player.Inventory)
                {
                    string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                    string name = FormatString($"{item.Name}", 24);
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                    bonus = FormatString(bonus, 12);

                    Console.Write("- ");

                    if (item.IsEquipped)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{equippedMarker}");
                    Console.ResetColor();

                    Console.WriteLine($"{name} | {bonus} | {item.Description}");

                    index++;
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(player.Inventory.Count > 0 ? "1" : "");
            Console.ResetColor();
            Console.WriteLine(player.Inventory.Count > 0 ? ". 장착 관리" : "");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("0");
            Console.ResetColor();
            Console.WriteLine(". 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;
            else if (input == "1" && player.Inventory.Count > 0)
            {
                ManageEquipment();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Console.ResetColor();
            }
        }
    }

    // 2-1. 인벤토리(장착 관리)
    static void ManageEquipment()
    {
        while (true)
        {
            Console.WriteLine("\n========================================");
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n인벤토리 - 장착 관리");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                var item = player.Inventory[i];
                string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                string name = FormatString($"{item.Name}", 24);
                string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = FormatString(bonus, 12);

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{i + 1}");
                Console.ResetColor();

                if (i < 9)
                    Console.Write(".  ");
                else
                    Console.Write(". ");

                if (item.IsEquipped)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{equippedMarker}");
                Console.ResetColor();

                Console.WriteLine($"{name} | {bonus} | {item.Description}");
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n0");
            Console.ResetColor();
            Console.WriteLine(". 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= player.Inventory.Count)
            {
                player.EquipItem(player.Inventory[index - 1]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Console.ResetColor();
            }
        }
    }

    // 3. 상점
    static void ShowShop()
    {

        while (true)
        {
            Console.WriteLine("\n========================================");
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n상점");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string name = FormatString($"{item.Name}", 16);
                string bonus = item.AttackBonus > 0 ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = FormatString(bonus, 12);
                string description = FormatString($"{item.Description}", 50);
                string price = item.IsPurchased ? "구매완료" : $"{item.Price} G";

                if (item.IsPurchased)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"- {name} | {bonus} | {description} | {price}");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n1");
            Console.ResetColor();
            Console.WriteLine(". 아이템 구매");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("2");
            Console.ResetColor();
            Console.WriteLine(". 아이템 판매");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("0");
            Console.ResetColor();
            Console.WriteLine(". 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;
            else if (input == "1")
                PurchaseItem();
            else if (input == "2")
                SellItem();
            else
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.\n");
        }
    }

    // 3-1. 상점(아이템 구매)
    static void PurchaseItem()
    {
        while (true)
        {
            Console.WriteLine("\n========================================");
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n상점 - 아이템 구매");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string name = FormatString($"{item.Name}", 16);
                string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = FormatString(bonus, 12);
                string description = FormatString($"{item.Description}", 50);
                string price = item.IsPurchased ? "구매완료" : $"{item.Price} G";

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{i + 1}");

                Console.ResetColor();

                if (i < 9)
                    Console.Write(".  ");
                else
                    Console.Write(". ");

                if (item.IsPurchased)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{name} | {bonus} | {description} | {price}");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n0");
            Console.ResetColor();
            Console.WriteLine(". 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= shopItems.Count)
            {
                var item = shopItems[index - 1];

                if (item.IsPurchased)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.\n");
                }
                else if (player.Gold >= item.Price)
                {
                    player.Gold -= item.Price;
                    item.IsPurchased = true;
                    player.Inventory.Add(item);
                    Console.WriteLine("구매를 완료했습니다.\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Gold 가 부족합니다.\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Console.ResetColor();
            }
        }
    }

    // 3-2. 상점(아이템 판매)
    static void SellItem()
    {
        while (true)
        {
            Console.WriteLine("\n========================================");
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n상점 - 아이템 판매");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            if (player.Inventory.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("판매할 아이템이 없습니다.\n");
                Console.ResetColor();
            }
            else
            {
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    var item = player.Inventory[i];
                    int sellPrice = (int)(item.Price * 0.85);

                    string name = FormatString($"{item.Name}", 24);
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                    bonus = FormatString(bonus, 12);
                    string description = FormatString($"{item.Description}", 50);
                    string price = item.CanSell ? $"{sellPrice} G" : "판매불가";

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"{i + 1}");

                    Console.ResetColor();

                    if (i < 9)
                        Console.Write(".  ");
                    else
                        Console.Write(". ");

                    if (!item.CanSell)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{name} | {bonus} | {description} | {price}");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n0");
            Console.ResetColor();
            Console.WriteLine(". 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= player.Inventory.Count)
            {
                var item = player.Inventory[index - 1];

                if (!item.CanSell)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("판매할 수 없는 아이템입니다.");
                    Console.ResetColor();
                }
                else
                {
                    if (item.IsEquipped)
                    {
                        item.IsEquipped = false;
                        Console.WriteLine("현재 장착 중인 아이템입니다.");
                        Console.Write("장착을 해제하고 판매하시겠습니까? ");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("(");

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("0");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(". 나가기 / ");

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("1");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(". 판매하기)");

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(">> ");
                        string choice = Console.ReadLine();
                        Console.ResetColor();

                        if (choice == "0")
                            break;

                        if (choice == "1")
                        {
                            player.Gold += (int)(item.Price * 0.85);
                            item.IsPurchased = false;
                            player.Inventory.Remove(item);
                            Console.WriteLine("판매를 완료했습니다.");
                        }
                    }
                    else
                    {
                        player.Gold += (int)(item.Price * 0.85);
                        item.IsPurchased = false;
                        player.Inventory.Remove(item);
                        Console.WriteLine("판매를 완료했습니다.");
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Console.ResetColor();
            }
        }
    }

    // 4. 던전
    static void ShowDungeon()
    {
        while (true)
        {
            Console.WriteLine("\n========================================");
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n던전");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("이곳에서 입장할 던전을 선택할 수 있습니다.\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("1");
            Console.ResetColor();
            Console.WriteLine(". 초급 던전");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("2");
            Console.ResetColor();
            Console.WriteLine(". 중급 던전");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("3");
            Console.ResetColor();
            Console.WriteLine(". 상급 던전");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("4");
            Console.ResetColor();
            Console.Write(". ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("극악 던전");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("0");
            Console.ResetColor();
            Console.WriteLine(". 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.ResetColor();

            switch (input)
            {
                case "1":
                    Console.WriteLine("업데이트 예정입니다!");
                    break;
                case "2":
                    Console.WriteLine("업데이트 예정입니다!");
                    break;
                case "3":
                    Console.WriteLine("업데이트 예정입니다!");
                    break;
                case "4":
                    Console.WriteLine("업데이트 예정입니다!");
                    break;
                case "0":
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    // 5. 광산
    static void ShowMine()
    {
        // TODO 광산 기능 추가
    }

    // 6. 휴식하기
    static void ShowRest()
    {
        while (true)
        {
            Console.WriteLine("\n========================================");
            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n휴식하기");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("1");
            Console.ResetColor();
            Console.WriteLine(". 휴식하기");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("0");
            Console.ResetColor();
            Console.WriteLine(". 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;
            else if (input == "1")
                player.Rest();
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.\n");
                Console.ResetColor();
            }
        }
    }

    // 한글 정렬 도구
    static int GetDisplayWidth(string text)
    {
        int width = 0;
        foreach (char c in text)
        {
            // 한글은 2칸, 나머지는 1칸
            width += (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
        }
        return width;
    }

    // 한글 간격 추가 메서드
    static string FormatString(string text, int totalWidth)
    {
        int padding = totalWidth - GetDisplayWidth(text);
        return text + new string(' ', padding > 0 ? padding : 0);
    }
}
