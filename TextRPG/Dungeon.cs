using Tool;

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
        Utils.ShowHeader("던전 내부", $"{Name}에 입장했습니다.");

        Random rand = new Random();
        int monsterCount = rand.Next(MinMonsterCount, MaxMonsterCount + 1);
        List<Monster> selectedMonsters = new List<Monster>();

        battle = new Battle(player, selectedMonsters, new List<Item>());

        for (int i = 0; i < monsterCount; i++)
        {
            Monster original = Monsters[rand.Next(Monsters.Count)];
            Monster clonedMonster = new Monster(original.Name, original.Level, original.Health, original.Attack); // 개별 객체 복사
            selectedMonsters.Add(clonedMonster);
        }

        Console.WriteLine("[몬스터 정보]");
        foreach (var monster in selectedMonsters)
        {
            Utils.MonsterText(monster.Level, monster.Name, monster.Attack, monster.Health, monster.IsDead);
        }

        Console.WriteLine();

        Console.WriteLine("[내 정보]");
        Console.Write("Lv.");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(player.Level);
        Console.ResetColor();

        Console.Write($"  {player.Name} ");

        string playerClass = Utils.FormatString($"({player.Class})", 13 - Utils.GetDisplayWidth(player.Name));

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(playerClass);
        Console.ResetColor();

        Console.Write($"(공격력 ");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(player.TotalAttack);
        Console.ResetColor();

        Console.Write($" / 체력 ");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(player.Health);
        Console.ResetColor();

        Console.WriteLine(")");

        Console.WriteLine();

        Utils.OptionText(1, "전투 시작");
        Utils.OptionText(0, "포기하기");

        switch (Utils.GetInput(0, 1))
        {
            case 1:
                break;
            case 0:
                SceneManager.Instance.mainScene.Open();
                break;
        }

        battle = new Battle(player, selectedMonsters, new List<Item>());
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
