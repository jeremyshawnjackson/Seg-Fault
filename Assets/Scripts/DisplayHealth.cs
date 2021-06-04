using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Redux;

public class DisplayHealth : MonoBehaviour
{
    private Image[] HealthPips;
    private int CurrentHealth;
    // private int MaxHealth;
    private PlayerController Player;

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();

        HealthPips = new Image[10];
        HealthPips[0] = GameObject.Find("Image").GetComponent<Image>();

        for (int i = 1; i < 10; i++)
        {
            var pip = GameObject.Find("Image (" + i + ")");
            HealthPips[i] = pip.GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = Player.GetHealth();
        // Debug.Log(CurrentHealth);
        for (int i = 0; i < HealthPips.Length; i++)
        {
            if (i < CurrentHealth)
            {
                HealthPips[i].enabled = true;
            }
            else
            {
                HealthPips[i].enabled = false;
            }
        }
    }
}
