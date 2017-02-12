using UnityEngine;
using System.Collections;

public class DayNightStart : MonoBehaviour {

	public float minutesInDay=1.0f ;
	public float startTime = 9.0f;


	float timer;
	float percentageOfDay;
	float turnSpeed;
	float timeOfDay;
	float startingAngle;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		//startingAngle = 360.0f; //this is a fudge to start at nightfall
		//transform.RotateAround (transform.position, transform.right, startingAngle);
	}
	
	// Update is called once per frame
	void Update () {
		checkTime ();
		UpdateLights ();

		turnSpeed = 360.0f / (minutesInDay * 60.0f) * Time.deltaTime;
		//turnSpeed = startingAngle / (minutesInDay * 60.0f) * Time.deltaTime;
	
		transform.RotateAround (transform.position, transform.right, turnSpeed*-1.0f);

		//Debug.Log(percentageOfDay);
		Debug.Log(transform.position);

	}

	void UpdateLights(){
		Light l = GetComponent<Light> ();
		if (isNight()){
			if (l.intensity>0.0f){
				l.intensity -= 0.05f;
			}
		}
		else {
			if(l.intensity <1.0f){
				l.intensity += 0.05f;

		}
		}
	}
	//just reversed these booleans to make everything start with night
	bool isNight(){
		bool c = true;
		if (percentageOfDay > 0.5){
			c = false;
		}
		return c;
	}

	void checkTime(){
		timer += Time.deltaTime;
		percentageOfDay = timer / (minutesInDay * 60.0f);
		timeOfDay = (startTime + (timer / 100)); 
		if (timer > (minutesInDay * 60.0f)){
			timer = 0.0f;
	}

}
}
