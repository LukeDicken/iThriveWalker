/*
 * 
 * WorldManager.cs -- Creates a world layout from a Tiled json file
 * Created 2/11/17
 * 
 * This class parses a Tiled JSON file and instantiates the map it describes
 * See http://www.mapeditor.org/ for more info on Tiled.
 * Relies on the MiniJSON parser being available
 * 
 * Currently this works using the "ETID" custom property in Tiled,
 * which is a numberic index to the relevant prefab to instantiate from the
 * array that the class keeps in "gos". This needs to be better, and should
 * eventually be a full json-driven system.
 * 
 */


using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MiniJSON;

public class WorldManager : MonoBehaviour {

	public string fileName;
	public GameObject[] gos;
	public string[,] map;

    private EntityManager em;
	// Use this for initialization
	void Start () {
        em = this.gameObject.GetComponent<EntityManager>();
		loadFromFile();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool isValidForPlacement(string type)
	{
        //return !(type == 0); // only water is not valid for placement
        return true; // hack til we figure out how this works in the new entity system.
	}

	public string getTypeFromPosition(Vector2 position)
	{
		Vector2 notoffset = position + new Vector2 (15, 15);
		int wholeX = (int)Math.Floor (notoffset.x / 15.0f);
		int wholeY = (int)Math.Floor (notoffset.y / 15.0f);
		return map [wholeX, wholeY];
	}

	void loadFromFile()
	{
		// load in from file
		string text = System.IO.File.ReadAllText(fileName);
		// parse from JSON
		var dict = Json.Deserialize(text) as Dictionary <string, System.Object>;
			// build tileSet
		List<System.Object> layers = dict["layers"] as List<System.Object>;
		Dictionary <string, System.Object> layerZero = layers [0] as Dictionary<string, System.Object>;
		int width = (int)((long)layerZero["width"]);
		int height = (int)((long)layerZero ["height"]);
		List<System.Object> data = layerZero ["data"] as List<System.Object>;
		List<System.Object> tilesets = dict ["tilesets"] as List<System.Object>;
		Dictionary<string, System.Object> setZero = tilesets [0] as Dictionary<string, System.Object>;
		Dictionary<string, System.Object> tiles = setZero ["tileproperties"] as Dictionary<string, System.Object>;

			// step across the data array and instantiate
			// Debug.Log("Width " + width);
			// Debug.Log("Height " + height);
		int counter = 0;
		map = new string[height, width];
		for (int i = (int) height-1; i >= 0; i--) {
			for (int j = 0; j < width; j++) {
				// need the lookup
				long tile = (long)data[counter];
				tile--;
				string index = tile.ToString ();
				Dictionary<string, System.Object> thisTile = tiles[index] as Dictionary<string, System.Object>;
				string etid = thisTile ["ETID"] as String;

                //Debug.Log (GOindex);
                GameObject go = em.getObjectFromEtidName(etid);
                //Debug.Log (go.name);
                GameObject newGO = GameObject.Instantiate (go, this.gameObject.transform.position+(j*Vector3.right*15)+(i*Vector3.forward*15), this.gameObject.transform.rotation) as GameObject;
				newGO.transform.parent = this.gameObject.transform;
                map[j, i] = etid;
				// instantiate
				counter++;
				//yield return new WaitForEndOfFrame ();
			}

		}
		int dataHash = data.GetHashCode ();
		IDictionary<string, System.Object> eventData = new Dictionary<string, object> ();
		eventData.Add ("mapHash", dataHash);
        //Analytics.CustomEvent ("Tiled map loaded", eventData);
        AnalyticsWrapper.logCustomEvent("Tiled map loaded", eventData);

	}
		
}
