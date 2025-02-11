using System.Numerics;
using Tool;

public class StoreScene : Scene
{
    private Character? player;
    private List<Item>? shopItems;

    public override void Open()
    {
        player = GameManager.Instance.player;
        shopItems = GameManager.Instance.shopItems;

        while (true)
        {
            Utils.ShowHeader("상점", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            string longestItem = shopItems.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = shopItems.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.HealthBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
            string longestDescription = shopItems.OrderByDescending(item => Utils.GetDisplayWidth(item.Description)).First().Description;

            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                string bonus = item.AttackBonus > 0 ? $"공격력 +{item.AttackBonus}" : $"체력   +{item.HealthBonus}";
                bonus = Utils.FormatString(bonus, 8 + longestBonus);
                string description = Utils.FormatString($"{item.Description}", Utils.GetDisplayWidth(longestDescription));
                string price = item.IsPurchased ? "구매완료" : $"{item.Price} G";

                if (item.IsPurchased)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"- {name} | {bonus} | {description} | {price}");
                Console.ResetColor();
            }

            Console.WriteLine();

            Utils.OptionText(1, "아이템 구매");
            Utils.OptionText(2, "아이템 판매");
            Utils.OptionText(0, "나가기");

            switch (Utils.GetInput(0, 2))
            {
                case 1:
                    PurchaseItem();
                    break;
                case 2:
                    SellItem();
                    break;
                case 0:
                    SceneManager.Instance.mainScene.Open();
                    break;
            }
        }
    }

    /// <summary>
    /// 아이템 구매 화면
    /// </summary>
    private void PurchaseItem()
    {
        player = GameManager.Instance.player;
        shopItems = GameManager.Instance.shopItems;

        while (true)
        {
            Utils.ShowHeader("상점 - 아이템 구매", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            string longestItem = shopItems.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
            int longestBonus = shopItems.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.HealthBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
            string longestDescription = shopItems.OrderByDescending(item => Utils.GetDisplayWidth(item.Description)).First().Description;

            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];

                string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"체력   +{item.HealthBonus}";
                bonus = Utils.FormatString(bonus, 8 + longestBonus);
                string description = Utils.FormatString($"{item.Description}", Utils.GetDisplayWidth(longestDescription));
                string price = item.IsPurchased ? "구매완료" : $"{item.Price} G";

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"{i + 1}");

                Console.ResetColor();

                if (i < 9)
                    Console.Write(".  ");
                else
                    Console.Write(". ");

                if (item.IsPurchased)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{name} | {bonus} | {description} | {price}");
                Console.ResetColor();
            }

            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, shopItems.Count);

            if (input >= 1 && input <= shopItems.Count)
            {
                var item = shopItems[input - 1];

                if (item.IsPurchased)
                    Utils.ErrorText("이미 구매한 아이템입니다.");
                else if (player.Gold >= item.Price)
                {
                    player.Gold -= item.Price;
                    if (item.Type != ItemType.Potion) item.IsPurchased = true;
                    player.Inventory.Add(item);
                    Console.WriteLine("구매를 완료했습니다.");
                }
                else
                    Utils.ErrorText("Gold 가 부족합니다.");
            }
            else if (input == 0)
                break;
        }
    }

    /// <summary>
    /// 아이템 판매 화면
    /// </summary>
    private void SellItem()
    {
        player = GameManager.Instance.player;
        shopItems = GameManager.Instance.shopItems;

        while (true)
        {
            Utils.ShowHeader("상점 - 아이템 판매", "필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Console.WriteLine("[아이템 목록]");

            if (player.Inventory.Count == 0)
                Utils.InfoText("판매할 아이템이 없습니다.");
            else
            {
                string longestItem = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Name)).First().Name;
                int longestBonus = player.Inventory.Select(item => item.Type == ItemType.Weapon ? item.AttackBonus.ToString() : item.HealthBonus.ToString()).OrderByDescending(bonus => bonus.Length).First().Length;
                string longestDescription = player.Inventory.OrderByDescending(item => Utils.GetDisplayWidth(item.Description)).First().Description;

                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    var item = player.Inventory[i];
                    int sellPrice = (int)(item.Price * 0.85);

                    string name = Utils.FormatString($"{item.Name}", Utils.GetDisplayWidth(longestItem));
                    string bonus = item.Type == ItemType.Weapon ? $"공격력 +{item.AttackBonus}" : $"체력   +{item.HealthBonus}";
                    bonus = Utils.FormatString(bonus, 8 + longestBonus);
                    string description = Utils.FormatString($"{item.Description}", Utils.GetDisplayWidth(longestDescription));
                    string price = item.CanSell ? $"{sellPrice} G" : "판매불가";

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"{i + 1}");

                    Console.ResetColor();

                    if (i < 9)
                        Console.Write(".  ");
                    else
                        Console.Write(". ");

                    if (!item.CanSell)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{name} | {bonus} | {description} | {price}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine();

            Utils.OptionText(0, "나가기");

            int input = Utils.GetInput(0, player.Inventory.Count);

            if (input >= 1 && input <= player.Inventory.Count)
            {
                var item = player.Inventory[input - 1];

                if (!item.CanSell)
                    Utils.ErrorText("판매할 수 없는 아이템입니다.");
                else
                {
                    if (item.IsEquipped)
                    {
                        item.IsEquipped = false;
                        Console.WriteLine("현재 장착 중인 아이템입니다.");
                        Console.Write("장착을 해제하고 판매하시겠습니까? ");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("(");

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("Y");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(". 판매하기 / ");

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("N");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(". 나가기)");

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(">> ");
                        string choice = Console.ReadLine();
                        Console.ResetColor();

                        if (choice == "Y" || choice == "y")
                        {
                            player.Gold += (int)(item.Price * 0.85);
                            item.IsPurchased = false;
                            player.Inventory.Remove(item);
                            Console.WriteLine("판매를 완료했습니다.");
                        }
                        else if (choice == "N" || choice == "n")
                            break;
                        else
                            Utils.ErrorText("잘못된 입력입니다. 다시 시도해주세요.");
                    }
                    else
                    {
                        player.Gold += (int)(item.Price * 0.85);
                        item.IsPurchased = false;
                        player.Inventory.Remove(item);
                        Console.WriteLine("판매를 완료했습니다.");
                    }
                }
            }
            else if (input == 0)
                break;
        }
    }
}
