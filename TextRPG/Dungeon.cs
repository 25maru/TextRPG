using Tool;

// 박지원님
public class Dungeon
{
    public string Name { get; }
    public int Time { get; set; }
    public int MinMonsterCount { get; }
    public int MaxMonsterCount { get; }
    public List<Monster> Monsters { get; set; }

    private Battle battle;

    public Dungeon(string name, int min, int max, List<Monster> monsters)
    {
        Name = name;
        MinMonsterCount = min;
        MaxMonsterCount = max;
        Monsters = monsters ?? new List<Monster>();
    }

    // TODO: 같은 몬스터의 경우 데미지를 같이 받는 문제 수정
    // 체력, 골드 반영 안되는 문제
    // UI 디자인 최적화

    public void Enter(Character player)
    {
        Utils.ShowHeader("", "");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"\n{Name}");
        Console.ResetColor();

        Console.WriteLine("에 입장합니다.\n");

        Random rand = new Random();
        int monsterCount = rand.Next(MinMonsterCount, MaxMonsterCount + 1);
        List<Monster> selectedMonsters = new List<Monster>();

        for (int i = 0; i < monsterCount; i++)
        {
            selectedMonsters.Add(Monsters[rand.Next(Monsters.Count)]);
        }

        Console.WriteLine("출현한 몬스터:");
        foreach (var monster in selectedMonsters)
        {
            Console.WriteLine($"- {monster.Name} (공격력: {monster.Attack}, 체력: {monster.Health})");
        }

        Console.WriteLine();

        battle = new Battle(player, selectedMonsters, new List<Item>());
        battle.StartBattle();
    }

    public async Task SetTimerAsync(int startTime)
    {
        Time = Math.Max(startTime, 0);

        while (Time > 0)
        {
            Time--;

            TimeSpan timeSpan = TimeSpan.FromSeconds(Time);
            string formattedTime = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";

            Console.Write($"\r남은 시간: {formattedTime}");

            await Task.Delay(1000);
        }

        Console.WriteLine("\n타임 오버");
    }
}
