// 송영주님
public class Monster
{
    public string Name {  get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool IsDead { get; set; }

    public Monster(string name, int level, int health, int attack)
    {
        if (health < 1)
        {
            throw new ArgumentException("체력을 1 이상으로 설정해주세요");
        }
        Name = name;
        Level = level;
        Health = health;
        Attack = attack;
        IsDead = false;
    }

    /// <summary>
    /// 몬스터의 공격(텍스트 출력 포함)
    /// </summary>
    /// <param name="name">플레이어의 이름</param>
    /// <returns>데미지</returns>
    public int Attacking(string name)
    {
        Console.Write("Lv.");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{Level} ");

        Console.ResetColor();
        Console.Write($"{Name} 의 공격");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("!");

        Console.ResetColor();
        Console.Write($"{name} 을(를) 맞췄습니다.  [데미지 : ");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{Attack}");

        Console.ResetColor();
        Console.WriteLine("]");

        Console.ResetColor();
        Console.WriteLine("");

        return Attack;
    }

    /// <summary>
    /// 몬스터가 맞는 공격(텍스트 출력 포함)
    /// </summary>
    /// <param name="name">플레이어 이름</param>
    /// <param name="damage">플레이어 데미지</param>
    public void Hitted(string name, int damage)
    {
        Console.Write($"{name} 의 공격");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("!");

        Console.ResetColor();
        Console.Write("Lv.");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{Level} ");

        Console.ResetColor();
        Console.Write($"{Name} 을(를) 맞췄습니다.  [데미지 : ");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{damage}");

        Console.ResetColor();
        Console.WriteLine("]");

        Console.ResetColor();
        Console.WriteLine("");

        Console.ResetColor();
        Console.Write("Lv. ");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{Level} ");

        Console.ResetColor();
        Console.WriteLine($"{Name}");

        if (Health > damage)
        {
            Console.ResetColor();
            Console.Write("HP ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Health} ");

            Console.ResetColor();
            Console.Write("-> ");

            Health = Health - damage;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Health}");

            Console.ResetColor();
            Console.WriteLine("");
        }
        else
        {
            Console.ResetColor();
            Console.Write("HP ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Health} ");

            Console.ResetColor();
            Console.Write("-> ");

            Health = 0;
            IsDead = true;

            Console.ResetColor();
            Console.WriteLine("Dead");

            Console.ResetColor();
            Console.WriteLine("");
        }
    }

    /// <summary>
    /// 몬스터 출력
    /// </summary>
    /// <param name="number">몇 번째 몬스터인지</param>
    public void ShowMonster(int number)
    {
        if (IsDead == false)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{number} ");

            Console.ResetColor();
            Console.Write("Lv.");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{Level} ");

            Console.ResetColor();
            Console.Write($"{Name} ");

            Console.ResetColor();
            Console.Write("HP ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Health}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{number} ");

            Console.Write("Lv.");

            Console.Write($"{Level} ");

            Console.Write($"{Name} ");

            Console.WriteLine($"Dead");
            Console.ResetColor();
        }
    }
}
