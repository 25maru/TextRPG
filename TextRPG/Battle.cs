using System;
using System.Runtime.Serialization.Formatters;

// 이상범님
public class Battle
{
    public List<Monster> monsters = new List<Monster>();
    public Character _character;

    GameManager GM = new GameManager();
    BattleManager BM = new BattleManager();

    public void StartBattle()
    {
        Console.WriteLine("BATTLE !!!");
        while ((_character.IsDead != false) && (monsters.IsDead != false))
        {
            BM.PlayerTurn();
            for (int i = 0; i <= monsters.Count; i++)
            {
                BM.MonsterTurn(monsters[i]);
            }
            BM.ClearDungeon();
        }
    }
}
public class BattleManager
{
    public void InstantiateMonster(List<Monster> _monsters)
    {
        Console.WriteLine($"The Count Of Monsters ; {_monsters.Count}");
    }
    public void PlayerTurn() //플레이어의 턴
    {
        Console.WriteLine("PlayerTurn Method");
        Console.WriteLine("Choice Your Behaviors !!!");
        Console.Write( ">>>");
        string KeyCode = Console.ReadLine();
        switch(KeyCode)
        {
            case "1":
                Console.WriteLine("Attack!!!");

                //int _monsterNum = int.Parse(Console.ReadLine());
                //if(_monsterNum >= )

                break;

            case "2":
                Console.WriteLine("Use The Healing Potion");
                break;

            case "3":
                Console.WriteLine("Use The Strengthen Position");
                break;

            default:
                Console.WriteLine("Wrong Input.");
                PlayerTurn();
                break;
        }
    }

    public void MonsterTurn(Monster _TurnMonster) //몬스터의 턴
    {
        Console.WriteLine("MonsterTurnMethod");
        MonsterAttack(_TurnMonster, 10);
    }

    public void PlayerAttack(Character _character, Monster AMonster, int Damage) //플레이어의 공격
    {
        Console.WriteLine($"AttackMethod : {Damage} Damage Has Been Attacked !!!");
        Console.WriteLine($"{AMonster} ");
        /*
        AMonster.Health -= Damage;
        if(Health <=0)
        {
            AMonster.IsDead = true;
        }
        */
    }
    public void MonsterAttack(Monster _monster, ,Character _character, int Damage) //몬스터의 공격 
    {
        _character.Health -= Damage;
        if( _character.Health <= 0 )
        {
            _character.IsDead = true;
        }
    }
    public void GetDamage(int Damage) //데미지 입히기
    {
        Console.WriteLine($"GetDamageMethod : {Damage} Damage Has Been Attacked!");
    }

    public void ClearDungeon() //던전 종료 메서드
    {
        Console.WriteLine("던전을 클리어 하였습니다.");
        Console.WriteLine("원하시는 보상을 선택하세요.");
        Console.WriteLine("1. 500 Gold\n2. 물약\n3. 랜덤상자");

        ExitDungeon();
    }

    public void ExitDungeon() //마을로 돌아가기
    {
        Console.WriteLine("ExitDungeonMethod");
        //마을로 돌아가기 추가 예정
    }
}
