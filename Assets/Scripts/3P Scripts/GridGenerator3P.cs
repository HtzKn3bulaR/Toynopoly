using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class GridGenerator3P : MonoBehaviour

{

    public Button carPicA;
    public Button carPicB;
    public Button carPicC;
    public Button carPicD;
    public Button carPicE;
    public Button carPicF;

    public AudioClip carPopulateSound;
    public AudioClip stageReady;
    public AudioClip transition;
    public AudioSource gameSounds;

    public ParticleSystem bliss;
    

    [SerializeField] GameObject carCarrousel;

    public TextMeshProUGUI track1;
    public TextMeshProUGUI track2;
    public TextMeshProUGUI track3;
    public TextMeshProUGUI track4;
    public TextMeshProUGUI track5;
    public TextMeshProUGUI track6;
    public TextMeshProUGUI track7;
    public TextMeshProUGUI track8;
    public TextMeshProUGUI track9;

    public TextMeshProUGUI bonusTrack;

    public TextMeshProUGUI carAText;
    public TextMeshProUGUI carBText;
    public TextMeshProUGUI carCText;
    public TextMeshProUGUI carDText;
    public TextMeshProUGUI carEText;
    public TextMeshProUGUI carFText;

    public TextMeshProUGUI player1NameField;
    public TextMeshProUGUI player2NameField;
    public TextMeshProUGUI player3NameField;
    public TextMeshProUGUI player4NameField;
    public TextMeshProUGUI player5NameField;
       

    public Button carASprite;
    public Button carBSprite;
    public Button carCSprite;
    public Button carDSprite;
    public Button carESprite;
    public Button carFSprite;

    [SerializeField] Button carrouselSprite;
        

    [SerializeField] GameObject gameStartingPanel;
    [SerializeField] GameObject timerPanel;

    private PlayerManager3P gameManagerScript;
    private Timer timerScript;

    private Animator carAPresentation;
    private Animator carBPresentation;
    private Animator carCPresentation;
    private Animator carDPresentation;
    private Animator carEPresentation;
    private Animator carFPresentation;

    private int rowShown = 0;

    private int carCardsPopulated = 0;

    public TextAsset standardTrackNames;
    public TextAsset bonusTrackNames;

    public TextAsset rookieNames;
    public TextAsset amateurNames;
    public TextAsset advancedNames;
    public TextAsset semiProNames;
    public TextAsset proNames;
    public TextAsset superProNames;


    List<string> trackList = new List<string>();

    List<string> bonusTrackList = new List<string>();

    public static event Action OnTrackPanelPopulate;
    public static event Action OnCarAPopulate;
    public static event Action OnCarBPopulate;
    public static event Action OnCarCPopulate;
    public static event Action OnCarDPopulate;
    public static event Action OnCarEPopulate;
    public static event Action OnCarFPopulate;

    public event EventHandler<OnCarCardPopulateEventArgs> OnCarCardPopulate;

    public class OnCarCardPopulateEventArgs : EventArgs
    {
        public TextMeshProUGUI carCard;
    }
        

    public List<string> activeList;
    public List<Sprite> activeSpriteList;

    List<string> rookieNamesList = new List<string>();

    

    public List<Sprite> rookieSpriteList = new List<Sprite>

    { };

    List<string> amateurNamesList = new List<string>();

    

    public List<Sprite> amateurSpriteList = new List<Sprite>

    { };

    List<string> advancedNamesList = new List<string>();

    

    public List<Sprite> advancedSpriteList = new List<Sprite>

    { };

    List<string> semiProNamesList = new List<string>();

    

    public List<Sprite> semiProSpriteList = new List<Sprite>

    { };

    List<string> proNamesList = new List<string>();

    

    public List<Sprite> proSpriteList = new List<Sprite>

    { };


    List<string> superProNamesList = new List<string>();

    

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

        ReadTrackLists();
        ReadCarLists();


        if (MainManager.gameResumed)
        {
            gameManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();
            timerScript = GameObject.Find("Timer").GetComponent<Timer>();

            PopulateCarCards();
            PopulateTrackPanel();
            player1NameField.text = MainManager.playerNames[0];
            player2NameField.text = MainManager.playerNames[1];
            player3NameField.text = MainManager.playerNames[2];

            if (MainManager.playerNumber > 3)
            { player4NameField.text = MainManager.playerNames[3]; }

            if (MainManager.playerNumber > 4)
            { player5NameField.text = MainManager.playerNames[4]; }

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


    void ReadTrackLists()
    {

        string[] standardData = standardTrackNames.text.Split(new string[] { "\n" }, StringSplitOptions.None);

        string[] bonusData = bonusTrackNames.text.Split(new string[] { "\n" }, StringSplitOptions.None);

        int tableSize = standardData.Length;

        for (int i = 0; i < tableSize; i++)
        {
            string nameTrimmed;

            nameTrimmed = standardData[i].TrimEnd(new char[] { '\r', ' ' });
            nameTrimmed = nameTrimmed.TrimStart(new char[] { '\r', ' ' });

            trackList.Add(nameTrimmed);
                        
        }

        for (int i = 0; i < tableSize; i++)
        {

            string nameTrimmed;

            nameTrimmed = bonusData[i].TrimEnd(new char[] { '\r', ' ' });
            nameTrimmed = nameTrimmed.TrimStart(new char[] { '\r', ' ' });

            bonusTrackList.Add(nameTrimmed);
        }


    }


    void ReadCarLists()
    {

        switch (MainManager.classSelected)
        {
            case 0:

                string[] rookieData = rookieNames.text.Split(new string[] { "\n" }, StringSplitOptions.None);

                int rookieCount = rookieData.Length;

                string nameTrimmed;

                foreach (string s in rookieData)
                {
                    nameTrimmed = s.TrimEnd(new char[] { '\r', ' ' });
                    nameTrimmed = nameTrimmed.TrimStart(new char[] { '\r', ' ' });
                    rookieNamesList.Add(nameTrimmed);
                }
                break;

            case 1:

                string[] amateurData = amateurNames.text.Split(new string[] { "\n" }, StringSplitOptions.None);

                int amateurCount = amateurData.Length;

                string nameTrimmedAm;

                foreach (string s in amateurData)
                {
                    nameTrimmedAm = s.TrimEnd(new char[] { '\r', ' ' });
                    nameTrimmedAm = nameTrimmedAm.TrimStart(new char[] { '\r', ' ' });
                    amateurNamesList.Add(nameTrimmedAm);
                }

                break;

            case 2:

                string[] advancedData = advancedNames.text.Split(new string[] { "\n" }, StringSplitOptions.None);

                int advancedCount = advancedData.Length;

                string nameTrimmedAd;

                foreach (string s in advancedData)
                {
                    nameTrimmedAd = s.TrimEnd(new char[] { '\r', ' ' });
                    nameTrimmedAd = nameTrimmedAd.TrimStart(new char[] { '\r', ' ' });
                    advancedNamesList.Add(nameTrimmedAd);
                }

                break;


            case 3:

                string[] semiProData = semiProNames.text.Split(new string[] { "\n" }, StringSplitOptions.None);

                int semiProCount = semiProData.Length;

                string nameTrimmedSe;

                foreach (string s in semiProData)
                {
                    nameTrimmedSe = s.TrimEnd(new char[] { '\r', ' ' });
                    nameTrimmedSe = nameTrimmedSe.TrimStart(new char[] { '\r', ' ' });
                    semiProNamesList.Add(nameTrimmedSe);
                }

                break;

            case 4:

                string[] proData = proNames.text.Split(new string[] { "\n" }, StringSplitOptions.None);

                int ProCount = proData.Length;

                string nameTrimmedP;

                foreach (string s in proData)
                {
                    nameTrimmedP = s.TrimEnd(new char[] { '\r', ' ' });
                    nameTrimmedP = nameTrimmedP.TrimStart(new char[] { '\r', ' ' });
                    proNamesList.Add(nameTrimmedP);
                }

                break;


            case 5:

                string[] superProData = superProNames.text.Split(new string[] { "\n" }, StringSplitOptions.None);

                int superProCount = superProData.Length;

                string nameTrimmedS;

                foreach (string s in superProData)
                {
                    nameTrimmedS = s.TrimEnd(new char[] { '\r', ' ' });
                    nameTrimmedS = nameTrimmedS.TrimStart(new char[] { '\r', ' ' });
                    superProNamesList.Add(nameTrimmedS);
                }

                break;

        }
    }




    void Shuffle<T>(List<T> inputList)

    {
        for (int i = 0; i < inputList.Count - 1; i++)

        {
            T temp = inputList[i];
            int rand = UnityEngine.Random.Range(i, inputList.Count);
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


    void StartCarrousel()

    {
        bliss.Play();


        carCarrousel.SetActive(true);



        InvokeRepeating("ChangePicture", 1.0f, 0.2f);



    }

    void ChangePicture()

    {

        if (carCardsPopulated < 6)

        {

            carrouselSprite.image.sprite = activeSpriteList[UnityEngine.Random.Range(0, activeSpriteList.Count)];

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

    void PopulateCarCardA()
    {
        
        carAText.text = MainManager.cars[0];
        OnCarCardPopulate?.Invoke(this, new OnCarCardPopulateEventArgs { carCard = carAText });
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
        OnCarCardPopulate?.Invoke(this, new OnCarCardPopulateEventArgs { carCard = carBText });
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
        OnCarCardPopulate?.Invoke(this, new OnCarCardPopulateEventArgs { carCard = carCText });
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
        OnCarCardPopulate?.Invoke(this, new OnCarCardPopulateEventArgs { carCard = carDText });
        gameSounds.PlayOneShot(carPopulateSound);
        carDPresentation.SetTrigger("PresentCarD");

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[3])

            { carPicD.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());

    }

    void PopulateCarCardE()

    {
        carEText.text = MainManager.cars[4];
        //OnCarCardPopulate?.Invoke(this, new OnCarCardPopulateEventArgs { carCard = carEText });
        gameSounds.PlayOneShot(carPopulateSound);
        carEPresentation.SetTrigger("PresentCarE");

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[4])

            { carPicE.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());

    }

    void PopulateCarCardF()

    {
        carFText.text = MainManager.cars[5];
        OnCarCardPopulate?.Invoke(this, new OnCarCardPopulateEventArgs { carCard = carFText });
        gameSounds.PlayOneShot(carPopulateSound);
        

        for (int i = 0; i < activeList.Count; i++)

        {
            if (activeList[i] == MainManager.cars[5])

            { carPicF.image.sprite = activeSpriteList[i]; }

        }

        carCardsPopulated++;
        StartCoroutine(TablePopulateDelayRoutine());
        carFPresentation.SetTrigger("PresentCarF");


    }





    void TrackSelect()

    {

        var uniqueRandomList = GetUniqueRandomElements(trackList, 9);


        for (int i = 0; i < uniqueRandomList.Count; i++)

        {
            MainManager.activeTracks[i] = uniqueRandomList[i];

        }


        int rand = UnityEngine.Random.Range(0, bonusTrackList.Count);
        MainManager.bonusTrack = bonusTrackList[rand];



        //PopulateTrackPanel();
    }

    void PopulateTrackPanel()

    {
        OnTrackPanelPopulate?.Invoke();

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
        gameManagerScript = GameObject.Find("PlayerManager3P").GetComponent<PlayerManager3P>();

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

            case 4:
                
                player1NameField.text = MainManager.playerNames[0];
                player2NameField.text = MainManager.playerNames[1];
                player3NameField.text = MainManager.playerNames[2];
                player4NameField.text = MainManager.playerNames[3];
                break;

            case 5:

                player1NameField.text = MainManager.playerNames[0];
                player2NameField.text = MainManager.playerNames[1];
                player3NameField.text = MainManager.playerNames[2];
                player4NameField.text = MainManager.playerNames[3];
                player5NameField.text = MainManager.playerNames[4];
                break;

        }

        StartCoroutine(WaitAfterLineupSelected());
                

        if (MainManager.playerNumber == 5)
        {
            gameManagerScript.statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {MainManager.raceThreshold - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");
        }
        else
        {
            gameManagerScript.statusInfoTextBar.text = ($"Active Player is {MainManager.playerNames[MainManager.activePlayer]} / Level: {MainManager.levelCounter} / Races remaining: {MainManager.raceThreshold - MainManager.roundCounter} / Races completed: {MainManager.roundCounter - 1}");
        }
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
                PopulateTrackPanel();
                bliss.Stop();
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

        
        timerPanel.gameObject.SetActive(true);
        


        ShowNextRow();
    }


}