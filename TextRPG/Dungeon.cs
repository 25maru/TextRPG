using Tool;

public enum Reward
{
    Tier1 = 1,
    Tier2,
    Tier3,
    Tier4
}

// 박지원님
public class Dungeon
{
    public string Name { get; }
    public int Time { get; set; }
    public int MinMonsterCount { get; }
    public int MaxMonsterCount { get; }
    public Reward RewardTier { get; }
    public List<Monster> Monsters { get; set; }

    public Dungeon(string name, int min, int max, Reward reward, List<Monster> monsters)
    {
        Name = name;
        MinMonsterCount = min;
        MaxMonsterCount = max;
        RewardTier = reward;
        Monsters = monsters;
    }

    private Battle? battle;

    public void Enter(Character player)
    {
        Utils.ShowHeader("던전 내부", $"{Name}에 입장했습니다.");

        Console.WriteLine("[내 정보]");
        Utils.PlayerText(player);

        Console.WriteLine();

        Random rand = new Random();
        int monsterCount = rand.Next(MinMonsterCount, MaxMonsterCount + 1);
        List<Monster> selectedMonsters = new List<Monster>();

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

        Utils.OptionText(1, "전투 시작");
        Utils.OptionText(0, "포기하기");

        switch (Utils.GetInput(0, 1))
        {
            case 1:
                break;
            case 0:
                SceneManager.Instance.dungeonScene.Open();
                break;
        }

        battle = new Battle(player, selectedMonsters, new List<Item>(), RewardTier);
        battle.StartBattle();
    }

    // 아직 사용처 없음
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
