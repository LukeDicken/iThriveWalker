using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AppreciationOfBeautyController : MonoBehaviour 
{
	public float minutesInDayMax = 1f;
	public float minutesInDayMin = 0.1f;
	public float timeToDayMin = 10f;

	public GameObject dayNight;

	float timeSinceMovement = 0f;

	void Start() 
	{
		timeSinceMovement = 0f;
	}
	
	void Update()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		FirstPersonController firstPersonController = player.GetComponent<FirstPersonController>();

		if (firstPersonController.Moving)
		{
			timeSinceMovement = 0f;
			dayNight.GetComponent<DayNightStart>().minutesInDay = minutesInDayMax;
		} else
		{
			timeSinceMovement += Time.deltaTime;
		
			if (timeSinceMovement > timeToDayMin)
				timeSinceMovement = timeToDayMin;

			float day = (minutesInDayMin - minutesInDayMax)*timeSinceMovement/timeToDayMin + minutesInDayMax;
			dayNight.GetComponent<DayNightStart>().minutesInDay = day;
		}
	}
}
