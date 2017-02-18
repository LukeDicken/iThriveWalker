using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CuriositySpawner : MonoBehaviour {

	public GameObject[] points;
	public int spawnRadius;
	public int minDistance;
	private GameObject player;
	private List<GameObject> currentPOIs;
	private List<GameObject> ignoreList;
	private WorldManager wm;

	// Use this for initialization
	void Start () {
		// grab the player GO so we can track where it goes

		currentPOIs = new List<GameObject> ();
		ignoreList = new List<GameObject> ();
		player = GameObject.FindGameObjectWithTag("Player");
		wm = GameObject.FindGameObjectWithTag ("WorldManager").GetComponent<WorldManager> ();
		// spawn a POI
		spawn();
	}
	
	// Update is called once per frame
	void Update () {
		// have I reached the POI?
		// log to analytics
		// spawn another
			// nearby?
		int spawnCount = 0;
		foreach (GameObject go in currentPOIs) {
			float dist = Vector3.Magnitude (player.transform.position - go.transform.position);
			if (dist < 5.0f && !(ignoreList.Contains(go))) {
				// reached
				//deletion.Add(go);
				ignoreList.Add(go);
				spawnCount++;
			}
		}
		while(spawnCount > 0)
		{
			spawn ();
			spawnCount--;
		}
	}

	public void spawn()
	{
		bool placeable = false;
		while(!placeable)
		{
			// pick a location on the unit circle * radius
			Vector2 r = Random.insideUnitCircle * spawnRadius;
			Vector3 pos = new Vector3(r.x, 0, r.y) + player.transform.position;
            pos.y = 0;
			// ask WorldManager if that type is placeable
			// if yes, spawn a random object
			// ensure a minimum distance
			if (Vector3.Magnitude (player.transform.position - pos) >= minDistance && wm.isValidForPlacement(wm.getTypeFromPosition(new Vector2(pos.x, pos.z)))) {
				int rand = Random.Range (0, this.points.Length);
				GameObject newGO = GameObject.Instantiate (points [rand], pos, player.transform.rotation) as GameObject;
				currentPOIs.Add (newGO);
				LogWrapper.Log (newGO.name);
				placeable = true;
			}
		}
	}
}
