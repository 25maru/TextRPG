using System.Numerics;
using Tool;

public class DungeonScene : Scene
{
    private Character? player;
    private List<Dungeon>? dungeons;

    public override void Open()
    {
        player = GameManager.Instance.player;
        dungeons = GameManager.Instance.dungeons;

        Utils.ShowHeader("던전 입구", "입장할 던전의 난이도를 선택할 수 있습니다.");

        Utils.OptionText(1, "초급 던전");
        Utils.OptionText(2, "중급 던전");
        Utils.OptionText(3, "상급 던전");
        Utils.OptionText(4, "보스 던전");
        Utils.OptionText(0, "나가기");

        int input = Utils.GetInput(0, 4);

        switch (input)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                dungeons[input - 1].Enter(player);
                break;
            case 0:
                SceneManager.Instance.mainScene.Open();
                break;
        }
    }
}
