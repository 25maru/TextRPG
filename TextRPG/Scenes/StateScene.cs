using Tool;

public class StateScene : Scene
{
    private Character player;

    public override void Open()
    {
        player = GameManager.Instance.player;

        while (true)
        {
            Utils.ShowHeader("상태 보기", "캐릭터의 정보가 표시됩니다.");

            player.ShowStatus();

            switch (Utils.GetInput(0, 0))
            {
                case 0:
                    SceneManager.Instance.mainScene.Open();
                    break;
            }
        }
    }
}
