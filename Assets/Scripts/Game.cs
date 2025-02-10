using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Game
{
    private GameplayManager gameplayManager;
    private GameObject GameContainer;
    public Game(GameplayManager gameplayManager)
    {
        this.gameplayManager = gameplayManager;
        OnCreate();
    }
    
    private void OnCreate()
    {
        Main.Instance.MainUpdate += UpdateGame;
        GameContainer = new GameObject("Game");
        GameContainer.transform.SetParent(gameplayManager.RootObject.transform);
    }

    public void StartGame()
    {
        Dealer dealer = new Dealer();
        Player player = new Player();
        dealer.AddPlayer(player);
        dealer.DealCards();
        
        player.SortHand(new SmartSort());
        player.PrintHand();
    }

    private void UpdateGame()
    {
        
    }
    
    public void Clear()
    {
        Main.Instance.MainUpdate -= UpdateGame;
    }
}

// multiple scene gecisler icin == loading gecis
// pool olusumu daha iyi sekilde
// debug ile yap sonra uia gecirirsin ya da sprite ile yapmanin yolunu coz
// kart dizilimi
// kart baslangic dagitim animasyonu
// kart tutma
// kart gezdirme

