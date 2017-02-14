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
    private DayNightStart dns;
    private FirstPersonController firstPersonController;

	float timeSinceMovement = 0f;

	void Start() 
	{
		timeSinceMovement = 0f;
        dns = dayNight.GetComponent<DayNightStart>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        firstPersonController = player.GetComponent<FirstPersonController>();
    }
	
	void Update()
	{
		if (firstPersonController.Moving)
		{
			timeSinceMovement = 0f;
			dns.minutesInDay = minutesInDayMax;
		} else
		{
			timeSinceMovement += Time.deltaTime;
		
			if (timeSinceMovement > timeToDayMin)
				timeSinceMovement = timeToDayMin;

			float day = (minutesInDayMin - minutesInDayMax)*timeSinceMovement/timeToDayMin + minutesInDayMax;
			dns.minutesInDay = day;
		}
	}
}
