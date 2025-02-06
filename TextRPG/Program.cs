using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        GameManager gameManager = new GameManager();
        gameManager.GameStart();
    }
}

class GameManager
{
    private Character player;
    private List<Item> shopItems;
    
    public GameManager()
    { 
        // 캐릭터 (이름, 직업, 레벨, 공격력, 방어력, 체력, 골드)
        player = new Character("플레이어", "전사", 1, 10f, 5f, 100, 1500);

        // 아이템 (이름, 설명, 타입, 보너스: 값, 가격: 값)
        shopItems = new List<Item>()
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
            new Item("아케인셰이드 투핸드소드", "스타포스 22성 레전드리 무기입니다.", ItemType.Weapon, attackBonus: 1223f, price: 2000000),
        };
    }

    #region Style
    public void ShowHeader(string title, string description)
    {
        Console.WriteLine("\n========================================================================================================================");
        Thread.Sleep(500);

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine($"\n{title}");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"{description}\n");
        Console.ResetColor();
    }

    public void ShowFooter()
    {
        Console.WriteLine("\n원하시는 행동을 입력해주세요.");

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(">> ");
    }

    public void OptionText(int index, string name)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(index);
        Console.ResetColor();
        Console.WriteLine($". {name}");
    }
    
    public void InfoText(string text)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public void ErrorText(string text)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public int GetDisplayWidth(string text)
    {
        int width = 0;
        foreach (char c in text)
        {
            // 한글은 2칸, 나머지는 1칸
            width += (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
        }
        return width;
    }

    public string FormatString(string text, int totalWidth)
    {
        int padding = totalWidth - GetDisplayWidth(text);
        return text + new string(' ', padding > 0 ? padding : 0);
    }
    #endregion

    #region Scene
    // 오프닝
    public void GameStart()
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

        GetStarterItem();
        ShowMainMenu();
    }
    
    private void GetStarterItem()
    {
        // 초보자 아이템 획득
        player.Inventory.Add(new Item("장난감 칼", "한 번 휘두르면 부러질 것 같습니다.", ItemType.Weapon, attackBonus: 0f) { IsEquipped = true, CanSell = false });
        player.Inventory.Add(new Item("낡은 가죽 갑옷", "공격을 막기에는 너무 약합니다.", ItemType.Armor, defenseBonus: 0f) { IsEquipped = true, CanSell = false });
    }

    // 메인 메뉴
    public void ShowMainMenu()
    {
        while (true)
        {
            ShowHeader("마을", "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            OptionText(1, "상태 보기");
            OptionText(2, "인벤토리");
            OptionText(3, "상점");
            OptionText(4, "던전");
            OptionText(5, "광산");
            OptionText(6, "휴식");

            ShowFooter();

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
                    ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }

    // 1. 상태 보기
    public void ShowStatus()
    {
        while (true)
        {
            ShowHeader("상태 보기", "캐릭터의 정보가 표시됩니다.");

            player.ShowStatus();

            ShowFooter();

            string input = Console.ReadLine();
            Console.ResetColor();

            switch (input)
            {
                case "0":
                    return;
                default:
                    ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }

    // 2. 인벤토리
    public void ShowInventory()
    {
        while (true)
        {
            ShowHeader("인벤토리", "보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("[아이템 목록]");

            string longestItem = player.Inventory.OrderByDescending(item => GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;

            if (player.Inventory.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("- 보유 중인 아이템이 없습니다.\n");
                Console.ResetColor();
            }
            else
            {
                foreach (var item in player.Inventory)
                {
                    string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                    string name = FormatString($"{item.Name}", GetDisplayWidth(longestItem));
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                    bonus = FormatString(bonus, 8 + longestBonus);

                    Console.Write("- ");

                    if (item.IsEquipped)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{equippedMarker}");
                    Console.ResetColor();

                    Console.WriteLine($"{name} | {bonus} | {item.Description}");
                }

                Console.WriteLine();
            }

            if (player.Inventory.Count > 0)
                OptionText(1, "장착 관리");

            OptionText(0, "나가기");

            ShowFooter();

            string input = Console.ReadLine();
            Console.ResetColor();

            switch (input)
            {
                case "1":
                    if (player.Inventory.Count > 0) ManageEquipment();
                    break;
                case "0":
                    return;
                default:
                    ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }
    
    // 2-1. 인벤토리(장착 관리)
    public void ManageEquipment()
    {
        while (true)
        {
            ShowHeader("인벤토리 - 장착 관리", "보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("[아이템 목록]");

            string longestItem = player.Inventory.OrderByDescending(item => GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                var item = player.Inventory[i];
                string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                string name = FormatString($"{item.Name}", GetDisplayWidth(longestItem));
                string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = FormatString(bonus, 8 + longestBonus);

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

            Console.WriteLine();

            OptionText(0, "나가기");

            ShowFooter();

            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= player.Inventory.Count)
            {
                var item = player.Inventory[index - 1];

                if (item.IsEquipped)
                {
                    item.IsEquipped = false;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"'{item.Name}'");
                    Console.ResetColor();

                    Console.WriteLine("아이템의 장착을 해제했습니다.");
                }
                else
                    player.EquipItem(item);
            }
            else
                ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
        }
    }

    // 3. 상점
    public void ShowShop()
    {
        while (true)
        {
            ShowHeader("상점", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            string longestItem = shopItems.OrderByDescending(item => GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = shopItems.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
            string longestDescription = shopItems.OrderByDescending(item => GetDisplayWidth(item.Description)).First().Description;

            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string name = FormatString($"{item.Name}", GetDisplayWidth(longestItem));
                string bonus = item.AttackBonus > 0 ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = FormatString(bonus, 8 + longestBonus);
                string description = FormatString($"{item.Description}", GetDisplayWidth(longestDescription));
                string price = item.IsPurchased ? "구매완료" : $"{item.Price} G";

                if (item.IsPurchased)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"- {name} | {bonus} | {description} | {price}");
                Console.ResetColor();
            }

            Console.WriteLine();

            OptionText(1, "아이템 구매");
            OptionText(2, "아이템 판매");
            OptionText(0, "나가기");

            ShowFooter();

            string input = Console.ReadLine();
            Console.ResetColor();

            switch (input)
            {
                case "1":
                    PurchaseItem();
                    break;
                case "2":
                    SellItem();
                    break;
                case "0":
                    return;
                default:
                    ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }

    // 3-1. 상점(아이템 구매)
    public void PurchaseItem()
    {
        while (true)
        {
            ShowHeader("상점 - 아이템 구매", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            string longestItem = shopItems.OrderByDescending(item => GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = shopItems.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
            string longestDescription = shopItems.OrderByDescending(item => GetDisplayWidth(item.Description)).First().Description;

            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];

                string name = FormatString($"{item.Name}", GetDisplayWidth(longestItem));
                string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = FormatString(bonus, 8 + longestBonus);
                string description = FormatString($"{item.Description}", GetDisplayWidth(longestDescription));
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

            ShowFooter();

            string input = Console.ReadLine();
            Console.ResetColor();

            if (input == "0")
                break;

            if (int.TryParse(input, out int index) && index >= 1 && index <= shopItems.Count)
            {
                var item = shopItems[index - 1];

                if (item.IsPurchased)
                    ErrorText("이미 구매한 아이템입니다.");
                else if (player.Gold >= item.Price)
                {
                    player.Gold -= item.Price;
                    item.IsPurchased = true;
                    player.Inventory.Add(item);
                    Console.WriteLine("구매를 완료했습니다.");
                }
                else
                    ErrorText("Gold 가 부족합니다.");
            }
            else
                ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
        }
    }

    // 3-2. 상점(아이템 판매)
    public void SellItem()
    {
        while (true)
        {
            ShowHeader("상점 - 아이템 판매", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            if (player.Inventory.Count == 0)
                InfoText("판매할 아이템이 없습니다.");
            else
            {
                string longestItem = player.Inventory.OrderByDescending(item => GetDisplayWidth(item.Name)).First().Name;
                int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
                string longestDescription = player.Inventory.OrderByDescending(item => GetDisplayWidth(item.Description)).First().Description;

                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    var item = player.Inventory[i];
                    int sellPrice = (int)(item.Price * 0.85);

                    string name = FormatString($"{item.Name}", GetDisplayWidth(longestItem));
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                    bonus = FormatString(bonus, 8 + longestBonus);
                    string description = FormatString($"{item.Description}", GetDisplayWidth(longestDescription));
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

            Console.WriteLine();

            OptionText(0, "나가기");

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
                    ErrorText("판매할 수 없는 아이템입니다.");
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
                        Console.Write("Y");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(". 판매하기 / ");

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("N");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(". 나가기)");

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(">> ");
                        string choice = Console.ReadLine();
                        Console.ResetColor();

                        if (choice == "Y")
                        {
                            player.Gold += (int)(item.Price * 0.85);
                            item.IsPurchased = false;
                            player.Inventory.Remove(item);
                            Console.WriteLine("판매를 완료했습니다.");
                        }
                        else if (choice == "N")
                            break;
                        else
                            ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
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
                ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
        }
    }

    // 4. 던전
    public void ShowDungeon()
    {
        while (true)
        {
            ShowHeader("던전", "이곳에서 입장할 던전을 선택할 수 있습니다.");

            OptionText(1, "초급 던전");
            OptionText(2, "중급 던전");
            OptionText(3, "상급 던전");
            OptionText(4, "지옥 던전");
            OptionText(0, "나가기");

            ShowFooter();

            string input = Console.ReadLine();
            Console.ResetColor();

            switch (input)
            {
                case "1":
                    InfoText("'초급 던전'은 업데이트 예정입니다!");
                    break;
                case "2":
                    InfoText("'중급 던전'은 업데이트 예정입니다!");
                    break;
                case "3":
                    InfoText("'상급 던전'은 업데이트 예정입니다!");
                    break;
                case "4":
                    InfoText("'극악 던전'은 업데이트 예정입니다!");
                    break;
                case "0":
                    return;
                default:
                    ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }

    // 5. 광산
    public void ShowMine()
    {
        while (true)
        {
            ShowHeader("광산", "이곳에서 골드를 획득할 수 있습니다.");

            OptionText(1, "기본 광산");
            OptionText(2, "요일 광산");
            OptionText(0, "나가기");

            ShowFooter();

            string input = Console.ReadLine();
            Console.ResetColor();

            switch (input)
            {
                case "1":
                    InfoText("'기본 광산'은 업데이트 예정입니다!");
                    break;
                case "2":
                    InfoText("'요일 광산'은 업데이트 예정입니다!");
                    break;
                case "0":
                    return;
                default:
                    ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }

    // 6. 휴식
    public void ShowRest()
    {
        while (true)
        {
            // 체력 회복 테스트를 위한 임시 코드
            player.Health -= 40;

            ShowHeader("휴식", "500 G 를 내면 체력을 회복할 수 있습니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            OptionText(1, "휴식하기");
            OptionText(0, "나가기");

            ShowFooter();

            string input = Console.ReadLine();
            Console.ResetColor();

            switch (input)
            {
                case "1":
                    player.Rest();
                    break;
                case "0":
                    return;
                default:
                    ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }
    #endregion
}
