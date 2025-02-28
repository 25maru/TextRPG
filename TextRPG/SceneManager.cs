﻿public class SceneManager
{
    private static SceneManager instance;
    public static SceneManager Instance = instance ??= new SceneManager();

    public MainScene mainScene;
    public StateScene stateScene;
    public InventoryScene inventoryScene;
    public StoreScene storeScene;
    public DungeonScene dungeonScene;

    private SceneManager()
    {
        mainScene = new MainScene();
        stateScene = new StateScene();
        inventoryScene = new InventoryScene();
        storeScene = new StoreScene();
        dungeonScene = new DungeonScene();
    }
}
