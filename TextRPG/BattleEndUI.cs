using System;
using System.ComponentModel.Design;

// 최보윤님
public class BattleEndUI
{
    /// <summary>
    /// 전투에 붙여주세요.
    /// </summary>
    /// <param name="isWin"></param>
    /// <param name="monsters"></param>
    public static void BattleEnd(bool isWin, List<Monster> monsters)
    {
        if (isWin) GiveReward(monsters);
        else Lose();
    }
    private static void GiveReward(List<Monster> monsters)
    {
        Console.WriteLine("전투를 승리하였습니다");

        for (int i = 0; i < monsters.Count; i++)
        {
            Console.WriteLine($"처치한 몬스터:{monsters[i]}");
        }
    }
    private static void Lose()
    {
        Console.WriteLine("전투에서 패배하셨습니다. 게임오버");
    }
}
