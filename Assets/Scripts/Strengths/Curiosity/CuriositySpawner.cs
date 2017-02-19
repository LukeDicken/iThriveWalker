/*
 * 
 * CuriositySpawner.cs -- Provides code to support the "Curiosity" strength
 * Created 2/11/17
 * 
 * As a proxy for curiosity, people who are curious might explore to see what
 * something on the horizon is. They might repeat this behaviour to learn more
 * about their environment. This class is a basic pass at that, by spawning
 * objects, and when the player gets close to them, spawning another
 * 
 * There is some sanity check back to the WorldManager to ensure that the
 * place that we are trying to spawn an object is actually valid for placement.
 * This logic can probably get richer as the world does.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuriositySpawner : MonoBehaviour {

    public EntityManager em;
	public GameObject[] points;
	public int spawnRadius;
	public int minDistance;
	private GameObject player;
	private List<GameObject> currentPOIs;
	private List<GameObject> ignoreList;
	private WorldManager wm;
    List<GameObject> gos;

    // Use this for initialization
    void Start () {
		// grab the player GO so we can track where it goes

		currentPOIs = new List<GameObject> ();
		ignoreList = new List<GameObject> ();
		player = GameObject.FindGameObjectWithTag("Player");
        GameObject WorldManager = GameObject.FindGameObjectWithTag("WorldManager");
        wm = WorldManager.GetComponent<WorldManager> ();
        em = WorldManager.GetComponent<EntityManager>();
        Dictionary<string, GameObject> POIs = em.getObjectsFromTag("POI");
        Dictionary<string, GameObject>.ValueCollection v = POIs.Values;
        
        gos = new List<GameObject>();
        gos.AddRange(v);
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

    private GameObject getRandomPOI()
    {
        int randIndex = Random.Range(0, gos.Count);
        return gos[randIndex];
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
               
				GameObject newGO = GameObject.Instantiate (getRandomPOI(), pos, player.transform.rotation) as GameObject;
				currentPOIs.Add (newGO);
				LogWrapper.Log (newGO.name);
				placeable = true;
			}
		}
	}
}
