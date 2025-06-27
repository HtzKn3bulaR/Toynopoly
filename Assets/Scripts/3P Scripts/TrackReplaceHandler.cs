using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrackReplaceHandler : MonoBehaviour
{
    
    [SerializeField] Button[] trackButtons;
    [SerializeField] TextMeshProUGUI[] trackNamesDisplay;

    private GridGenerator3P gridGeneratorScript;

    [SerializeField] GameObject trackReplacePanel;
    int bonusTrackIndex = 9;
    int trackPosition;
    string newTrackName;


    // Start is called before the first frame update
    void Start()
    {
        gridGeneratorScript = GameObject.Find("GridGenerator3P").GetComponent<GridGenerator3P>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitiateReplaceTrackProcedure()
    {
        trackReplacePanel.gameObject.SetActive(true);

        for (int i = 0; i < MainManager.activeTracks.Length; i++)
        {
            trackNamesDisplay[i].text = MainManager.activeTracks[i];
        }

        trackNamesDisplay[bonusTrackIndex].text = MainManager.bonusTrack;

    }

    public void GetTrackPosition(int trackPos)
    {
        trackPosition = trackPos;

    }

    public void AssignNewTrackString(string newTrack)

    {
        newTrackName = newTrack;
    }


    public void ExecuteTrackReplace()
    {
        if (trackPosition != 9)
        {
            MainManager.activeTracks[trackPosition] = newTrackName;

        }

        else

        {
            MainManager.bonusTrack = newTrackName;
        }

        UpdateTrackPanel();
        trackReplacePanel.gameObject.SetActive(false);
    }


    private void UpdateTrackPanel()
    {
        gridGeneratorScript.track1.text = MainManager.activeTracks[0];
        gridGeneratorScript.track2.text = MainManager.activeTracks[1];
        gridGeneratorScript.track3.text = MainManager.activeTracks[2];
        gridGeneratorScript.track4.text = MainManager.activeTracks[3];
        gridGeneratorScript.track5.text = MainManager.activeTracks[4];
        gridGeneratorScript.track6.text = MainManager.activeTracks[5];
        gridGeneratorScript.track7.text = MainManager.activeTracks[6];
        gridGeneratorScript.track8.text = MainManager.activeTracks[7];
        gridGeneratorScript.track9.text = MainManager.activeTracks[8];

        gridGeneratorScript.bonusTrack.text = MainManager.bonusTrack;
    }

    public void TrackReplaceCancel()
    {
        trackReplacePanel.gameObject.SetActive(false);
    }

}


