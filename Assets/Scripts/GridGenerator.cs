using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GridGenerator : MonoBehaviour
      
    {

    public Button carPicA;
    public Button carPicB;
    public Button carPicC;
    public Button carPicD;
    public Button carPicE;
    public Button carPicF;

    [SerializeField] TextMeshProUGUI track1;
    [SerializeField] TextMeshProUGUI track2;
    [SerializeField] TextMeshProUGUI track3;
    [SerializeField] TextMeshProUGUI track4;
    [SerializeField] TextMeshProUGUI track5;
    [SerializeField] TextMeshProUGUI track6;
    [SerializeField] TextMeshProUGUI track7;
    [SerializeField] TextMeshProUGUI track8;
    [SerializeField] TextMeshProUGUI track9;

    [SerializeField] TextMeshProUGUI bonusTrack;

    [SerializeField] TextMeshProUGUI carAText;
    [SerializeField] TextMeshProUGUI carBText;
    [SerializeField] TextMeshProUGUI carCText;
    [SerializeField] TextMeshProUGUI carDText;
    [SerializeField] TextMeshProUGUI carEText;
    [SerializeField] TextMeshProUGUI carFText;

    [SerializeField] TextMeshProUGUI player1NameField;
    [SerializeField] TextMeshProUGUI player2NameField;

    public TextMeshProUGUI p1NameInputField;
    public TextMeshProUGUI p2NameInputField;


    [SerializeField] Button carASprite;
    [SerializeField] Button carBSprite;
    [SerializeField] Button carCSprite;
    [SerializeField] Button carDSprite;
    [SerializeField] Button carESprite;
    [SerializeField] Button carFSprite;

    [SerializeField] Button playerNamesSubmit;

    [SerializeField] GameObject enterPlayerNamesPanel;

    private GameManager gameManagerScript;

        

    List<string> trackList = new List<string>

    {
        "School's Out 1",
        "Museum 1","Toy World Aquatica: Redux","Ghost Town 1","Rooftops 1","Rooftops","Castle 1","HMS Invincible Redux","Aspenside",
        "Ranch","Airport","Fairground 1","Port Limano 2","StadVolt","Toytanic 2","Casino RV","Supermarket 1","Biohazard Factory","Toys In The Hood 2",
        "Toy World Mayhem","Smashride Circuit","RV Temple","Meltdown","Petro Volt","Botanical Garden ","Mysterious Toy-Volt Factory 1","Snowland 1","Home 2",
        "Subway 2","Mysterious Toy-Volt Factory 2","School's Out 2","Moon Dawn","Radioactive Garden","Toy World 1","Holiday Camp California Edition","ToySoldierz",
        "Santorini","Kadish Sprint","The Great Silence","Spa-Volt 1","Lunar","Skating Toys Redux","Museum EX","Library","Sakura","Wildland","Hospital 2","Museum 3",
        "Home 1","Rooftop Chase Redux","Hospital 1","Game Room 2","Venice","Quake!","Mid-sea Island","Metro-Volt","urbanX","Toytanic 1","Industry","Snowy River","Toy World 3",
        "Game Room 1","Botanical Garden EX","Helios","Subway","Paper Town 1","Route-77","Castle 2","Urban Sprint 1","Wonderful Skylands 1","Hull Breach 3000","Fairground 2",
        "Supermarket 2","White Rose Chapel","Grisville","Spaceship","RC Stadium 2","Images Of Giza: Redux","Toy World 2","Toys in The Hood 1","The Bunker","Spa-Volt 2",
        "Medieval Redux","Port Limano 1","SBX Alpine","Jailhouse Rock","Ghost Town 2","Museum 2","Shoppe","Re-Ville","Cliffside","POD: Roc","High Rollers","Cliffside Court",
        "Floating World","Synthwave","Battered Mansion 2","Desolate District 1","Downtown 1","Downtown 2","Port Limano EX"
    };

    List<string> bonusTrackList = new List<string>

    { "Airport 2","Battered Mansion 1","Battered Mansion 2","Best-Milk Farm","Chilled to the Bone","Cumulonimbus Clouds","Donut Plains 3","Freestoyle 2","GBA Bowser Castle 3",
        "Hallow's Eve","High Rollers","Illusion","Junkyard 2","Market Mayhem","Miami Manic","Molten Caverns","Mid-sea Island","Palm Marsh","Paper Town 1","Penny Racers - Caves"
        ,"Penny Racers - Harbour","POD: Roc","Port Limano EX","RC Stadium 1","Rigs Highway","Road in the Sky","Spooky-Volt","Subway","Swan Street","Temple of the Burning Darkness"
        ,"Tetris Festival","Terminus","The Felling Yard","Toy World EX","Toys in the Hood EX","Wonderful Skylands 2","Avalon","Belgium","Blood on the Rooftops","Broken Sunlight"
        ,"Cake","Chess Night","Christmas Snow Globe","Christmas Special Stage","Cliffside","Cliffside Court","Desolate District 1","Downhill Jam (THUG2)","Endgame","Floating World",
        "Fools Mate 2","Fukushima","Genghis Kastle","Go Play Outside!","Ground N Smash 2","Harbor Lights"
    };


    List<string> activeList;

    List<string> rookieNamesList = new List<string>

    {
        "Toukka 4x4","Starfire GT","Lancer","El Gekko","Condor GRV","Junky","Rouge","Get Air","BigVolt","Road Star","Sunset Light","Show-Off","Nimbus","Harvester","Rebound 4x4","Albatross GT",
        "Updraft","Chubble","El Rapido","Vaanbus","Kanberra Kruiser","Blobster","Col. Moss","Angus 400","Nesbitt","Hot Spot","Micro","Phat Slug","Hurricane","LR 64","Super Wheat","Dust Mite","High-Rod",
        "Crazy Pat","Myrmech","Mr. Bedtime","Tesla","Funziona","Phat Trucker","Splat","Panorama","Ciagnik","Genghis Kar","Quaqa Turbo","Volken Turbo","HSF-1","Pipsqueak","Naranja Turbo","RC Phink","E-Razr", "FunkFlea"
    };

    public List<Sprite> rookieSpriteList = new List<Sprite>

    { };

    List<T> GetUniqueRandomElements<T>(List<T> inputList, int count)

    {
        List<T> inputListClone = new List<T>(inputList);
        Shuffle(inputListClone);
        return inputListClone.GetRange(0, count);

    }

    void Awake()
    {
        CarSelect();

        TrackSelect();
    }

   


    void Shuffle<T>(List<T> inputList)

    {
        for (int i = 0; i < inputList.Count - 1; i++)

        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }

    public void CarSelect()

    {
        switch (MainManager.classSelected)

            {
                case 0:
                    activeList = rookieNamesList;
                    break;

                case 1:
                    //activeList = amateurList;
                    break;

                case 2:
                    //activeList = advancedList;
                    break;

                case 3:
                    //activeList = semiProList;
                    break;

                case 4:
                    //activeList = proList;
                    break;

                case 5:
                    //activeList = superProList;
                    break;

            }



        var uniqueRandomList = GetUniqueRandomElements(activeList, 6);

        for (int i = 0; i < uniqueRandomList.Count; i++)

        {
            MainManager.cars[i] = uniqueRandomList[i];
            

        }

        Debug.Log("First Car" + MainManager.cars[0]);
        
        PopulateCarCards();
            
    }

    void PopulateCarCards()

    {
        carAText.text = MainManager.cars[0];
        carBText.text = MainManager.cars[1];
        carCText.text = MainManager.cars[2];
        carDText.text = MainManager.cars[3];
        carEText.text = MainManager.cars[4];
        carFText.text = MainManager.cars[5];

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[0])

            { carPicA.image.sprite = rookieSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[1])

            { carPicB.image.sprite = rookieSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[2])

            { carPicC.image.sprite = rookieSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[3])

            { carPicD.image.sprite = rookieSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[4])

            { carPicE.image.sprite = rookieSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[5])

            { carPicF.image.sprite = rookieSpriteList[i]; }

        }
    }

    void TrackSelect()

    {
        
            var uniqueRandomList = GetUniqueRandomElements(trackList, 9);

                        
            for (int i = 0; i < uniqueRandomList.Count; i++)

            {
                MainManager.activeTracks[i] = uniqueRandomList[i];
            }


        int rand = Random.Range(0, bonusTrackList.Count);
        MainManager.bonusTrack = bonusTrackList[rand];



        PopulateTrackPanel();
    }

    void PopulateTrackPanel()

    {
        track1.text = MainManager.activeTracks[0];
        track2.text = MainManager.activeTracks[1];
        track3.text = MainManager.activeTracks[2];
        track4.text = MainManager.activeTracks[3];
        track5.text = MainManager.activeTracks[4];
        track6.text = MainManager.activeTracks[5];
        track7.text = MainManager.activeTracks[6];
        track8.text = MainManager.activeTracks[7];
        track9.text = MainManager.activeTracks[8];

        bonusTrack.text = MainManager.bonusTrack;

        enterPlayerNamesPanel.SetActive(true);


    }

    public void PopulatePlayerPanel()

    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        MainManager.playerNames[0] = p1NameInputField.text;
        MainManager.playerNames[1] = p2NameInputField.text;
        player1NameField.text = MainManager.playerNames[0];
        player2NameField.text = MainManager.playerNames[1];
        enterPlayerNamesPanel.SetActive(false);

        gameManagerScript.statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {13 - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");
    }


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
