using Tool;

public class MineScene : Scene
{
    private Character? player;
    private List<Dungeon>? dungeons;

    public override void Open()
    {
        player = GameManager.Instance.player;
        dungeons = GameManager.Instance.dungeons;

        Utils.ShowHeader("광산 입구", "입장할 광산을 선택할 수 있습니다.");

        Utils.OptionText(1, "일반 광산");
        Utils.OptionText(2, "특수 광산");
        Utils.OptionText(0, "나가기");

        int input = Utils.GetInput(0, 2);

        switch (input)
        {
            case 1:
                Utils.ErrorText("입장 레벨 조건을 충족하지 못했습니다. (Lv. 5)");
                break;
            case 2:
                Utils.ErrorText("입장 레벨 조건을 충족하지 못했습니다. (Lv. 25)");
                break;
            case 0:
                SceneManager.Instance.mainScene.Open();
                break;
        }
    }
}
