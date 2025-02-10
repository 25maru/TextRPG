using Tool;

public class Program
{
    static void Main(string[] args)
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.GameStart();
    }
}

public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ??= new GameManager();

    public Character player;

    public List<Item> shopItems;
    public List<Dungeon> dungeons;
    
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

        dungeons = new List<Dungeon>()
        {
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
        Console.WriteLine("   _____ _____ __________                  ");
        Console.WriteLine("  |__  // ___// ____/ __ \\___ ____  _______");
        Console.WriteLine("   /_ </ __ \\/___ \\/ / / / __` / / / / ___/");
        Console.WriteLine(" ___/ / /_/ /___/ / /_/ / /_/ / /_/ /__ \\  ");
        Console.WriteLine("/____/\\____/_____/_____/\\___,_\\__, /____/  ");
        Console.WriteLine("                             /____/        \n");

        // Console.WriteLine("   ____     __     ___       _             _  _         \r\n  |__ /    / /    | __|   __| |   __ _    | || |   ___  \r\n   |_ \\   / _ \\   |__ \\  / _` |  / _` |    \\_, |  (_-<  \r\n  |___/   \\___/   |___/  \\__,_|  \\__,_|   _|__/   /__/_ \r\n_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|_|\"\"\"\"\"|\r\n\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'\"`-0-0-'");

        Console.WriteLine("\n스파르타 마을에 오신 것을 환영합니다.");
        Thread.Sleep(1000);

        Console.WriteLine("당신의 이름을 알려주세요.");
        Thread.Sleep(1000);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(">> ");
        player.Name = Console.ReadLine();
        Console.ResetColor();

        GetStarterItem();
        // ShowMainMenu();
        SceneManager.Instance.mainScene.Open();
    }
    
    private void GetStarterItem()
    {
        // 초보자 아이템 획득
        player.Inventory.Add(new Item("장난감 칼", "한 번 휘두르면 부러질 것 같습니다.", ItemType.Weapon, attackBonus: 0f) { IsEquipped = true, CanSell = false });
        player.Inventory.Add(new Item("낡은 가죽 갑옷", "공격을 막기에는 너무 약합니다.", ItemType.Armor, defenseBonus: 0f) { IsEquipped = true, CanSell = false });
    }
}
