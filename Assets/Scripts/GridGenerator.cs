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

    [SerializeField] GameObject carCarrousel;

    public AudioClip carPopulateSound;
    public AudioClip stageReady;
    public AudioClip transition;
    public AudioClip newGame;
    public AudioSource gameSounds;


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
    [SerializeField] TextMeshProUGUI player3NameField;

    //public TextMeshProUGUI p1NameInputField;
    //public TextMeshProUGUI p2NameInputField;
    //public TextMeshProUGUI p3NameInputField;


    [SerializeField] Button carASprite;
    [SerializeField] Button carBSprite;
    [SerializeField] Button carCSprite;
    [SerializeField] Button carDSprite;
    [SerializeField] Button carESprite;
    [SerializeField] Button carFSprite;

    [SerializeField] Button carrouselSprite;

    //[SerializeField] Button playerNamesSubmit;

    [SerializeField] GameObject gameStartingPanel;

    private GameManager gameManagerScript;

    private Animator carAPresentation;
    private Animator carBPresentation;
    private Animator carCPresentation;
    private Animator carDPresentation;
    private Animator carEPresentation;
    private Animator carFPresentation;




    private int carCardsPopulated = 0;

    private int rowShown = 0;

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
    List<Sprite> activeSpriteList;

    List<string> rookieNamesList = new List<string>

    {
        "Toukka 4x4","Starfire GT","Lancer","El Gekko","Condor GRV","Junky","Rouge","Get Air","BigVolt","Road Star","Sunset Light","Show-Off","Nimbus","Harvester","Rebound 4x4","Albatross GT",
        "Updraft","Chubble","El Rapido","Vaanbus","Kanberra Kruiser","Blobster","Col. Moss","Angus 400","Nesbitt","Hot Spot","Micro","Phat Slug","Hurricane","LR 64","Super Wheat","Dust Mite","High-Rod",
        "Crazy Pat","Myrmech","Mr. Bedtime","Tesla","Funziona","Phat Trucker","Splat","Panorama","Ciagnik","Genghis Kar","Quaqa Turbo","Volken Turbo","HSF-1","Pipsqueak","Naranja Turbo","RC Phink","E-Razr", "FunkFlea"
    };

    public List<Sprite> rookieSpriteList = new List<Sprite>

    { };

    List<string> amateurNamesList = new List<string>

    {
        "AMCO TC","Bad Bison","Badd RC","Baja Dash","Breadfast","Bumblebee","Candy Pebbles","Dr. Grudge","Eatium","Emilia","Evil Weasel","Exceed","Flatter 4V","Frograph","Groovster","Harmor","Honeybee",
        "Hotknife","Ignit-9","Koin Karp","Kyarus","LA 54","LMW","Locker","Madness","Manfred","Moby Trick","Mongoose","Mouse","Muller GT","Nevermore","Nitro Crusher","NY 54","Off Gear","Phantum","Power Cap",
        "Queen Bee","RC Bandit","Reddlum","Red Kermit","Reliance","Road King","Rotor","RV Loco","Silvarooky","Smokie","Sprinter XL","Star Carbs","Strax","Tempest","Toy-Volt Towing","Triton","Ultima","Ultra Gamma",
        "Vixen","Wild Ride"

    };

    public List<Sprite> amateurSpriteList = new List<Sprite>

    { };

    List<string> advancedNamesList = new List<string>

    {
        "75C","Aerozad","Akagi Attacker","Alice","APC L-13","Aquamarina","Aquasonic","Bajaette","Bendor","Bertha Ballistics","BossVolt","Breaker","Camelia","Cerveth","Donnie TC","Drawall","DRJ-61","Duck Sky",
        "Ember","Emperor","Flower Power","Frostbite","Frosted Delight","Fulon X","Grimlock","Hammerhead","Hyper XL","Junker","Le Pastel","Lithmus","Llag Sat","Marauder","Matra XL","Micro Tache","Panga TC","Panther",
        "Pest Control","Phenom","Pole Poz","Prizmer","R6 Turbo","Raudy","RC 1999","RC San","Recon MK1","Rice Ball","Road Rage","Romeo","Sarge","Shocker","Spearhead","Springtrap","ST 1","Sturm","Sunnyboy","Swizz Cheezer",
        "Thunder","Twyster","Unicorn","Urban Jungle","Vibe Box","Wave Dancer","Weaver","Whiplash","YNZ"
    };

    public List<Sprite> advancedSpriteList = new List<Sprite>

    { };

    List<string> semiProNamesList = new List<string>

    {
        "5TP","Acclaim GT","Adeon","Aeromaster","AMW","Ancile","Arnoux","Artifact","BHV 1","Big Load","Big Match Jim","Blazar","Blaze V8","Bushido RS","CHC 305","Chubba","Cobra Max","Cossie","Danger","Dragoon",
        "Dual Signal","Ducktail","Fat Agnus","Gravel Basher","Heritage","Hydrox","Iron-Z","Jackal","Jet Astro","JG-7","Jungle Beast","Karlington","KC-3","Locust","LV 54","Mambra","Maxam RS","Max Attack","Middle5",
        "Nitromare","Norwood","Ontogen","Oval Ace","Pemto","Predator","Quazar","RC-Erra","RC Vector","RG1","Riptor","Rothams Racing","Runner 2000","Rustique","Sasquatch","Serrate","Sizzler","Sokudo","Speed Balancer","Styn",
        "Swede","Touga", "Tri-Enter","Tribute","Trundle Buster","Victoria","Voltz XL","Winger","Yuurei V8","Zipper"
     };

    public List<Sprite> semiProSpriteList = new List<Sprite>

    { };

    List<string> proNamesList = new List<string>

    {
        "After Image","Artair","Ayrton SP","BajaVolt","BanKing","Black Widow","Cerberus","Cherencov","Chimera TC","Cintach","Cougar","Drome Champ","Duflame","Eaglet","Electric Sheep","EXE TC","G3X","Gust","Humma"
        ,"Hydro Flame","Indy B","Jet Spike","Karen","Keyakizaka","Maverick","Mean Streak","Mid-Musc","N-Sharp","Outlaw","Panga","Patriot","Power Loader","Prime Target","Probe-24","Proto Combo","Puma","Purp XL",
        "Quinx","RC Bulldog","RC Winglet","Redhead","Ridgeback","RVRC 20","RVXXL 5","Ryu","S13 Alltune","Sandstorm","Shark Bite","Shinobi","Sin","Sir Gleam","SNW 35","Spectrum","SR-8000","Star Oil","Steezy",
        "Sucker Punch","Sunrise","The Knight","Tier 15","Toro GT84","Toyeca","TT Raider","Ultra Drive","Velter Ultron","Visconti R","Vitesse","Warrion","Wattage","Wildstar"

    };

    public List<Sprite> proSpriteList = new List<Sprite>

    { };

    List<string> superProNamesList = new List<string>

    {
        "Anaconda GT","Armand","AU-8","Calcure","Cambold R","Commandine","Daemmon","Dragheat","Elyta","Endo","Exclaim GT Mk.2","FD-400","FLIR","Golden Eye","Gungnir","Hanabira","Hemera","Hetgarde GT1","Horizenna",
        "Identity X","King Kaiju","King Moloko","Komet","La Rossa","Madax GT","Maxxas XLR8","Megalodon XL","Mudman","Nakajima","Napalm","Nyx","Orbitron","Orion","P4 Super","Prototype FX77","Quicksilver","Raven",
        "Reiser","Revel","Rinne","RVRC","Saeger","Selsia Turbo","Sentaro XL","Sideswipe","Skarlet","Skull Crusher","Slingshot","Spectron","Starmac","Sterling F77","Stinger","Sylea","Tesseract","Ultra-RV","U.V.G.S.",
        "Voltrex","Wind Slicer","Wyvern","XM250","Yinisa"
    };

    public List<Sprite> superProSpriteList = new List<Sprite>

    { };




    List<T> GetUniqueRandomElements<T>(List<T> inputList, int count)

    {
        List<T> inputListClone = new List<T>(inputList);
        Shuffle(inputListClone);
        return inputListClone.GetRange(0, count);

    }

    void Awake()
    {

        if (MainManager.gameResumed)
        {
            gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

            PopulateCarCards();
            PopulateTrackPanel();
            player1NameField.text = MainManager.playerNames[0];
            player2NameField.text = MainManager.playerNames[1];
            carCarrousel.SetActive(false);

            foreach (GameObject row in gameManagerScript.rows)
            {
                row.SetActive(true);
            }
            
            
            for (int i = 0; i < MainManager.fieldAvailable.Length; i++)
            {
                if (!MainManager.fieldAvailable[i])
                {
                    gameManagerScript.fields[i].gameObject.SetActive(false);
                }
            }

            gameSounds = GetComponent<AudioSource>();
        }
        else
        {

            TrackSelect();

            gameSounds = GetComponent<AudioSource>();

            carAPresentation = carPicA.GetComponent<Animator>();
            carBPresentation = carPicB.GetComponent<Animator>();
            carCPresentation = carPicC.GetComponent<Animator>();
            carDPresentation = carPicD.GetComponent<Animator>();
            carEPresentation = carPicE.GetComponent<Animator>();
            carFPresentation = carPicF.GetComponent<Animator>();

            CarSelect();

        }
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
                activeSpriteList = rookieSpriteList;
                break;

            case 1:
                activeList = amateurNamesList;
                activeSpriteList = amateurSpriteList;
                break;

            case 2:
                activeList = advancedNamesList;
                activeSpriteList = advancedSpriteList;
                break;

            case 3:
                activeList = semiProNamesList;
                activeSpriteList = semiProSpriteList;
                break;

            case 4:
                activeList = proNamesList;
                activeSpriteList = proSpriteList;
                break;

            case 5:
                activeList = superProNamesList;
                activeSpriteList = superProSpriteList;
                break;

        }



        var uniqueRandomList = GetUniqueRandomElements(activeList, 6);

        for (int i = 0; i < uniqueRandomList.Count; i++)

        {
            MainManager.cars[i] = uniqueRandomList[i];


        }

        Debug.Log("First Car" + MainManager.cars[0]);

        StartCarrousel();


        StartCoroutine(FirstCarPresentationDelay());


    }

    //populate without delay:

    void PopulateCarCards()

    {
        switch (MainManager.classSelected)

        {
            case 0:
                activeList = rookieNamesList;
                activeSpriteList = rookieSpriteList;
                break;

            case 1:
                activeList = amateurNamesList;
                activeSpriteList = amateurSpriteList;
                break;

            case 2:
                activeList = advancedNamesList;
                activeSpriteList = advancedSpriteList;
                break;

            case 3:
                activeList = semiProNamesList;
                activeSpriteList = semiProSpriteList;
                break;

            case 4:
                activeList = proNamesList;
                activeSpriteList = proSpriteList;
                break;

            case 5:
                activeList = superProNamesList;
                activeSpriteList = superProSpriteList;
                break;

        }


        carAText.text = MainManager.cars[0];
        carBText.text = MainManager.cars[1];
        carCText.text = MainManager.cars[2];
        carDText.text = MainManager.cars[3];
        carEText.text = MainManager.cars[4];
        carFText.text = MainManager.cars[5];

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[0])

            { carPicA.image.sprite = activeSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[1])

            { carPicB.image.sprite = activeSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[2])

            { carPicC.image.sprite = activeSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[3])

            { carPicD.image.sprite = activeSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[4])

            { carPicE.image.sprite = activeSpriteList[i]; }

            else if (activeList[i] == MainManager.cars[5])

            { carPicF.image.sprite = activeSpriteList[i]; }

        }
    }

    void StartCarrousel()

    {
        carCarrousel.SetActive(true);
        



        InvokeRepeating("ChangePicture", 1.0f, 0.2f);



    }

    void ChangePicture()

    {

        if (carCardsPopulated < 6)

        {

            carrouselSprite.image.sprite = activeSpriteList[Random.Range(0, activeSpriteList.Count)];

        }

        else
        {
            carCarrousel.SetActive(false);
        }

    }


    IEnumerator FirstCarPresentationDelay()

    {
        yield return new WaitForSeconds(5.0f);

        PopulateCarCardA();

    }


    void PopulateCarCardA()

    {


        carAText.text = MainManager.cars[0];
        gameSounds.PlayOneShot(carPopulateSound);
        carAPresentation.SetTrigger("PresentCarA");

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[0])

            { carPicA.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());
    }

    void PopulateCarCardB()

    {
        carBText.text = MainManager.cars[1];
        gameSounds.PlayOneShot(carPopulateSound);
        carBPresentation.SetTrigger("PresentCarB");

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[1])

            { carPicB.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());
    }

    void PopulateCarCardC()
    {
        carCText.text = MainManager.cars[2];
        gameSounds.PlayOneShot(carPopulateSound);
        carCPresentation.SetTrigger("PresentCarC");

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[2])

            { carPicC.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());
    }

    void PopulateCarCardD()
    {
        carDText.text = MainManager.cars[3];
        gameSounds.PlayOneShot(carPopulateSound);

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[3])

            { carPicD.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());
        carDPresentation.SetTrigger("PresentCarD");

    }

    void PopulateCarCardE()

    {
        carEText.text = MainManager.cars[4];
        gameSounds.PlayOneShot(carPopulateSound);

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[4])

            { carPicE.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());
        carEPresentation.SetTrigger("PresentCarE");

    }

    void PopulateCarCardF()

    {
        carFText.text = MainManager.cars[5];
        gameSounds.PlayOneShot(carPopulateSound);

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[5])

            { carPicF.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());
        carFPresentation.SetTrigger("PresentCarF");
        //gameSounds.PlayOneShot(stageReady);

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


    }

    public void PopulatePlayerPanel()

    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        switch (MainManager.playerNumber)

        {
            case 2:
                
                player1NameField.text = MainManager.playerNames[0];
                player2NameField.text = MainManager.playerNames[1];
                break;

            case 3:
                
                player1NameField.text = MainManager.playerNames[0];
                player2NameField.text = MainManager.playerNames[1];
                player3NameField.text = MainManager.playerNames[2];
                break;

        }

        StartCoroutine(WaitAfterLineupSelected());
               

        gameManagerScript.statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {13 - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");
    }

    void ShowNextRow()
    {
        if (rowShown < 6)

        {
            gameManagerScript.rows[rowShown].SetActive(true);

            StartCoroutine(FieldsAppearingDelay());
        }

    }


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TablePopulateDelayRoutine()

    {
        yield return new WaitForSeconds(6.0f);

        switch (carCardsPopulated)

        {
            case 1:
                PopulateCarCardB();
                break;
            case 2:
                PopulateCarCardC();
                break;
            case 3:
                PopulateCarCardD();
                break;
            case 4:
                PopulateCarCardE();
                break;
            case 5:
                PopulateCarCardF();
                break;
            default:
                gameStartingPanel.SetActive(true);
                PopulatePlayerPanel();
                break;
        }
    }

    IEnumerator FieldsAppearingDelay()

    {
        yield return new WaitForSeconds(0.8f);

        rowShown++;
        ShowNextRow();
    }

    IEnumerator WaitAfterLineupSelected()

    {
        yield return new WaitForSeconds(6.0f);
        gameStartingPanel.SetActive(false);

        gameSounds.PlayOneShot(transition);
        gameManagerScript.helpText.gameObject.SetActive(true);

        ShowNextRow();
    }

}
