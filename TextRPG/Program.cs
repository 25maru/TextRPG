using Tool;

public class Program
{
    static void Main(string[] args)
    {
        GameManager.Instance.GameStart();
    }
}

public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= new GameManager();

    public Character player;

    private List<Item> shopItems;
    private List<Dungeon> dungeons;
    
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

        dungeons = new List<Dungeon>
        {
            // 던전 (이름, 최소 스폰량, 최대 스폰량, 몹 테이블)
            new Dungeon("초급 던전", 1, 4, new List<Monster>
            {
                // 몬스터 (이름, 레벨, 체력, 공격력)
                new Monster("슬라임", 2, 20, 5),
                new Monster("고블린", 3, 25, 7),
                new Monster("늑대", 4, 30, 8)
            }),
            new Dungeon("중급 던전", 3, 6, new List<Monster>
            {
                new Monster("골렘", 8, 50, 15),
                new Monster("오우거", 6, 45, 18),
                new Monster("자이언트", 10, 60, 20)
            }),
            new Dungeon("상급 던전", 5, 10, new List<Monster>
            {
                new Monster("다크엘프", 20, 100, 30),
                new Monster("암흑기사", 15, 120, 30),
                new Monster("타락한 마법사", 25, 80, 40)
            }),
            new Dungeon("보스 던전", 1, 1, new List<Monster>
            {
                new Monster("고대 드래곤", 20, 200, 60),
                new Monster("엘드리치", 15, 180, 75),
                new Monster("마왕", 25, 250, 50)
            })
        };
    }

    // 오프닝
    public void GameStart()
    {
        // Console.WriteLine("   _____ _____ __________                  ");
        // Console.WriteLine("  |__  // ___// ____/ __ \\____ ___  _______");
        // Console.WriteLine("   /_ </ __ \\/___ \\/ / / / __ `/ / / / ___/");
        // Console.WriteLine(" ___/ / /_/ /___/ / /_/ / /_/ / /_/ (__  ) ");
        // Console.WriteLine("/____/\\____/_____/_____/\\__\\_/\\__, /____/  ");
        // Console.WriteLine("                             /____/        \n");

        Console.WriteLine("   ____     __     ___       _             _  _         \r\n  |__ /    / /    | __|   __| |   __ _    | || |   ___  \r\n   |_ \\   / _ \\   |__ \\  / _` |  / _` |    \\_, |  (_-<  \r\n  |___/   \\___/   |___/  \\__,_|  \\__,_|   _|__/   /__/_ \r\n_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_| \"\"\"\"|_|\"\"\"\"\"|\r\n\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'");

        Console.WriteLine("\n스파르타 마을에 오신 것을 환영합니다.");
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
            Utils.ShowHeader("마을", "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            Utils.OptionText(1, "상태 보기");
            Utils.OptionText(2, "인벤토리");
            Utils.OptionText(3, "상점");
            Utils.OptionText(4, "던전");
            Utils.OptionText(5, "광산");
            Utils.OptionText(6, "휴식");

            switch (Utils.GetInput(1, 6))
            {
                case 1:
                    ShowStatus();
                    break;
                case 2:
                    ShowInventory();
                    break;
                case 3:
                    ShowShop();
                    break;
                case 4:
                    ShowDungeon();
                    break;
                case 5:
                    ShowMine();
                    break;
                case 6:
                    ShowRest();
                    break;
            }
        }
    }

    // 1. 상태 보기
    public void ShowStatus()
    {
        while (true)
        {
            Utils.ShowHeader("상태 보기", "캐릭터의 정보가 표시됩니다.");

            player.ShowStatus();

            switch (Utils.GetInput(0, 0))
            {
                case 0:
                    return;
            }
        }
    }

    // 2. 인벤토리
    public void ShowInventory()
    {
        while (true)
        {
            Utils.ShowHeader("인벤토리", "보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("[아이템 목록]");

            string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;

            if (player.Inventory.Count == 0)
            {
                Utils.InfoText("- 보유 중인 아이템이 없습니다.");
            }
            else
            {
                foreach (var item in player.Inventory)
                {
                    string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                    string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                    bonus = Utils.FormatString(bonus, 8 + longestBonus);

                    Console.Write("- ");

                    if (item.IsEquipped)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{equippedMarker}");
                    Console.ResetColor();

                    Console.WriteLine($"{name} | {bonus} | {item.Description}");
                }
            }

            Console.WriteLine();

            if (player.Inventory.Count > 0)
                Utils.OptionText(1, "장착 관리");

            Utils.OptionText(0, "나가기");

            switch (Utils.GetInput(0, 1))
            {
                case 1:
                    if (player.Inventory.Count > 0) ManageEquipment();
                    break;
                case 0:
                    return;
            }
        }
    }
    
    // 2-1. 인벤토리(장착 관리)
    public void ManageEquipment()
    {
        while (true)
        {
            Utils.ShowHeader("인벤토리 - 장착 관리", "보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("[아이템 목록]");

            string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                var item = player.Inventory[i];
                string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = Utils.FormatString(bonus, 8 + longestBonus);

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

            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, player.Inventory.Count);

            if (input >= 1 && input <= player.Inventory.Count)
            {
                var item = player.Inventory[input - 1];

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
            else if (input == 0)
                break;
        }
    }

    // 3. 상점
    public void ShowShop()
    {
        while (true)
        {
            Utils.ShowHeader("상점", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            string longestItem = shopItems.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = shopItems.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
            string longestDescription = shopItems.OrderByDescending(item => Utils.GetDisplayWidth(item.Description)).First().Description;

            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                string bonus = item.AttackBonus > 0 ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = Utils.FormatString(bonus, 8 + longestBonus);
                string description = Utils.FormatString($"{item.Description}", Utils.GetDisplayWidth(longestDescription));
                string price = item.IsPurchased ? "구매완료" : $"{item.Price} G";

                if (item.IsPurchased)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"- {name} | {bonus} | {description} | {price}");
                Console.ResetColor();
            }

            Console.WriteLine();

            Utils.OptionText(1, "아이템 구매");
            Utils.OptionText(2, "아이템 판매");
            Utils.OptionText(0, "나가기");

            switch (Utils.GetInput(0, 2))
            {
                case 1:
                    PurchaseItem();
                    break;
                case 2:
                    SellItem();
                    break;
                case 0:
                    return;
            }
        }
    }

    // 3-1. 상점(아이템 구매)
    public void PurchaseItem()
    {
        while (true)
        {
            Utils.ShowHeader("상점 - 아이템 구매", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            string longestItem = shopItems.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = shopItems.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
            string longestDescription = shopItems.OrderByDescending(item => Utils.GetDisplayWidth(item.Description)).First().Description;

            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];

                string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = Utils.FormatString(bonus, 8 + longestBonus);
                string description = Utils.FormatString($"{item.Description}", Utils.GetDisplayWidth(longestDescription));
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

            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, shopItems.Count);

            if (input >= 1 && input <= shopItems.Count)
            {
                var item = shopItems[input - 1];

                if (item.IsPurchased)
                    Utils.ErrorText("이미 구매한 아이템입니다.");
                else if (player.Gold >= item.Price)
                {
                    player.Gold -= item.Price;
                    item.IsPurchased = true;
                    player.Inventory.Add(item);
                    Console.WriteLine("구매를 완료했습니다.");
                }
                else
                    Utils.ErrorText("Gold 가 부족합니다.");
            }
            if (input == 0)
                break;
        }
    }

    // 3-2. 상점(아이템 판매)
    public void SellItem()
    {
        while (true)
        {
            Utils.ShowHeader("상점 - 아이템 판매", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            if (player.Inventory.Count == 0)
                Utils.InfoText("판매할 아이템이 없습니다.");
            else
            {
                string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
                int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
                string longestDescription = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Description)).First().Description;

                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    var item = player.Inventory[i];
                    int sellPrice = (int)(item.Price * 0.85);

                    string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                    bonus = Utils.FormatString(bonus, 8 + longestBonus);
                    string description = Utils.FormatString($"{item.Description}", Utils.GetDisplayWidth(longestDescription));
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

            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, player.Inventory.Count);

            if (input >= 1 && input <= player.Inventory.Count)
            {
                var item = player.Inventory[input - 1];

                if (!item.CanSell)
                    Utils.ErrorText("판매할 수 없는 아이템입니다.");
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

                        if (choice == "Y" || choice == "y")
                        {
                            player.Gold += (int)(item.Price * 0.85);
                            item.IsPurchased = false;
                            player.Inventory.Remove(item);
                            Console.WriteLine("판매를 완료했습니다.");
                        }
                        else if (choice == "N" || choice == "n")
                            break;
                        else
                            Utils.ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
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
            if (input == 0)
                break;
        }
    }

    // 4. 던전
    public void ShowDungeon()
    {
        Utils.ShowHeader("던전", "입장할 던전의 난이도를 선택할 수 있습니다.");

        Utils.OptionText(1, "초급 던전");
        Utils.OptionText(2, "중급 던전");
        Utils.OptionText(3, "상급 던전");
        Utils.OptionText(4, "보스 던전");
        Utils.OptionText(0, "나가기");

        int input = Utils.GetInput(0, 4);

        switch (input)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                dungeons[input - 1].Enter(player);
                break;
            case 0:
                return;
        }
    }

    // 5. 광산
    public void ShowMine()
    {
        while (true)
        {
            Utils.ShowHeader("광산", "이곳에서 골드를 획득할 수 있습니다.");

            Utils.OptionText(1, "기본 광산");
            Utils.OptionText(2, "요일 광산");
            Utils.OptionText(0, "나가기");

            switch (Utils.GetInput(0, 2))
            {
                case 1:
                    Utils.InfoText("'기본 광산'은 업데이트 예정입니다!");
                    break;
                case 2:
                    Utils.InfoText("'요일 광산'은 업데이트 예정입니다!");
                    break;
                case 0:
                    return;
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

            Utils.ShowHeader("휴식", "500 G 를 내면 체력을 회복할 수 있습니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Utils.OptionText(1, "휴식하기");
            Utils.OptionText(0, "나가기");

            switch (Utils.GetInput(0, 1))
            {
                case 1:
                    player.Rest();
                    break;
                case 0:
                    return;
            }
        }
    }
}
