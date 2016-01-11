using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int chickConditionAmt;
    public int chickenConditionAmt;
    public static int chickCurrentAmt;
    public static int chickenCurrentAmt;

    public bool hasTimer;
    public float MaxTimer;

    public static int AmountInField = 0;

    //  UI
    public Text TimerText;

    //  Audio
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        if (hasTimer)
        {
            StartCoroutine(StartTimer());
            //  UI
            TimerText.gameObject.SetActive(true);
        }
	}

    public void DecrementAmount()
    {
        if(AmountInField > 0)
            AmountInField--;
        if (AmountInField <= 0)
            CheckWinCondition(); 
    }

    void CheckWinCondition()
    {
        if(chickCurrentAmt >= chickConditionAmt && 
           chickenCurrentAmt >= chickenConditionAmt)
        {
            Time.timeScale = 0;
            Debug.Log("Win!");
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("Lose!");
        }    
    }

    IEnumerator StartTimer()
    {
        while(MaxTimer > 0)
        {
            yield return null;
            TimerText.text = MaxTimer.ToString("f1");
            MaxTimer -= Time.deltaTime; 
        }
        CheckWinCondition();
    }
}
