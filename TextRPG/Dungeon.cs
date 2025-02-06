using System;

// 박지원님
public class Dungeon
{
    public string Name { get; }
    public List<Monster> Monsters { get; set; }

    public Dungeon(string name)
    {
        Name = name;
        Monsters = new List<Monster>();
    }
}
