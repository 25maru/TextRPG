using Tool;

public class InventoryScene : Scene
{
    private Character? player;

    public override void Open()
    {
        player = GameManager.Instance.player;

        while (true)
        {
            Utils.ShowHeader("인벤토리", "보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("[아이템 목록]\n");

            player.Inventory = player.Inventory.OrderBy(item => item.Type == ItemType.Potion ? 1 : 0).ToList();

            string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.HealthBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
            string longestDescription = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Description)).First().Description;

            if (player.Inventory.Count == 0)
            {
                Utils.InfoText("- 보유 중인 아이템이 없습니다.");
            }
            else
            {
                // Console.ForegroundColor = ConsoleColor.Cyan;
                // Console.Write("(장비)");
                // Console.ResetColor();
                // Console.Write(new string('-', 1 + Utils.GetDisplayWidth(longestItem)) + "+");
                // Console.Write(new string('-', 9 + longestBonus + 1) + "+");
                // Console.WriteLine(new string('-', 1 + Utils.GetDisplayWidth(longestDescription)));

                foreach (var item in player.Inventory)
                {
                    if (item.Type != ItemType.Potion)
                    {
                        string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                        string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                        string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"체력   +{item.HealthBonus}";
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

                // Console.ForegroundColor = ConsoleColor.Cyan;
                // Console.Write("(소비)");
                // Console.ResetColor();
                // Console.Write(new string('-', 1 + Utils.GetDisplayWidth(longestItem)) + "+");
                // Console.Write(new string('-', 9 + longestBonus + 1) + "+");
                // Console.WriteLine(new string('-', 1 + Utils.GetDisplayWidth(longestDescription)));

                Console.WriteLine();

                foreach (var item in player.Inventory)
                {
                    if (item.Type == ItemType.Potion)
                    {
                        string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                        string bonus = item.AttackBonus > 0 ? $"공격력 +{item.AttackBonus}" : (item.HealthBonus > 0 ? $"체력   +{item.HealthBonus}" : "        ");
                        bonus = Utils.FormatString(bonus, 8 + longestBonus);

                        Console.Write("- ");

                        // Console.WriteLine($"    {name} | {bonus} | {item.Description}");
                        Console.WriteLine($"    {name} | {bonus}");
                    }
                }

                // Console.Write(new string('-', 6 + Utils.GetDisplayWidth(longestItem) + 1) + "+");
                // Console.Write(new string('-', 9 + longestBonus + 1) + "+");
                // Console.WriteLine(new string('-', 1 + Utils.GetDisplayWidth(longestDescription)));
            }
            Console.WriteLine();

            if (player.Inventory.Where(item => item.Type != ItemType.Potion).ToList().Count > 0)
                Utils.OptionText(1, "장착 관리");
            else
                Utils.InfoText("1. 장착 관리");

            if (player.Inventory.Where(item => item.Type == ItemType.Potion).ToList().Count > 0)
                Utils.OptionText(2, "소비 아이템 관리");
            else
                Utils.InfoText("2. 소비 아이템 관리");

            Utils.OptionText(0, "나가기");

            switch (Utils.GetInput(0, 2))
            {
                case 1:
                    if (player.Inventory.Where(item => item.Type != ItemType.Potion).ToList().Count > 0)
                        ManageEquipment();
                    else
                        Utils.ErrorText("장착할 수 있는 아이템이 없습니다.");
                    break;
                case 2:
                    if (player.Inventory.Where(item => item.Type == ItemType.Potion).ToList().Count > 0)
                        ManageUseItems();
                    else
                        Utils.ErrorText("사용할 수 있는 아이템이 없습니다.");
                    break;
                case 0:
                    SceneManager.Instance.mainScene.Open();
                    break;
            }
        }
    }

    /// <summary>
    /// 장착/장착해제 관리 화면
    /// </summary>
    private void ManageEquipment()
    {
        player = GameManager.Instance.player;

        while (true)
        {
            Utils.ShowHeader("인벤토리 - 장착 관리", "보유 중인 아이템을 사용할 수 있습니다.");

            Console.WriteLine("[아이템 목록]");

            player.Inventory = player.Inventory.OrderBy(item => item.Type == ItemType.Potion ? 1 : 0).ToList();

            string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.HealthBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                var item = player.Inventory[i];

                if (item.Type != ItemType.Potion)
                {
                    string equippedMarker = item.IsEquipped ? "[E] " : "[ ] ";
                    string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"체력   +{item.HealthBonus}";
                    bonus = Utils.FormatString(bonus, 8 + longestBonus);

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(i + 1);
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
            }

            Console.WriteLine();

            Utils.OptionText(0, "나가기");

            // 포션을 제외한 아이템 리스트 생성
            var filteredInventory = player.Inventory.Where(item => item.Type != ItemType.Potion).ToList();

            int input = Utils.GetInput(0, filteredInventory.Count);

            if (input >= 1 && input <= filteredInventory.Count)
            {
                var item = filteredInventory[input - 1];

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

    private void ManageUseItems()
    {
        player = GameManager.Instance.player;

        while (true)
        {
            Utils.ShowHeader("인벤토리 - 소비 아이템 관리", "보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("[아이템 목록]");

            player.Inventory = player.Inventory.OrderBy(item => item.Type != ItemType.Potion ? 1 : 0).ToList();

            string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.HealthBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;

            for (int i = 0; i < player.Inventory.Count; i++)
            {
                var item = player.Inventory[i];

                if (item.Type == ItemType.Potion)
                {
                    string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                    string bonus = item.AttackBonus > 0 ? $"공격력 +{item.AttackBonus}" : (item.HealthBonus > 0 ? $"체력   +{item.HealthBonus}" : "        ");
                    bonus = Utils.FormatString(bonus, 8 + longestBonus);

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(i + 1);
                    Console.ResetColor();

                    if (i < 9)
                        Console.Write(".  ");
                    else
                        Console.Write(". ");

                    Console.WriteLine($"{name} | {bonus} | {item.Description}");
                    // Console.WriteLine($"{name} | {bonus}");
                }
            }

            Console.WriteLine();

            Utils.OptionText(0, "나가기");

            // 포션 아이템 리스트 생성
            var filteredInventory = player.Inventory.Where(item => item.Type == ItemType.Potion).ToList();

            int input = Utils.GetInput(0, filteredInventory.Count);

            if (input >= 1 && input <= filteredInventory.Count)
            {
                var item = filteredInventory[input - 1];

                string type = item.AttackBonus > 0 ? $"Power" : (item.HealthBonus > 0 ? $"Heal" : "RandomBox");
                player.UsePotion(type);
            }
            else if (input == 0)
                break;
        }
    }
}
