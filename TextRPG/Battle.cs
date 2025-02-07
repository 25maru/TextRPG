@ -10,6 + 10,9 @@ public class Battle

    GameManager GM = GameManager.Instance;

/// <summary>
/// 캐릭터, 몬스터 리스트, 리워드 리스트
/// </summary>
public Battle(Character B_character, List<Monster> B_monsters, List<Item> B_rewarditem)
{
    _character = B_character;
@ -21,14 + 24,21 @@ public class Battle
{
    GM.ShowHeader("전투 시작 !!!", "몬스터와의 전투를 시작합니다.");

        while ((!_character.IsDead) && (isAliveMonstersExists(_monsters))) // 몬스터 사망처리 우선구현
        {
        while ((!_character.IsDead) && (isAliveMonstersExists(_monsters))) // 몬스터 사망처리
        { //생각해보니 살아있는 만큼이 아니라 살아있는애만 ...
            PlayerTurn();
            for (int i = 0; i <= CountAliveMonsters(_monsters); i++) //죽은 몬스터 제외처리 필요
            for (int i = 0; i<_monsters.Count(); i++) //살아있는 몬스터 만큼 턴을 씀 
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
@ -42,6 + 52,7 @@ public class Battle

    public bool isAliveMonstersExists(List<Monster> _monsters) // 살아있는 몬스터 존재여부
{

    Console.WriteLine($"살아있는 몬스터가 {CountAliveMonsters(_monsters)} 마리 있음.");

    return _monsters.Exists(_monsters => !_monsters.IsDead);
@ -58,10 + 69,17 @@ public class Battle

    public void PlayerTurn() //플레이어의 턴
{

    foreach (var monster in _monsters)
    {
        Console.WriteLine("플레이어 턴 메소드 입니다.");

        //Console.WriteLine("플레이어 턴 메소드 입니다.");
    }
    for (int i = 0; i < CountAliveMonsters(_monsters); i++) //몬스터 현재 상태 보여주기
    {
        _monsters[i].ShowMonster(i + 1);
    }
    Console.WriteLine();

    GM.OptionText(1, "공격");
    GM.OptionText(2, "회복 포션 사용");
@ -71,7 + 89,21 @@ public class Battle
        switch (KeyCode)
{
    case 1:
        Console.WriteLine("공격을 가합니다.");
        Console.WriteLine("=================================");
        Console.WriteLine("공격을 가합니다.\n");

        Console.WriteLine("=================================");
        Console.WriteLine("어떤 몬스터에게 공격을 가합니까?");

        for (int i = 0; i < CountAliveMonsters(_monsters); i++) //몬스터 현재 상태 보여주기
        {
            Console.Write(i + 1);
            _monsters[i].ShowMonster(i + 1);
        }
        int _TargetMonster = GM.GetInput(0, _monsters.Count + 1);
        PlayerAttack(_character, _monsters[_TargetMonster - 1], (int)_character.TotalAttack);

                //_monsters[_TargetMonster - 1].Hitted(_character.Name,(int)_character.TotalAttack);

                //int _monsterNum = int.Parse(Console.ReadLine()); //남은 몬스터까지만 번호 보여주기 필요
                //if(_monsterNum >= )
@ -80,12 + 112,17 @@ public class Battle

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
@ -94,7 + 131,6 @@ public class Battle
{
        if (_character.IsDead == false) //플레이어가 살아있으면
        {
            Console.WriteLine("MonsterTurnMethod");
            MonsterAttack(_TurnMonster, _character);
}
        else //플레이어 사망 시
@ -105,8 + 141,8 @@ public class Battle

    public void PlayerAttack(Character _character, Monster AMonster, int Damage) //플레이어의 공격
{
    Console.WriteLine($"AttackMethod : {Damage} Damage Has Been Attacked !!!");
    Console.WriteLine($"{AMonster} ");
    //Console.WriteLine($"AttackMethod : {Damage} Damage Has Been Attacked !!!");
    //Console.WriteLine($"{AMonster} ");
    AMonster.Hitted(_character.Name, Damage);
}
public void MonsterAttack(Monster _monster, Character _character) //몬스터의 공격 
@ -123,17 + 159,21 @@ public class Battle

    public void FailedDungeon()
{
    BattleEndUI.BattleEnd(false, _monsters);
    // 마을 복귀 메서드
    GM.ShowMainMenu();
}

public void ClearDungeon() ///던전 종료 메서드 //보윤님 코드 우선
{
    Console.WriteLine("던전을 클리어 하였습니다.");
    Console.WriteLine("원하시는 보상을 선택하세요.");
    Console.WriteLine("1. 500 Gold\n2. 물약\n3. 랜덤상자");
    BattleEndUI.BattleEnd(true, _monsters);
    GM.ShowMainMenu();

    //Console.WriteLine("던전을 클리어 하였습니다.");
    //Console.WriteLine("원하시는 보상을 선택하세요.");
    //Console.WriteLine("1. 500 Gold\n2. 물약\n3. 랜덤상자");

    Console.ReadLine();
    //Console.ReadLine();
    //GiveReward(); //종료시 보상
}

