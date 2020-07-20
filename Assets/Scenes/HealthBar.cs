using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private static Slider playerOneHealthBar;
    private static Slider playerTwoHealthBar;
    private PlayerStats playerOneStats;
    private PlayerStats playerTwoStats;


    // Start is called before the first frame update
    void Start()
    {
        playerOneHealthBar = GameObject.Find("SliderOne").GetComponent<Slider>();
        playerTwoHealthBar = GameObject.Find("SliderTwo").GetComponent<Slider>();

        playerOneStats = GameObject.Find("Player1").GetComponent<PlayerStats>();
        playerTwoStats = GameObject.Find("Player2").GetComponent<PlayerStats>();


        // Se suman las vidas máximas con un offset para que se vea bien 
        playerOneHealthBar.maxValue = playerOneStats.maxHealth;
        playerTwoHealthBar.maxValue = playerTwoStats.maxHealth;

        playerOneHealthBar.value = playerOneStats.maxHealth;
        playerTwoHealthBar.value = playerTwoStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    static public void UpdatePlayerOneHealthBar(float damage)
    {
        playerOneHealthBar.value -= damage;
    }

    static public void UpdatePlayerTwoHealthBar(float damage)
    {
        playerTwoHealthBar.value -= damage;
    }


}
