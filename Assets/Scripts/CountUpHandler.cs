using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CountUpHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI [] carCounters;
    [SerializeField] float countDuration;
    public float currentValue;
    public float targetValue;
    Coroutine _C2T;

    [SerializeField] ParticleSystem[] CarCardParticles;
    [SerializeField] GameObject[] rows;



    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Start()
    {
        targetValue = currentValue;
    }

    IEnumerator CountTo(float currentValue, float targetValue)
    {
        var rate = Mathf.Abs(targetValue - currentValue) / countDuration;
        while (currentValue != targetValue)
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, rate * Time.deltaTime);

            if (MainManager.levelCounter == 1)
            {
                carCounters[MainManager.currentCarIndex].color = Color.HSVToRGB(0.39f, 1.0f, 1.0f);
                carCounters[MainManager.currentCarIndex].text = ((int)currentValue).ToString();
                
            }
            else if (MainManager.IsToynopolyBattle == false)
            {
                carCounters[MainManager.TimeBattleCarIndex].color = Color.HSVToRGB(0.39f, 1.0f, 1.0f);
                carCounters[MainManager.TimeBattleCarIndex].text = ((int)currentValue).ToString();
                
            }

            else
            {
                carCounters[MainManager.currentCarIndex].color = Color.HSVToRGB(0.39f, 1.0f, 1.0f);
                carCounters[MainManager.currentCarIndex].text = ((int)currentValue).ToString();
            }


            yield return null;
            
        }
        
        if( currentValue == targetValue)
        {
            carCounters[MainManager.TimeBattleCarIndex].color = Color.white;
            carCounters[MainManager.currentCarIndex].color = Color.white;
            carCounters[MainManager.currentCarIndex].fontSize = 41;
            carCounters[MainManager.TimeBattleCarIndex].fontSize = 41;
        }

        if (currentValue < 1)
        {
            if (MainManager.levelCounter > 1 && MainManager.IsToynopolyBattle == false)
            {
                carCounters[MainManager.TimeBattleCarIndex].color = Color.grey;
            }

            else

            carCounters[MainManager.currentCarIndex].color = Color.grey;
        }


    }

    public void AddValue(float old, float set)
    {
        if(set < 0)
        { set = 0; }


        if (MainManager.levelCounter == 1 && set - old > 9)
        {
            carCounters[MainManager.currentCarIndex].fontSize = 60;
            CarCardParticles[MainManager.currentCarIndex].Play();

            for (int i = 0; i < rows.Length; i++ )
            {
                rows[i].gameObject.SetActive(false);
            }


        }
        else if (MainManager.levelCounter == 2 && set - old > 19 && MainManager.IsToynopolyBattle == true && set > 0)
        {
            carCounters[MainManager.currentCarIndex].fontSize = 60;
            CarCardParticles[MainManager.currentCarIndex].Play();

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].gameObject.SetActive(false);
            }
        }

        else if (MainManager.levelCounter == 2 && set - old > 19 && MainManager.IsToynopolyBattle == false) 
        {
            carCounters[MainManager.TimeBattleCarIndex].fontSize = 60;
            CarCardParticles[MainManager.TimeBattleCarIndex].Play();

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].gameObject.SetActive(false);
            }
        }

        else if (MainManager.levelCounter == 2)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i].gameObject.SetActive(false);
            }
        }


        targetValue = set;
        currentValue = old;
        if (_C2T != null)
            StopCoroutine(_C2T);
        _C2T = StartCoroutine(CountTo(currentValue, targetValue));
                

    }

    

}
