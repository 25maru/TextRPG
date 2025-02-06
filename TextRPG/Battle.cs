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
        while ((_character.IsDead != false) && (monsters.IsDead != false)) //Private라서 자체처리로 바꿀 예정
        {
            BM.PlayerTurn();
            for (int i = 0; i <= monsters.Count; i++) //죽은 몬스터 제외처리 필요
            {
                BM.MonsterTurn(monsters[i], _character);
            }
            BM.ClearDungeon(); //한 턴 끝나고 던전 클리어 작동 테스트
            BM.ExitDungeon();
        }
        //BM.ClearDungeon(); //한 턴 끝나고 던전 클리어 작동 테스트
        //BM.ExitDungeon();
    }
}
public class BattleManager //배틀 유틸리티
{
    public void InstantiateMonster(List<Monster> _monsters) //몬스터 생성
    {
        Console.WriteLine($"The Count Of Monsters ; {_monsters.Count}");
    }
    public void PlayerTurn() //플레이어의 턴
    {
        Console.WriteLine("PlayerTurn Method");
        Console.WriteLine("Choice Your Behaviors !!!");
        Console.Write(">>>");
        string KeyCode = Console.ReadLine();
        switch (KeyCode)
        {
            case "1":
                Console.WriteLine("Attack!!!");

                //int _monsterNum = int.Parse(Console.ReadLine()); //남은 몬스터까지만 번호 보여주기 필요
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

    public void MonsterTurn(Monster _TurnMonster, Character _character) //몬스터의 턴
    {
        Console.WriteLine("MonsterTurnMethod");
        MonsterAttack(_TurnMonster, _character, _TurnMonster.Attack); //이것도 Private

    }

    public void PlayerAttack(Character _character, Monster AMonster, int Damage) //플레이어의 공격
    {
        Console.WriteLine($"AttackMethod : {Damage} Damage Has Been Attacked !!!");
        Console.WriteLine($"{AMonster} ");
        AMonster.Hitted(_character.Name, Damage);
    }
    public void MonsterAttack(Monster _monster, Character _character) //몬스터의 공격 
    {
        _character.Health -= _monster.Attacking(_character.Name);
        if (_character.Health <= 0)
        {
            _character.IsDead = true;
            Console.WriteLine($"{_character.Name}가 죽었습니다. 마을로 돌아갑니다.");
            ExitDungeon();
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

        Console.ReadLine();
        GiveReward(); //종료시 보상
    }

    public void GiveReward() //던전 종료 후 보상 메서드
    {
        Console.WriteLine("");
    }

    public void ExitDungeon() //마을로 돌아가기
    {
        Console.WriteLine("ExitDungeonMethod");
        //마을로 돌아가기 추가 예정
    }
}
