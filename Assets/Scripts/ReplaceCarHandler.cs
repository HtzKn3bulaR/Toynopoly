using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ReplaceCarHandler : MonoBehaviour
{

    private GridGenerator gridGeneratorScript;
    
    private string newCar;
    private string searchString;

    [SerializeField] TextMeshProUGUI newCarNameField;
    [SerializeField] TextMeshProUGUI notFound;
    [SerializeField] TextMeshProUGUI replaceUnavailableMessage;
    [SerializeField] TextMeshProUGUI[] carReplaceOldLinupDisplays;

    [SerializeField] GameObject carReplaceDialoguePanel;

    [SerializeField] Button newCarSprite;
    [SerializeField] Button submitNewCar;
    [SerializeField] Button[] carReplaceOldLineupNames;
    [SerializeField] Button searchButton;

    private int newCarIndex;
    private int newCarPosition;



    // Start is called before the first frame update
    void Start()
    {
        gridGeneratorScript = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitiateReplaceProcedure()
    {
        carReplaceDialoguePanel.gameObject.SetActive(true);

        for (int i = 0; i < MainManager.cars.Length; i++)

        {
            carReplaceOldLinupDisplays[i].text = MainManager.cars[i];

        }


        if (MainManager.roundCounter > 1)

        {
            replaceUnavailableMessage.gameObject.SetActive(true);
            newCarSprite.gameObject.SetActive(false);
            searchButton.gameObject.SetActive(false);

        }


    }


    public void AssignStringVariable(string field)
    {
        searchString = field;
        Debug.Log(field);
    }


    public void GetListIndex(int carPosition)
    {
        newCarPosition = carPosition;
                           
    }


    public void SearchList()

    {
                
        Debug.Log(searchString);

        newCarSprite.gameObject.SetActive(true);
        submitNewCar.gameObject.SetActive(true);


        if (gridGeneratorScript.activeList.Contains(searchString))

        {
            notFound.gameObject.SetActive(false);

            newCarIndex = gridGeneratorScript.activeList.IndexOf(searchString);
                     
            newCarSprite.image.sprite = gridGeneratorScript.activeSpriteList[newCarIndex];
        }

        else
        {
            notFound.gameObject.SetActive(true);
            newCarSprite.gameObject.SetActive(false);
            submitNewCar.gameObject.SetActive(false);
        }

    }

    public void ExecuteCarReplace()
        {

        MainManager.cars[newCarPosition] = gridGeneratorScript.activeList[newCarIndex];
        
        switch(newCarPosition)
        {
            case 0:
                gridGeneratorScript.carAText.text = MainManager.cars[0];
                gridGeneratorScript.carPicA.image.sprite = gridGeneratorScript.activeSpriteList[newCarIndex];
                break;

            case 1:
                gridGeneratorScript.carBText.text = MainManager.cars[1];
                gridGeneratorScript.carPicB.image.sprite = gridGeneratorScript.activeSpriteList[newCarIndex];
                break;

            case 2:
                gridGeneratorScript.carCText.text = MainManager.cars[2];
                gridGeneratorScript.carPicC.image.sprite = gridGeneratorScript.activeSpriteList[newCarIndex];
                break;

            case 3:
                gridGeneratorScript.carDText.text = MainManager.cars[3];
                gridGeneratorScript.carPicD.image.sprite = gridGeneratorScript.activeSpriteList[newCarIndex];
                break;

            case 4:
                gridGeneratorScript.carEText.text = MainManager.cars[4];
                gridGeneratorScript.carPicE.image.sprite = gridGeneratorScript.activeSpriteList[newCarIndex];
                break;

            case 5:
                gridGeneratorScript.carFText.text = MainManager.cars[5];
                gridGeneratorScript.carPicF.image.sprite = gridGeneratorScript.activeSpriteList[newCarIndex];
                break;
         }

        carReplaceDialoguePanel.SetActive(false);

    }


    public void CarReplaceCancel()
    {
        carReplaceDialoguePanel.gameObject.SetActive(false);
        notFound.gameObject.SetActive(false);
    }


             
               
                
               
}
