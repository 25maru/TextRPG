using System;
using System.Runtime.Serialization.Formatters;

// 이상범님
public class Battle
{
    public List<Monster> _monsters;
    public Character _character;
    public List<Item> _rewarditem;

    GameManager GM = GameManager.Instance;

    /// <summary>
    /// 캐릭터, 몬스터 리스트, 리워드 리스트
    /// </summary>
    public Battle(Character B_character, List<Monster> B_monsters, List<Item> B_rewarditem)
    {
        _character = B_character;
        _monsters = B_monsters;
        _rewarditem = B_rewarditem;
    }

    public void StartBattle()
    {
        GM.ShowHeader("전투 시작 !!!", "몬스터와의 전투를 시작합니다.");

        while ((!_character.IsDead) && (isAliveMonstersExists(_monsters))) // 몬스터 사망처리
        { //생각해보니 살아있는 만큼이 아니라 살아있는애만 ...
            PlayerTurn();
            for (int i = 0; i < _monsters.Count(); i++) //살아있는 몬스터 만큼 턴을 씀 
            {
                if (_monsters[i].IsDead) //번호 몬스터가 죽어있으면
                {
                    //그냥 넘어가기
                }
                else //살아있으면
                {
                    MonsterTurn(_monsters[i], _character); 
                }
            }
        }
        ClearDungeon(); // 둘중 하나 죽어도 일단 클리어로
    }
    public void InstantiateMonster(List<Monster> _monsters) //몬스터 생성 //위에꺼 쓰고 이건 안쓸듯 ..?
    {
        Console.WriteLine($"The Count Of Monsters ; {_monsters.Count}");
        for (int i = 0; _monsters.Count >= i; i++)
        {
            Monster _monster = _monsters[i];
        }
    }

    public bool isAliveMonstersExists(List<Monster> _monsters) // 살아있는 몬스터 존재여부
    {

        Console.WriteLine($"살아있는 몬스터가 {CountAliveMonsters(_monsters)} 마리 있음.");

        return _monsters.Exists(_monsters => !_monsters.IsDead);
    }

    public int CountAliveMonsters(List<Monster> _monsters) // 살아있는 몬스터 개체 수
    {
        int AllMonstersCounts = _monsters.Count();
        int Alives = _monsters.Count(_monsters => !_monsters.IsDead);
        int DeadBodies = _monsters.Count(_monsters => _monsters.IsDead);

        return Alives;
    }

    public void PlayerTurn() //플레이어의 턴
    {

        foreach (var monster in _monsters)
        {

            //Console.WriteLine("플레이어 턴 메소드 입니다.");
        }
        for (int i = 0; i < CountAliveMonsters(_monsters); i++) //몬스터 현재 상태 보여주기
                {
                    _monsters[i].ShowMonster(i+1);
                }
        Console.WriteLine();

        GM.OptionText(1, "공격");
        GM.OptionText(2, "회복 포션 사용");
        GM.OptionText(3, "공격 포션 사용");

        int KeyCode = GM.GetInput(1, 3);
        switch (KeyCode)
        {
            case 1:
                Console.WriteLine("=================================");
                Console.WriteLine("공격을 가합니다.\n");

                Console.WriteLine("=================================");
                Console.WriteLine("어떤 몬스터에게 공격을 가합니까?");

                for (int i = 0; i < CountAliveMonsters(_monsters); i++) //몬스터 현재 상태 보여주기
                {
                    Console.Write(i+1);
                    _monsters[i].ShowMonster(i + 1);
                }
                int _TargetMonster = GM.GetInput(0, _monsters.Count+1);
                PlayerAttack(_character, _monsters[_TargetMonster - 1], (int)_character.TotalAttack);

                //_monsters[_TargetMonster - 1].Hitted(_character.Name,(int)_character.TotalAttack);

                //int _monsterNum = int.Parse(Console.ReadLine()); //남은 몬스터까지만 번호 보여주기 필요
                //if(_monsterNum >= )

                break;

            case 2:
                Console.WriteLine("회복 포션을 사용합니다.");
                ClearDungeon();
                break;


            case 3:
                Console.WriteLine("강화 포션을 사용합니다.");
                _character.Health = 0;
                break;

            case 4:
                Console.WriteLine("4번 항목입니다.");
                break;
        }
    }

    public void MonsterTurn(Monster _TurnMonster, Character _character) //몬스터의 턴
    {
        if (_character.IsDead == false) //플레이어가 살아있으면
        {
            MonsterAttack(_TurnMonster, _character);
        }
        else //플레이어 사망 시
        {
            Console.WriteLine($"{_character.Name}는 허접이래용");
        }
    }

    public void PlayerAttack(Character _character, Monster AMonster, int Damage) //플레이어의 공격
    {
        //Console.WriteLine($"AttackMethod : {Damage} Damage Has Been Attacked !!!");
        //Console.WriteLine($"{AMonster} ");
        AMonster.Hitted(_character.Name, Damage);
    }
    public void MonsterAttack(Monster _monster, Character _character) //몬스터의 공격 
    {
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
        BattleEndUI.BattleEnd(false, _monsters);
        // 마을 복귀 메서드
        GM.ShowMainMenu();
    }

    public void ClearDungeon() ///던전 종료 메서드 //보윤님 코드 우선
    {
        BattleEndUI.BattleEnd(true, _monsters);
        GM.ShowMainMenu();

        //Console.WriteLine("던전을 클리어 하였습니다.");
        //Console.WriteLine("원하시는 보상을 선택하세요.");
        //Console.WriteLine("1. 500 Gold\n2. 물약\n3. 랜덤상자");

        //Console.ReadLine();
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
