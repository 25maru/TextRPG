using System;

// 박지원님
public class Dungeon
{
    public string Name { get; }
    public int Time { get; set; }
    public int MinMonsterCount { get; }
    public int MaxMonsterCount { get; }
    public List<Monster> Monsters { get; set; }

    public Dungeon(string name, int min, int max, List<Monster> monsters)
    {
        Name = name;
        MinMonsterCount = min;
        MaxMonsterCount = max;
        Monsters = monsters;
    }

    private Battle battle;

    public void Enter()
    {
        // 배틀 스크립트도 추후에 싱글톤 패턴으로 관리하는게 좋을듯
        battle = new Battle();

        GameManager.Instance.ShowHeader("", "");

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

        battle.StartBattle();
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
