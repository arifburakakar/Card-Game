using System.Collections;
using UnityEngine;

public class GameplayManager : BaseManager
{
    public GameplayConfig GameplayConfig { get; private set; }
    public PoolHandler PoolHandler { get; private set; }
    private Game activeGame;
    public UIGameplayHudPanel UIGameplayHud;

    public Game ActiveGame => activeGame;
    
    protected override void OnInitialize()
    {
        base.OnInitialize();
        GameplayConfig = Resources.Load<GameplayConfig>("GameplayConfig");
        PoolHandler = new PoolHandler();
        PoolHandler.Initialize(Resources.Load<GameItemsConfig>("GameItemsConfig"));
    }

    public override void CreateManagerNecessary()
    {
        base.CreateManagerNecessary();
        
        //find better way to subscribe
        UIGameplayHud = Object.Instantiate(Resources.Load<UIGameplayHudPanel>("UI Gameplay Hud Panel"));
        UIGameplayHud.Create(RootObject.transform);
        UIControllerSystem.Instance.GameplayController = UIGameplayHud;
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        activeGame = new Game(this);
    }

    public override void OnExecute()
    {
        base.OnExecute();
        activeGame.StartGame();
    }
    

    protected override void OnDisable()
    {
        base.OnDisable();
        PoolHandler.Clear();
        activeGame?.Clear();
        activeGame = null;
    }
}