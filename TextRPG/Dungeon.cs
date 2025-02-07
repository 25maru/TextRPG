using System;
using System.Collections.Generic;

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

    // TODO: 같은 몬스터의 경우 데미지를 같이 받는 문제 수정
    // 체력, 골드 반영 안되는 문제
    // UI 디자인 최적화

    private Battle battle;

    public void Enter(Character player)
    {
        GameManager.Instance.ShowHeader("", "");

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"\n{Name}");
        Console.ResetColor();

        Console.WriteLine("에 입장합니다.\n");

        Random rand = new Random();
        int monsterCount = rand.Next(MinMonsterCount, MaxMonsterCount + 1);
        List<Monster> selectedMonsters = new List<Monster>();

        battle = new Battle(player, selectedMonsters, new List<Item>());

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
