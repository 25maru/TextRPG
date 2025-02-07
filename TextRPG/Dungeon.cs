using System;

// 박지원님
public class Dungeon
{
    public string Name { get; }
    public int Time { get; set; }
    public List<Monster> Monsters { get; set; }

    private static List<Dungeon> dungeons = new List<Dungeon>
    {
        new Dungeon("초급 던전", new List<Monster>
        {
            // 몬스터 (이름, 레벨, 체력, 공격력)
            new Monster("슬라임", 2, 20, 5),
            new Monster("고블린", 3, 25, 7),
            new Monster("늑대", 4, 30, 8)
        }),
        new Dungeon("중급 던전", new List<Monster>
        {
            // 몬스터 (이름, 레벨, 체력, 공격력)
            new Monster("오우거", 8, 50, 15),
            new Monster("다크엘프", 6, 45, 18),
            new Monster("골렘", 10, 60, 20)
        }),
        new Dungeon("상급 던전", new List<Monster>
        {
            // 몬스터 (이름, 레벨, 체력, 공격력)
            new Monster("드래곤", 20, 100, 40),
            new Monster("리치", 15, 80, 35),
            new Monster("마왕", 25, 120, 50)
        })
    };

    public Dungeon(string name, List<Monster> monsters)
    {
        Name = name;
        Monsters = monsters;
    }

    public static void SelectDungeon()
    {
        Console.WriteLine("\n던전을 선택하세요:");
        for (int i = 0; i < dungeons.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {dungeons[i].Name}");
        }
        Console.WriteLine("0. 나가기\n");
        Console.Write("원하시는 행동을 입력해주세요. >> ");

        string input = Console.ReadLine();
        if (int.TryParse(input, out int dungeonIndex) && dungeonIndex >= 1 && dungeonIndex <= dungeons.Count)
        {
            dungeons[dungeonIndex - 1].Enter();
        }
        else if (input == "0")
        {
            return;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.\n");
        }
    }

    public void Enter()
    {
        Console.WriteLine($"\n{Name} 던전에 입장합니다.\n");

        Random rand = new Random();
        int monsterCount = rand.Next(1, 5);
        List<Monster> selectedMonsters = new List<Monster>();

        for (int i = 0; i < monsterCount; i++)
        {
            selectedMonsters.Add(Monsters[rand.Next(Monsters.Count)]);
        }

        Console.WriteLine("출현한 몬스터:");
        foreach (var monster in selectedMonsters)
        {
            Console.WriteLine($"- {monster.Name} (공격력: {monster.Attack}, 방어력: {monster.Defense}, 체력: {monster.Health})");
        }
        Console.WriteLine();
    }

    public void SetTimer(int startTime)
    {
        Time = startTime;

        while (Time >= 0)
        {
            Time--;

            if (Time <= 0)
                Time = 0;

            TimeSpan timeSpan = TimeSpan.FromSeconds(Time);
            string formattedTime = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

            Console.Write($"\r남은 시간: {formattedTime}");

            Thread.Sleep(1000);
        }
    }
}
