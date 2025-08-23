using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public static string selectedFilePath;

    public static bool autoResultsValid = false;
    
    
    public static bool gameResumed = false;
    
    public static int playerNumber = 0;

    public static bool shortMatch = false;

    public static int classSelected = 0;

    public static string[] cars = { "CarA", "CarB", "CarC", "CarD", "CarE", "CarF" };
    public static bool[] carIsInDefault = { false, false, false, false, false, false };
    public static bool[] DefProcedureCompleted = { false, false, false, false, false, false };
    public static List<bool> playerOutofOptions = new List<bool> { false, false, false, false, false, false };
    public static bool[] spectator = { false, false, false, false, false, false };
    public static int playerWithBuyOption = 0;
    public static bool[] protection = { false, false, false, false, false, false };
    public static bool[] shieldAvailable = { true, true, true, true, true };

    public static string[] activeTracks = { "Track1", "Track2", "Track3", "Track4", "Track5", "Track6", "Track7", "Track8", "Track9" };

    public static string bonusTrack = "";

    public static string[] playerNames = { "Player1", "Player2", "Player3", "Player4", "Player5", "Player6" };

    public static int[] fieldsLeftForCar = { 10, 10, 10, 10, 10, 10 };
    public static bool[] fieldAvailable = new bool[60];

    public static int pendingField = 0;
    public static int currentCarIndex = 0;

    public static int TimeBattleCarIndex = 0;

    public static int activePlayer = 0;

    public static int[] inactivePlayers = { 0, 1, 2, 3, 4};
    public static int defendingPlayer = 0;

    public static int roundCounter = 1;
    public static int levelCounter = 1;
    public static int raceThreshold = 0;

    public static int raceWinner = 0;
    public static int seller = 0;
    public static int buyer = 0;

    public static bool activePlayerWins = false;

    public static bool IsToynopolyBattle = false;

    public static int changeValue = 0;

    
    //game Scores New

    public static int[] playerCash = {150, 150, 150, 150, 150, 150};
    public static int[,] playerInventory = { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
    //inventory format is player, number of cars
    




    //game Market Data

    public static int[] carPrizes = { 20, 20, 20, 20, 20, 20 };

    public static int timeBattleSeconds = 0;

    public static bool gameOver = false;

    public static bool matchTimeDisplayed = true;

    public static List<int> tempdividends = new List<int> { };

    


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


       
}
