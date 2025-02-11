using System.Numerics;
using Tool;

// 이상범님
public class Battle
{
    public List<Monster> monsters;
    public Character player;
    public List<Item> rewarditem;
    public Reward reward;

    /// <summary>
    /// 캐릭터, 몬스터 리스트, 리워드 리스트
    /// </summary>
    public Battle(Character player, List<Monster> monsters, List<Item> rewarditem, Reward reward)
    {
        this.player = player;
        this.monsters = monsters;
        this.rewarditem = rewarditem;
        this.reward = reward;
    }

    public void StartBattle()
    {
        player.IsBattle = true;

        while ((!player.IsDead) && (IsAliveMonstersExists(monsters))) // 몬스터 사망처리
        {
            PlayerTurn();
            for (int i = 0; i < monsters.Count; i++) //몬스터 만큼 턴을 씀 
            {
                if (monsters[i].IsDead) //번호 몬스터가 죽어있으면
                {
                    //그냥 넘어가기
                }
                else //살아있으면
                {
                    MonsterTurn(monsters[i], player);
                }
            }
        }
        ClearDungeon(); // 둘중 하나 죽어도 일단 클리어로
    }
    public void InstantiateMonster(List<Monster> monsters) //몬스터 생성 //위에꺼 쓰고 이건 안쓸듯 ..?
    {
        Console.WriteLine($"The Count Of Monsters ; {monsters.Count}");
        for (int i = 0; monsters.Count >= i; i++)
        {
            Monster monster = monsters[i];
        }
    }

    public bool IsAliveMonstersExists(List<Monster> monsters) // 살아있는 몬스터 존재여부
    {
        // Console.WriteLine($"남은 몬스터: {CountAliveMonsters(monsters)}/{monsters.Count}.");
        return monsters.Exists(monsters => !monsters.IsDead);
    }

    public int CountAliveMonsters(List<Monster> monsters) // 살아있는 몬스터 개체 수
    {
        int AllMonstersCounts = monsters.Count;
        int Alives = monsters.Count(monsters => !monsters.IsDead);
        int DeadBodies = monsters.Count(monsters => monsters.IsDead);

        return Alives;
    }

    public void PlayerTurn() //플레이어의 턴
    {
        Utils.ShowHeader("전투", "플레이어의 턴");

        Console.WriteLine("[내 정보]");
        Utils.PlayerText(player);

        Console.WriteLine("[몬스터 정보]");

        for (int i = 0; i < monsters.Count; i++) //몬스터 현재 상태 보여주기
        {
            monsters[i].ShowMonster();
        }
        Console.WriteLine();

        Utils.OptionText(1, "공격");
        Utils.OptionText(2, "회복 포션 사용");
        Utils.OptionText(3, "강화 포션 사용");
        Utils.OptionText(4, "던전 클리어 (테스트)");
        Utils.OptionText(5, "던전 실패 (테스트)");

        switch (Utils.GetInput(1, 5))
        {
            case 1:
                Utils.ShowHeader("공격", "몬스터를 선택해주세요.");

                for (int i = 0; i < monsters.Count; i++) //몬스터 현재 상태 보여주기
                {
                    monsters[i].ShowMonster(i + 1);
                }
                
                int TargetMonster = Utils.GetInput(0, monsters.Count + 1);

                if (monsters[TargetMonster - 1].IsDead)
                {
                    Utils.ErrorText($"선택하신 {monsters[TargetMonster - 1].Name}은 선택할 수 없는 대상입니다.\n유효한 대상을 선택해주세요.\n");
                    PlayerTurn();
                }
                else
                {
                    PlayerAttack(player, monsters[TargetMonster - 1], (int)player.TotalAttack);
                }
                break;

            case 2:
                Utils.ShowHeader("회복", "사용할 회복 포션을 선택해주새요");
                player.UsePotion("Heal");
                break;

            case 3:
                Utils.ShowHeader("강화", "사용할 강화 포션을 선택해주새요");
                Console.WriteLine("강화 포션을 사용합니다.");
                player.UsePotion("Power");
                break;

            case 4:
                Utils.ShowHeader("던전 클리어를 선택하셨습니다.", "던전 성공 메서드를 호출합니다.");
                ClearDungeon();
                break;
            case 5:
                Utils.ShowHeader("던전 실패를 선택하셨습니다", "던전 실패 메서드를 호출합니다.");
                FailedDungeon();
                break;
        }
    }

    public void MonsterTurn(Monster TurnMonster, Character character) //몬스터의 턴
    {
        Utils.ShowHeader("전투", "몬스터의 턴");

        if (character.IsDead == false) //플레이어가 살아있으면
        {
            MonsterAttack(TurnMonster, character);
        }
        else //플레이어 사망 시
        {
            Console.WriteLine($"{character.Name}는 허접이래용");
        }
    }

    public void PlayerAttack(Character character, Monster AMonster, int Damage) //플레이어의 공격
    {
        AMonster.Hitted(character, Damage);
    }

    public void MonsterAttack(Monster monster, Character character) //몬스터의 공격 
    {
        character.Health -= monster.Attacking(character);
        if (character.Health <= 0)
        {
            character.IsDead = true;
            FailedDungeon();
            Console.WriteLine($"{character.Name}가 죽었습니다. 마을로 돌아갑니다.");
        }
    }

    public void FailedDungeon() //던전 실패
    {
        BattleEndUI.BattleEnd(false, player, monsters, reward);
        
        // 마을 복귀 메서드
        SceneManager.Instance.mainScene.Open();
    }

    public void ClearDungeon() //던전 클리어 //보윤님 코드 연결
    {
        BattleEndUI.BattleEnd(true, player, monsters, reward);
        SceneManager.Instance.mainScene.Open();
    }
}
