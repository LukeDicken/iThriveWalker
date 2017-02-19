using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MiniJSON;

public class EntityManager : MonoBehaviour {

    private Dictionary<string, GameObject> entityLibrary;

	// Use this for initialization
	void Start () {
        entityLibrary = new Dictionary<string, GameObject>();
        loadFromJSON();
	}
	
    private void loadFromJSON()
    {
        /*
        * scan for JSONs
        * load them into the library
        *      (we'll need a library)
        * long term we might want to setup prefab parameters
        * but for now lets focus on getting
        * 
        */
        string[] jsons = Directory.GetFiles("Assets/Data/EntityTemplates", "*.json", SearchOption.AllDirectories);
        foreach(string s in jsons)
        {
            string text = System.IO.File.ReadAllText(s);
            var dict = Json.Deserialize(text) as Dictionary<string, System.Object>;
            string etidName = dict["etid"] as string;
            var asset = dict["asset"] as IDictionary<string, System.Object>;
            string prefab = asset["prefab"] as string;
            // find a prefab with that name
            LogWrapper.Log(prefab);
            GameObject go = Resources.Load(prefab) as GameObject;
            
            // here we might want to add components and set them up with parameters

            entityLibrary.Add(etidName, go);
        }
    }

    public GameObject getObjectFromEtidName(string name)
    {
        LogWrapper.Log("Finding etid " + name);
        if (entityLibrary.ContainsKey(name))
        {
            LogWrapper.Log("Etid exists");
            GameObject go = entityLibrary[name];
            return go;
        }
        LogWrapper.Error("Couldn't find entity with name " + name);
        return null;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
