using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CSVFileSelector : MonoBehaviour
{
    [SerializeField] TMP_Dropdown fileDropdown;
    [SerializeField] TextMeshProUGUI rvglFolder;
    public List<string> csvFiles = new List<string>();
    public string fileName;
    public string selectedFilePath;
    private string applicationDataPath;
    private string defaultFolderName = "D:/Re-Volt";

    private CSVFileReader fileReaderScript;
   
    
    
    public class ResultsSet
    {
        public static string player;
        public static string car;
        public static string time;

    }

    void Start()
    {
                
        applicationDataPath = defaultFolderName + "/profiles/";

        GetAllCSVFiles();

    }

    
    void Update()
    {
        
    }

    public void SetNewMainFolder()
    {
        defaultFolderName = rvglFolder.text;
    }


    public void GetAllCSVFiles()
    {
        csvFiles.Clear();

        //selectedFilePath = "";

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
        }

        catch (Exception e)
        {
            Debug.LogError($"An error occured while accessing files: {e.Message}");
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
