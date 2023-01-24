using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{   float time;
    bool win;
    public float MaxPower;
    public Text Powerguage;
    public Text Timenum;
    public GameObject EndSCREEN;
    public GameObject WinSCREEN;

    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    private void Start()
    {
        time = AlienMovement.time;
    }
    public void Update()
    { win = DepositCheck.isWon;
        time = AlienMovement.time; 
        float fuelremain = MaxPower - time;
        Powerguage.text = "Power :"+fuelremain;
        Timenum.text = "With : " + time;
        if ( fuelremain<= 0 ) {
            EndSCREEN.SetActive(true);
        }
        if (win== true)
        {
            WinSCREEN.SetActive(true);
        }
        
        
    }
}
