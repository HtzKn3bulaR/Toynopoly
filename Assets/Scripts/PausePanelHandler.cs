using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;


public class PausePanelHandler : MonoBehaviour
{
    public static PausePanelHandler instance;

    [SerializeField] private Button hostOptions;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideGameHostOptionsForClient()
    {
        if(!NetworkManager.Singleton.IsHost)
        {
            hostOptions.gameObject.SetActive(false);            
        }
    }
        
}
