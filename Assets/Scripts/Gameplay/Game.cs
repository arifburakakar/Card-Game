using UnityEngine;

public partial class Game
{
    private GameplayManager gameplayManager;
    private GameObject gameContainer;
    private BoardProxy boardProxy;
    private Player player;
    private Dealer dealer;

    private bool isGameRunning = false;
    
    public Game(GameplayManager gameplayManager)
    {
        this.gameplayManager = gameplayManager;
        OnCreate();
    }
    
    private void OnCreate()
    {
        Main.Instance.MainUpdate += UpdateGame;
        gameContainer = new GameObject("Game");
        gameContainer.transform.SetParent(gameplayManager.RootObject.transform);
        boardProxy = gameplayManager.UIGameplayHud.boardProxy;
        GameObject dealerDeck = Object.Instantiate(gameplayManager.GameplayConfig.DealerDeck, gameContainer.transform);
        dealerDeck.transform.position = boardProxy.DealerPoint.position;
        
        InitializeInput();
        Main.Instance.SetInputEnable(false);
    }

    public void StartGame()
    {
        dealer = new Dealer();
        player = new Player();
        dealer.AddPlayer(player);
        dealer.DealCards(GameControllerSystem.Instance.UseZyngaCaseCards);
        CreateSlots();
        StartVisualDealSequence();
        isGameRunning = true;
    }

    private void UpdateGame()
    {
        if (!isGameRunning)
        {
            return;
        }
        
        UpdatePositions();
    }
    
    public void Clear()
    {
        Main.Instance.MainUpdate -= UpdateGame;
        ResetInput();
        Object.Destroy(gameContainer);
    }
}

// arc update
// raycast imput