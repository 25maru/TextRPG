using Tool;

public class InventoryScene : Scene
{
    private Character player;

    public override void Open()
    {
        player = GameManager.Instance.player;

        while (true)
        {
            Utils.ShowHeader("인벤토리", "보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("[아이템 목록]");

            string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;

            if (player.Inventory.Count == 0)
            {
                Utils.InfoText("- 보유 중인 아이템이 없습니다.");
            }
            else
            {
                foreach (var item in player.Inventory)
                {
                    string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                    string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                    bonus = Utils.FormatString(bonus, 8 + longestBonus);

                    Console.Write("- ");

                    if (item.IsEquipped)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{equippedMarker}");
                    Console.ResetColor();

                    Console.WriteLine($"{name} | {bonus} | {item.Description}");
                }
            }
            Console.WriteLine();

            if (player.Inventory.Count > 0)
                Utils.OptionText(1, "장착 관리");

            Utils.OptionText(0, "나가기");

            switch (Utils.GetInput(0, 1))
            {
                case 1:
                    if (player.Inventory.Count > 0) ManageEquipment();
                    break;
                case 0:
                    SceneManager.Instance.mainScene.Open();
                    break;
            }
        }
    }

    private void ManageEquipment()
    {
        while (true)
        {
            Utils.ShowHeader("인벤토리 - 장착 관리", "보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("[아이템 목록]");

            string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.DefenseBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                var item = player.Inventory[i];
                string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"방어력 +{item.DefenseBonus}";
                bonus = Utils.FormatString(bonus, 8 + longestBonus);

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{i + 1}");
                Console.ResetColor();

                if (i < 9)
                    Console.Write(".  ");
                else
                    Console.Write(". ");

                if (item.IsEquipped)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{equippedMarker}");
                Console.ResetColor();

                Console.WriteLine($"{name} | {bonus} | {item.Description}");
            }

            Console.WriteLine();

            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, player.Inventory.Count);

            if (input >= 1 && input <= player.Inventory.Count)
            {
                var item = player.Inventory[input - 1];

                if (item.IsEquipped)
                {
                    item.IsEquipped = false;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"'{item.Name}'");
                    Console.ResetColor();

                    Console.WriteLine("아이템의 장착을 해제했습니다.");
                }
                else
                    player.EquipItem(item);
            }
            else if (input == 0)
                break;
        }
    }
}
