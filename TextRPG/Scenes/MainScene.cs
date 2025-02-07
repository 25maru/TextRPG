using Tool;

public class MainScene : Scene
{
    public override void Open()
    {
        Utils.ShowHeader("마을", "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

        Utils.OptionText(1, "상태 보기");
        Utils.OptionText(2, "인벤토리");
        Utils.OptionText(3, "상점");
        Utils.OptionText(4, "던전");
        Utils.OptionText(5, "광산");
        Utils.OptionText(6, "휴식");

        switch (Utils.GetInput(1, 6))
        {
            case 1:
                SceneManager.Instance.stateScene.Open();
                break;
            case 2:
                SceneManager.Instance.inventoryScene.Open();
                break;
            case 3:
                SceneManager.Instance.storeScene.Open();
                break;
            case 4:
                SceneManager.Instance.dungeonScene.Open();
                break;
            case 5:
                // 미구현
                break;
            case 6:
                GameManager.Instance.player.Rest();
                break;
        }
    }
}