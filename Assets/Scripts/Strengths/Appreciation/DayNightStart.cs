using UnityEngine;
using System.Collections;

public class DayNightStart : MonoBehaviour 
{
	public float minutesInDay = 1.0f;
	public float startTime = 9.0f;
	public float duskPercentOfDay = 0.25f;

	float timer;
	float percentageOfDay;
	float turnSpeed;
	float timeOfDay;
	float startingAngle;

	void Start()
	{
		timer = 0.0f;
		//startingAngle = 360.0f; //this is a fudge to start at nightfall
		//transform.RotateAround (transform.position, transform.right, startingAngle);
	}

	void Update() 
	{
		checkTime();
		UpdateLights();

		//turnSpeed = 360.0f / (minutesInDay * 60.0f) * Time.deltaTime;
		turnSpeed = startingAngle / (minutesInDay * 60.0f) * Time.deltaTime;
	
		transform.RotateAround(transform.position, transform.right, turnSpeed*-1.0f);

		//Debug.Log(percentageOfDay);
		//Debug.Log(transform.position);
	}

	void UpdateLights()
	{
		Light l = GetComponent<Light>();

		if (isNight())
		{
			if (l.intensity > 0.0f)
			{
				float duskSpeed = Time.deltaTime/(minutesInDay*60f*duskPercentOfDay);
				l.intensity -= duskSpeed;
			}
		}
		else 
		{
			if (l.intensity < 1.0f)
			{
				float dawnSpeed = Time.deltaTime/(minutesInDay*60f*duskPercentOfDay);
				l.intensity += dawnSpeed;
			}
		}
	}

	//just reversed these booleans to make everything start with night
	bool isNight()
	{
		bool c = false;
		if (percentageOfDay > 0.5)
		{
			c = true;
		}

		return c;
	}

	void checkTime()
	{
		timer += Time.deltaTime;
		percentageOfDay = timer/(minutesInDay*60.0f);
		timeOfDay = (startTime + (timer/100)); 
		if (timer > (minutesInDay*60.0f))
		{
			timer = 0.0f;
		}
	}
}
