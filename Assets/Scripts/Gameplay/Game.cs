using UnityEngine;

public partial class Game
{
    private GameplayManager gameplayManager;
    private GameObject gameContainer;
    private BoardProxy boardProxy;
    private Player player;
    private Dealer dealer;
    
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
    }

    public void StartGame()
    {
        dealer = new Dealer();
        player = new Player();
        
        dealer.AddPlayer(player);
        dealer.DealCards();
        
        
        CreateSlots();
        StartDealSequence();
    }

    private void UpdateGame()
    {
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