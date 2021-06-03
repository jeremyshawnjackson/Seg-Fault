using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountdownTimer : MonoBehaviour {
    public float TimeStart = 5;
    public GameObject TimerUI;
    public Text TextBox;
// 	void Start () {
// 		TimerUI.SetActive(true);
//         TextBox.text = TimeStart.ToString();
// 	}
	
// 	void Update () {
//         TimeStart -= Time.deltaTime;
		
// 		TimeSpan timeSpan = TimeSpan.FromSeconds(TimeStart);
// 		TextBox.text = String.Format("Enemy Shields:\n{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds + 1);

// 		if (TimeStart <= 0)
// 		{
// 			TimerUI.SetActive(false);
// 		}
// 	}
}