using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public static int playerNumber = 0;

    public static int classSelected = 0;

    public static string[] cars = { "CarA", "CarB", "CarC", "CarD", "CarE", "CarF" };
    public static bool[] carIsInDefault = { false, false, false, false, false, false };
    public static bool[] DefProcedureCompleted = { false, false, false, false, false, false };
    public static List<bool> playerOutofOptions = new List<bool> { false, false, false, false, false, false };
    public static int playerWithBuyOption = 0;

    public static string[] activeTracks = { "Track1", "Track2", "Track3", "Track4", "Track5", "Track6", "Track7", "Track8", "Track9" };

    public static string bonusTrack = "";

    public static string[] playerNames = { "Player1", "Player2", "Player3", "Player4", "Player5", "Player6" };

    public static int[] fieldsLeftForCar = { 10, 10, 10, 10, 10, 10 };

    public static int pendingField = 0;
    public static int currentCarIndex = 0;

    public static int TimeBattleCarIndex = 0;

    public static int activePlayer = 0;

    public static int[] inactivePlayers = { 0, 1, 2, 3, 4};
    public static int defendingPlayer = 0;

    public static int roundCounter = 1;
    public static int levelCounter = 1;

    public static int raceWinner = 0;
    public static int seller = 0;

    public static bool activePlayerWins = false;

    //game Scores Old

    public static int player1Cash = 150;
    public static int player2Cash = 150;

    public static int[] p1Inventory = { 0, 0, 0, 0, 0, 0 };
    public static int[] p2Inventory = { 0, 0, 0, 0, 0, 0 };

    //game Scores New

    public static int[] playerCash = {150, 150, 150, 150, 150, 150};
    public static int[,] playerInventory = { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };



    //game Market Data

    public static int[] carPrizes = { 20, 20, 20, 20, 20, 20 };

    public static int timeBattleSeconds = 0;

    public static bool gameOver = false;


    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)

        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
