using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PulsingEffect : MonoBehaviour
{
    public Color Color1;
    public Color Color2;

    public TextMeshProUGUI buttonText;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        FlashingText();

    }


    public void FlashingText()
    {

        buttonText.color = Color.Lerp(Color1, Color2, Mathf.PingPong(Time.time, 1));

    }
}
