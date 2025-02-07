using System;
using System.ComponentModel.Design;

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
        Console.WriteLine("전투를 승리하였습니다");
        Console.WriteLine("원하시는 보상을 선택하세요.");
        Console.WriteLine("1. 500 Gold\n2.회복 물약\n3. 랜덤상자");
        Console.ReadLine();


        switch (GameManager.Instance.GetInput(1, 3))
        {
            case 1:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("500 Gold를 획득하셨습니다.");
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("회복 물약을 획득하셨습니다.");
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("랜덤 상자를 획득하셨습니다.");
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                break;
        }
       /* for (int i = 0; i < monsters.Count; i++)
        {
            Console.WriteLine($"처치한 몬스터:{monsters[i]}");
        }*/
        foreach (Monster monster in monsters) 
        {
            Console.WriteLine($"처치한 몬스터:{monster.Name}");
        }
    }
    private static void Lose()
    {
        Console.WriteLine("전투에서 패배하셨습니다. 게임오버");
    }
}
