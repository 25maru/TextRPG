using System;

// 이상범님
public class Battle
{
    //이상범입니다.
    //이상범입니다. 010
    public List<Monster> monsters = new List<Monster>();
    GameManager GM = new GameManager();

    public void InstantiateMonster()
    {
        monsters.Add(new Monster());
        monsters.Add(new Monster());
        monsters.Add(new Monster());

    }
    public void StartBattle()
    {
        GM.ShowHeader("","");
        Console.WriteLine("BATTLE !!!");
    }

    public void PlayerTurn()
    {
        Console.WriteLine("플레이어의 턴입니다.");
    }

    public void MonsterTurn()
    {

    }

    public void GetDamage()
    {

    }

    public void ClearDungeon()
    {
        Console.WriteLine("던전을 클리어 하였습니다.");
        Console.WriteLine("던전을 클리어 하였습니다.");
        Console.Write("원하시는 보상을 선택하세요.");
    }

    public void ExitDungeon()
    {

    }
}
