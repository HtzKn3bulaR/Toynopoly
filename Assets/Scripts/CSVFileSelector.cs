using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CSVFileSelector : MonoBehaviour
{
    
    public static CSVFileSelector Instance { get; private set; }
    
    [SerializeField] TMP_Dropdown fileDropdown;
    [SerializeField] TextMeshProUGUI rvglFolder;
    [SerializeField] TextMeshProUGUI gameFolderPlaceholder;
    public List<string> csvFiles = new List<string>();
    public string fileName;
    public string selectedFilePath;
    private string applicationDataPath;
    //private string defaultFolderName;

    private CSVFileReader fileReaderScript;
   
    
    
    public class ResultsSet
    {
        public static string player;
        public static string car;
        public static string time;

    }

    void Start()
    {         
        Instance = this;         
        
        applicationDataPath = MainManager.gameFolder + "/profiles/";

        GetAllCSVFiles();
    }

    
    void Update()
    {
        
    }

    public string ReturnGameFolder()
    {
        return MainManager.gameFolder;
    }
       

    public void SetNewMainFolder(string folder)
    {
        string newFolder = folder;
        MainManager.gameFolder = newFolder;
        Debug.Log(MainManager.gameFolder + "set as New Game Folder");
        applicationDataPath = MainManager.gameFolder + "/profiles/";
        Debug.Log(applicationDataPath + "set as Application Path");
        gameFolderPlaceholder.text = MainManager.gameFolder;
        GetAllCSVFiles();
    }


    public void GetAllCSVFiles()
    {
        csvFiles.Clear();
                
        try
        {
            csvFiles.Add("Select the Log File for your session");

            string[] files = Directory.GetFiles(applicationDataPath, "*csv");

            foreach (string file in files)
            {
                csvFiles.Add(Path.GetFileName(file));
            }

            if (files.Length == 0)
            {
                fileDropdown.interactable = false;
            }
        }

        catch (UnassignedReferenceException)
        {
            Debug.LogError("Access denied to some directories");
            return;
            
        }

        catch (Exception e)
        {
            fileDropdown.ClearOptions();
            Debug.LogError($"An error occured while accessing files: {e.Message}");
            return;                        
        }

        SetDropdownOptions();

    }


    private void SetDropdownOptions()
    {
        fileDropdown.ClearOptions();
        fileDropdown.AddOptions(csvFiles);

        if (fileDropdown.options.Count > 1)
        {
            fileDropdown.interactable = true;
        }
    }
        
    public void OnCSVSelected()
    {
        if (fileDropdown.value != 0)
        {
            fileName = fileDropdown.options[fileDropdown.value].text;
            selectedFilePath = applicationDataPath + fileName;
        }

        else
        {
            //fileName = "";
            //selectedFilePath = "";
        }

        if (selectedFilePath != null)
        {
            MainManager.selectedFilePath = selectedFilePath;

            Debug.Log("Load Session File:" + applicationDataPath + fileName);

            
            
        }

       

    }

    

    


}
