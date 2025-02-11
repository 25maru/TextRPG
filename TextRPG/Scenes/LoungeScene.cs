using System.Numerics;
using Tool;

public class LoungeScene : Scene
{
    private Character? player;

    public override void Open()
    {
        player = GameManager.Instance.player;

        while (true)
        {
            Utils.ShowHeader("휴식", "500 G 를 내면 체력을 회복할 수 있습니다.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");
            Console.ResetColor();

            Utils.OptionText(1, "휴식하기");
            Utils.OptionText(0, "나가기");

            switch (Utils.GetInput(0, 1))
            {
                case 1:
                    player.Rest();
                    break;
                case 0:
                    SceneManager.Instance.mainScene.Open();
                    break;
            }
        }
    }
}