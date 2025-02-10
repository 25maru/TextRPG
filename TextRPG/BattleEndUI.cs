using Tool;

// 최보윤님
public class BattleEndUI
{
    /// <summary>
    /// 전투에 붙여주세요.
    /// </summary>
    public static void BattleEnd(bool isWin, List<Monster> monsters)
    {
        if (isWin) GiveReward(monsters);
        else Lose();
    }

    private static void GiveReward(List<Monster> monsters)
    {
       Utils.ShowHeader("승리하였습니다!", "원하는 보상을 선택해서 수령할 수 있습니다.");

        Utils.OptionText(1, "500 G");
        Utils.OptionText(2, "회복 물약 x2");
        Utils.OptionText(3, "랜덤박스");;

        switch (Utils.GetInput(1, 3))
        {
            case 1:
                GameManager.Instance.player.Gold += 500;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("500 Gold를 획득하셨습니다.");
                Console.ResetColor();
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("회복 물약을 획득하셨습니다.");
                Console.ResetColor();
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("랜덤 상자를 획득하셨습니다.");
                Console.ResetColor();
                break;
        }

        foreach (Monster monster in monsters) 
        {
            Utils.InfoText($"처치한 몬스터:{monster.Name}");
        }
    }
    private static void Lose()
    {
        Console.WriteLine("전투에서 패배하셨습니다. 게임오버");
    }
}
