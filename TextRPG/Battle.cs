using System;

// 이상범님
public class Battle
{
    public List<Monster> monsters = new List<Monster>();
    GameManager GM = new GameManager();

    public Battle()
    {

    }
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
        /*
        while ()
        {
            PlayerTurn();
            MonsterTurn();
        }
        */
    }

    public void PlayerTurn()
    {
        Console.WriteLine("PlayerTurn Method");
        
    }

    public void MonsterTurn()
    {
        Console.WriteLine("MonsterTurnMethod");
    }

    public void Attack()
    {
        Console.WriteLine("AttackMethod");
    }

    public void GetDamage()
    {
        Console.WriteLine("GetDamageMethod");
    }

    public void ClearDungeon()
    {
        Console.WriteLine("던전을 클리어 하였습니다.");
        Console.WriteLine("던전을 클리어 하였습니다.");
        Console.Write("원하시는 보상을 선택하세요.");
    }

    public void ExitDungeon()
    {
        Console.WriteLine("ExitDungeonMethod");
    }
}
