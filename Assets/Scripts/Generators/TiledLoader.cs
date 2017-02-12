using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using MiniJSON;
using UnityEngine.Analytics;

public class TiledLoader : MonoBehaviour {

	public string fileName;
	public GameObject[] gos;
	// Use this for initialization
	IEnumerator Start () {
		Debug.Log ("Top of the method");
		yield return StartCoroutine(loadFromFile());
		//loadFromFile ();
		Debug.Log("Coroutine running");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator loadFromFile()
	{
		// load in from file
		yield return new WaitForEndOfFrame();
		string text = System.IO.File.ReadAllText(fileName);
		// parse from JSON
		var dict = Json.Deserialize(text) as Dictionary <string, System.Object>;
			// build tileSet
		List<System.Object> layers = dict["layers"] as List<System.Object>;
		Dictionary <string, System.Object> layerZero = layers [0] as Dictionary<string, System.Object>;
		long width = (long)layerZero["width"];
		long height = (long)layerZero ["height"];
		List<System.Object> data = layerZero ["data"] as List<System.Object>;
		List<System.Object> tilesets = dict ["tilesets"] as List<System.Object>;
		Dictionary<string, System.Object> setZero = tilesets [0] as Dictionary<string, System.Object>;
		Dictionary<string, System.Object> tiles = setZero ["tileproperties"] as Dictionary<string, System.Object>;

			// step across the data array and instantiate
			// Debug.Log("Width " + width);
			// Debug.Log("Height " + height);
		int counter = 0;
		for (int i = (int) height-1; i >= 0; i--) {
			for (int j = 0; j < width; j++) {
				// need the lookup
				long tile = (long)data[counter];
				tile--;
				string index = tile.ToString ();
				Dictionary<string, System.Object> thisTile = tiles[index] as Dictionary<string, System.Object>;
				string etid = thisTile ["ETID"] as String;
				int GOindex = Int32.Parse (etid);
				//Debug.Log (GOindex);
				GameObject go = this.gos[GOindex];
				//Debug.Log (go.name);
				GameObject.Instantiate (go, this.gameObject.transform.position+(j*Vector3.right*15)+(i*Vector3.forward*15), this.gameObject.transform.rotation);
				// instantiate
				counter++;
				//yield return new WaitForEndOfFrame ();
			}

		}
		int dataHash = data.GetHashCode ();
		IDictionary<string, System.Object> eventData = new Dictionary<string, object> ();
		eventData.Add ("mapHash", dataHash);
		Analytics.CustomEvent ("Tiled map loaded", eventData); 
		yield return new WaitForEndOfFrame();

	}
		
}
