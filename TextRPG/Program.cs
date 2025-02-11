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
        player = new Character("플레이어", "전사", 1, 10f, 100f, 1500000000);

        // 아이템 (이름, 설명, 타입, 보너스: 값, 가격: 값)
        shopItems = new List<Item>()
        {
            new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", ItemType.Weapon, attackBonus: 2f, price: 500),
            new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", ItemType.Armor, healthBonus: 5f, price: 750),
            new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", ItemType.Weapon, attackBonus: 15f, price: 1500),
            new Item("청동 거울", "공격을 막을 수 있을까요?", ItemType.Shield, healthBonus: 7f, price: 2000),
            new Item("청동 갑옷", "청동으로 만들어졌지만 생각보다 단단합니다.", ItemType.Armor, healthBonus: 10f, price: 2000),
            new Item("강철 대검", "강력한 공격을 날릴 수 있습니다.", ItemType.Armor, attackBonus: 25f, price: 3300),
            new Item("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", ItemType.Armor, healthBonus: 50f, price: 3300),
            new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", ItemType.Weapon, attackBonus: 75f, price: 10000),
            new Item("스파르타의 방패", "스파르타의 전사들이 사용했다는 전설의 방패입니다.", ItemType.Shield, healthBonus: 100f, price: 10000),
            new Item("스파르타의 투구", "스파르타의 전사들이 사용했다는 전설의 투구입니다.", ItemType.Helmet, healthBonus: 100f, price: 10000),
            new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", ItemType.Armor, healthBonus: 100f, price: 10000),
            new Item("아케인셰이드 투핸드소드", "스타포스 22성 레전드리 무기입니다.", ItemType.Weapon, attackBonus: 1223f, price: 50000000),
            new Item("하급 회복 포션", "", ItemType.Potion, healthBonus: 25f, price: 100),
            new Item("하급 강화 포션", "", ItemType.Potion, attackBonus: 10f, price: 100),
            new Item("상급 회복 포션", "", ItemType.Potion, healthBonus: 50f, price: 250),
            new Item("상급 강화 포션", "", ItemType.Potion, attackBonus: 20f, price: 250),
            new Item("최상급 회복 포션", "", ItemType.Potion, healthBonus: 150f, price: 1000),
            new Item("최상급 강화 포션", "", ItemType.Potion, attackBonus: 50f, price: 1000),
        };

        dungeons = new List<Dungeon>()
        {
            new Dungeon("초급 던전", 1, 4, Reward.Tier1, new List<Monster>
            {
                // 몬스터 (이름, 레벨, 체력, 공격력)
                new Monster("슬라임", 2, 20, 5),
                new Monster("고블린", 3, 25, 7),
                new Monster("늑대", 4, 30, 8)
            }),
            new Dungeon("중급 던전", 5, 6, Reward.Tier2, new List<Monster>
            {
                new Monster("골렘", 8, 45, 15),
                new Monster("오우거", 6, 50, 10),
                new Monster("자이언트", 10, 60, 12)
            }),
            new Dungeon("상급 던전", 5, 10, Reward.Tier3, new List<Monster>
            {
                new Monster("다크엘프", 20, 100, 35),
                new Monster("암흑기사", 15, 120, 30),
                new Monster("타락한 마법사", 25, 80, 40)
            }),
            new Dungeon("보스 던전", 1, 1, Reward.Tier4, new List<Monster>
            {
                new Monster("고대 드래곤", 100, 2000, 80),
                new Monster("엘드리치", 100, 1800, 90),
                new Monster("마왕", 100, 2500, 75)
            })
        };
    }


    /// <summary>
    /// 게임 시작 시 호출되는 메서드
    /// </summary>
    public void GameStart()
    {
        // Console.WriteLine("   _____ _____ __________                  ");
        // Console.WriteLine("  |__  // ___// ____/ __ \\___ ____  _______");
        // Console.WriteLine("   /_ </ __ \\/___ \\/ / / / __` / / / / ___/");
        // Console.WriteLine(" ___/ / /_/ /___/ / /_/ / /_/ / /_/ /__ \\  ");
        // Console.WriteLine("/____/\\____/_____/_____/\\___,_\\__, /____/  ");
        // Console.WriteLine("                             /____/        \n");

        // 로고 애니메이션 재생
        StartAnim.Play();

        Console.WriteLine("\n스파르타 마을에 오신 것을 환영합니다.");
        Thread.Sleep(1000);

        Console.WriteLine("당신의 이름을 알려주세요.");
        Thread.Sleep(1000);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(">> ");
        player.Name = Console.ReadLine();
        Console.ResetColor();

        GetStarterItem();

        // 씬 매니저에 등록된 메인 씬의 오픈 메서드 호출
        SceneManager.Instance.mainScene.Open();
    }
    
    /// <summary>
    /// 플레이어의 인벤토리에 기본 아이템 추가
    /// </summary>
    private void GetStarterItem()
    {
        // 시작 아이템 획득
        player.Inventory.Add(new Item("장난감 칼", "한 번 휘두르면 부러질 것 같습니다.", ItemType.Weapon, attackBonus: 0f) { IsEquipped = true, CanSell = false });
        player.Inventory.Add(new Item("낡은 가죽 갑옷", "공격을 막기에는 너무 약합니다.", ItemType.Armor, healthBonus: 0f) { IsEquipped = true, CanSell = false });
        player.Inventory.Add(new Item("하급 강화 포션", "", ItemType.Potion, attackBonus: 10f, price: 100));
        player.Inventory.Add(new Item("하급 회복 포션", "", ItemType.Potion, healthBonus: 25f, price: 100));
    }
}
