using Tool;

// 최보윤님
public class BattleEndUI
{
    /// <summary>
    /// 전투 결과를 받아서 결과 화면에 반영하는 메서드로 이동
    /// </summary>
    /// <param name="isWin">승리 여부</param>
    /// <param name="monsters">처치한 몬스터</param>
    public static void BattleEnd(bool isWin, Character player, List<Monster> monsters, Reward reward)
    {
        player.IsBattle = false;
        player.ResetPotionBuffs();

        if (isWin) GiveReward(player, monsters, reward);
        else Lose(player);
    }

    /// <summary>
    /// 전투 승리 시 보상 지급
    /// </summary>
    /// <param name="monsters"></param>
    private static void GiveReward(Character player, List<Monster> monsters, Reward reward)
    {
        Utils.ShowHeader("전투 결과", "");
        Utils.ShowHeader("승리!!", "원하는 보상을 선택해서 수령할 수 있습니다.", ConsoleColor.DarkGreen);

        int gold = reward switch
        {
            Reward.Tier1 => 500,
            Reward.Tier2 => 2500,
            Reward.Tier3 => 12500,
            Reward.Tier4 => 1000000,
            _ => 0
        };

        Item potion = reward switch
        {
            Reward.Tier1 => new Item("하급 회복 포션", "", ItemType.Potion, healthBonus: 25f, price: 100),
            Reward.Tier2 => new Item("상급 회복 포션", "", ItemType.Potion, healthBonus: 50f, price: 250),
            Reward.Tier3 => new Item("최상급 회복 포션", "", ItemType.Potion, healthBonus: 150f, price: 1000),
            Reward.Tier4 => new Item("초월 회복 포션", "", ItemType.Potion, healthBonus: 10000f, price: 10000),
            _ => new Item("오류 아이템", "", ItemType.Potion)
        };

        Utils.OptionText(1, $"{gold} G");
        Utils.OptionText(2, $"{potion.Name} x2");
        Utils.OptionText(3, "랜덤박스");

        switch (Utils.GetInput(1, 3))
        {
            case 1:
                player.Gold += gold;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{gold} G 를 획득하셨습니다.");
                Console.ResetColor();
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("회복 물약을 획득하셨습니다.");
                player.Inventory.Add(potion);
                player.Inventory.Add(potion);
                Console.ResetColor();
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("랜덤박스를 획득하셨습니다.");
                player.Inventory.Add(new Item($"랜덤박스 (T{(int)reward})", "무엇이 들어있을지 알 수 없습니다.", ItemType.Potion) { CanSell = false });
                Console.ResetColor();
                break;
        }

        foreach (Monster monster in monsters) 
        {
            Utils.InfoText($"처치한 몬스터:{monster.Name}");
        }
    }
    private static void Lose(Character player)
    {
        Utils.ShowHeader("전투 결과", "");
        Utils.ShowHeader("패배", "더욱 강해진 후 도전해주세요", ConsoleColor.DarkRed);

        Utils.PlayerText(player);
    }
}
