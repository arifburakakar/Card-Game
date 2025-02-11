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
        GameObject dealerDeck = Object.Instantiate(gameplayManager.GameplayConfig.DealerDeck);
        dealerDeck.transform.position = boardProxy.DealerPoint.position;
    }

    public void StartGame()
    {
        dealer = new Dealer();
        player = new Player();
        
        dealer.AddPlayer(player);
        dealer.DealCards();
        
        
        CreateSlots();
        StartDealSequence();

        isGameRunning = true;
    }

    private void UpdateGame()
    {
        if (!isGameRunning)
        {
            return;
        }
        UpdateHandCardPositions();
    }
    
    public void Clear()
    {
        Main.Instance.MainUpdate -= UpdateGame;
    }
}

// card donme animasyonu
// card gitme animasyonu
// card slotlari olusturmasi 
// cardlari dagismasi
// transitaionlari oynadiktan sonra kapat
// item data pass in pool
// smart sort refactor 