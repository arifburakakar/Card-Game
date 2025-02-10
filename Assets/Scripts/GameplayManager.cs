using System.Collections;
using UnityEngine;

public class GameplayManager : BaseManager
{
    public PoolHandler PoolHandler { get; private set; }
    private Game activeGame;
    
    protected override void OnInitialize()
    {
        base.OnInitialize();
        PoolHandler = new PoolHandler();
        PoolHandler.Initialize(Resources.Load<GameItemsConfig>("GameItemsConfig"));
    }

    public override void CreateManagerNecessary()
    {
        base.CreateManagerNecessary();
        
        //find better way to subscribe
        UIGameplayHudPanel gameplayController = Object.Instantiate(Resources.Load<UIGameplayHudPanel>("UI Gameplay Controller"));
        gameplayController.Create(RootObject.transform);
        UIControllerSystem.Instance.GameplayController = gameplayController;
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