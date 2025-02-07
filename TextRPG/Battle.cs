using System;
using System.Runtime.Serialization.Formatters;

// 이상범님
public class Battle
{
    public List<Monster> monsters;
    public Character _character;

    GameManager GM = new GameManager();
    BattleManager BM = new BattleManager();

    public Battle(Character B_character, List<Monster> B_monsters)
    {
        _character = B_character;
        monsters = B_monsters;
    }

    public void StartBattle()
    {
        Console.WriteLine("BATTLE !!!");
        while ((!_character.IsDead) && (BM.isAliveMonstersExists)) // 몬스터 사망처리 우선구현
        {
            BM.PlayerTurn();
            for (int i = 0; i <= BM.CountAliveMonsters(monsters); i++) //죽은 몬스터 제외처리 필요
            {
                BM.MonsterTurn(monsters[i], _character);
            }
        }
        BM.ClearDungeon(); // 둘중 하나 죽어도 일단 클리어로
    }
}
public class BattleManager //배틀 유틸리티
{
    public void InstantiateMonster(List<Monster> _monsters) //몬스터 생성 //위에꺼 쓰고 이건 안쓸듯 ..?
    {
        Console.WriteLine($"The Count Of Monsters ; {_monsters.Count}");
        for (int i = 0; _monsters.Count >= i; i++)
        {
            Monster _monster = _monsters[i];
        }
    }

    public bool isAliveMonstersExists(List<Monster> _monsters) /// 살아있는 몬스터 존재여부
    {
        Console.WriteLine($"살아있는 몬스터가 {CountAliveMonsters(_monsters)} 마리 있음.");

        return _monsters.Exists(_monsters => !_monsters.IsDead);
    }

    public int CountAliveMonsters(List<Monster> _monsters) // 살아있는 몬스터 개체 수
    {
        int AllMonstersCounts = _monsters.Count();
        int Alives = _monsters.Count(_monsters => !_monsters.IsDead);
        int DeadBodies = _monsters.Count(_monsters => _monsters.IsDead);

        Console.WriteLine($"{Alives} 만큼 살아있음");

        return Alives;
    }

    public void PlayerTurn() //플레이어의 턴
    {
        Console.WriteLine("플레이어 턴 메소드 입니다.");

        Console.WriteLine("플레이어 턴 메소드 입니다.");
        Console.WriteLine("플레이어 턴 메소드 입니다.");
        Console.WriteLine("플레이어 턴 메소드 입니다.");

        Console.WriteLine("행동을 선택해 주세요!");

        Console.Write(">>>");

        string KeyCode = Console.ReadLine();
        switch (KeyCode)
        {
            case "1":
                Console.WriteLine("공격을 가합니다.");

                //int _monsterNum = int.Parse(Console.ReadLine()); //남은 몬스터까지만 번호 보여주기 필요
                //if(_monsterNum >= )

                break;

            case "2":
                Console.WriteLine("회복 포션을 사용합니다.");
                break;

            case "3":
                Console.WriteLine("강화 포션을 사용합니다.");
                break;

            default:
                Console.WriteLine("올바르지 않은 선택입니다.");
                PlayerTurn();
                break;
        }
    }

    public void MonsterTurn(Monster _TurnMonster, Character _character) //몬스터의 턴
    {
        Console.WriteLine("MonsterTurnMethod");
        MonsterAttack(_TurnMonster, _character);
    }

    public void PlayerAttack(Character _character, Monster AMonster, int Damage) //플레이어의 공격
    {
        Console.WriteLine($"AttackMethod : {Damage} Damage Has Been Attacked !!!");
        Console.WriteLine($"{AMonster} ");
        AMonster.Hitted(_character.Name, Damage);
    }
    public void MonsterAttack(Monster _monster, Character _character) //몬스터의 공격 
    {
        _monster.Hitted(_character.Name, (int)_character.TotalAttack); // TotalAttack은 float  Damage는 int로 되어있어 변환 시킵니다.
        _character.Health -= _monster.Attacking(_character.Name);
        if (_character.Health <= 0)
        {
            _character.IsDead = true;
            FailedDungeon();
            Console.WriteLine($"{_character.Name}가 죽었습니다. 마을로 돌아갑니다.");
            //ExitDungeon();
        }
    }

    public void FailedDungeon()
    {

    }

    public void ClearDungeon() ///던전 종료 메서드 //보윤님 코드 우선
    {
        Console.WriteLine("던전을 클리어 하였습니다.");
        Console.WriteLine("원하시는 보상을 선택하세요.");
        Console.WriteLine("1. 500 Gold\n2. 물약\n3. 랜덤상자");

        Console.ReadLine();
        //GiveReward(); //종료시 보상
    }

    /*
    public void GiveReward() //던전 종료 후 보상 메서드
    {
        Console.WriteLine("");
    }

    public void ExitDungeon() //마을로 돌아가기
    {
        Console.WriteLine("ExitDungeonMethod");
        //마을로 돌아가기 추가 예정
    }
    */
}
