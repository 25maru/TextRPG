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
        {
            PlayerTurn();
            for (int i = 0; i < _monsters.Count(); i++) //몬스터 만큼 턴을 씀 
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
        Console.WriteLine($"남은 몬스터: {CountAliveMonsters(_monsters)}/{_monsters.Count()}.");
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
        for (int i = 0; i < _monsters.Count; i++) //몬스터 현재 상태 보여주기
        {
            _monsters[i].ShowMonster(i + 1);
        }
        Console.WriteLine();

        GM.OptionText(1, "공격");
        GM.OptionText(2, "회복 포션 사용");
        GM.OptionText(3, "공격 포션 사용");
        GM.OptionText(4, "던전 클리어");
        GM.OptionText(5, "던전 실패");

        int KeyCode = GM.GetInput(1, 6);
        switch (KeyCode)
        {
            case 1:
                GM.ShowHeader("공격을 가합니다", "몬스터를 선택해주세요.");

                for (int i = 0; i < _monsters.Count; i++) //몬스터 현재 상태 보여주기
                {
                    _monsters[i].ShowMonster(i + 1);
                }
                int _TargetMonster = GM.GetInput(0, _monsters.Count + 1);

                if (_monsters[_TargetMonster - 1].IsDead)
                {
                    GM.ErrorText($"선택하신 {_monsters[_TargetMonster - 1].Name}은 선택할 수 없는 대상입니다.\n유효한 대상을 선택해주세요.\n");
                    PlayerTurn();
                }
                else
                {
                    PlayerAttack(_character, _monsters[_TargetMonster - 1], (int)_character.TotalAttack);
                }
                break;

            case 2:
                Console.WriteLine("회복 포션을 사용합니다.");
                _character.Health += 20;
                if (_character.Health >= _character.MaxHealth)
                {

                }
                break;

            case 3:
                Console.WriteLine("강화 포션을 사용합니다.");
                GM.ErrorText("미구현 항목입니다.");
                break;

            case 4:
                GM.ShowHeader("던전 클리어를 선택하셨습니다.", "던전 성공 메서드를 호출합니다.");
                ClearDungeon();
                break;
            case 5:
                GM.ShowHeader("던전 실패를 선택하셨습니다", "던전 실패 메서드를 호출합니다.");
                FailedDungeon();
                break;
            default:
                Console.WriteLine("어케 띄웠음 이걸");
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
        }
    }

    public void FailedDungeon() //던전 실패
    {
        BattleEndUI.BattleEnd(false, _monsters);
        GM.ShowMainMenu();
    }

    public void ClearDungeon() //던전 클리어 //보윤님 코드 연결
    {
        BattleEndUI.BattleEnd(true, _monsters);
        GM.ShowMainMenu();
    }
}
