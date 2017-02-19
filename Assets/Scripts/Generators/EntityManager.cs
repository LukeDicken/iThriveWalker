using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
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
            GameObject go = Resources.Load(prefab) as GameObject;
            go = Instantiate(go, new Vector3(-100,-100,-100), this.transform.rotation, this.transform); // this delinks the internal thing from the prefab itself?
            
            // here we want to add components and set them up with parameters
            if(dict.ContainsKey("components"))
            {
                var components = dict["components"] as Dictionary<string, System.Object>;
                foreach (KeyValuePair<string, System.Object> component in components)
                {
                    var componentPieces = component.Value as Dictionary<string, System.Object>;
                    string componentName = componentPieces["componentName"] as string;
                    Type t = Type.GetType(componentName);
                    Component c = go.GetComponent(t);
                    if (c == null)
                    {
                        c = go.AddComponent(t);
                    }

                    // parse parameters
                    var parameters = componentPieces["parameters"] as List<System.Object>;
                    foreach (System.Object p in parameters)
                    {
                        var elements = p as Dictionary<string, System.Object>;
                        string name = elements["name"] as string;
                        //string type = elements["type"] as string;
                        System.Object value = elements["value"];
                        c.SendMessage("set" + name, value);
                        //if(type.Equals("int"))
                        //{
                        //    int value = (int)elements["value"];
                        //    c.SendMessage("set" + name, value);
                        //}
                        //else if(type.Equals("float"))
                        //{

                        //}
                        //else if(type.Equals("string"))
                        //{

                        //}
                        //else if(type.Equals("bool"))
                        //{

                        //}
                        //else
                        //{
                        //    // unsupported parameter type
                        //    LogWrapper.Error("Unsupported paramter type in " + etidName + ", parameter: " + name);
                        //}
                    }
                }
            }

            // we should maybe do some sort of clever tag thing?
            List<string> tags = new List<string>();
            List<System.Object> rawTags = dict["tags"] as List<System.Object>;
            foreach (System.Object t in rawTags)
            {
                string sString = (string)t;
                tags.Add(sString);
            }
            // but errr what do we do with it npw?

            entityLibrary.Add(etidName, go);
        }
        
    }

    public GameObject getObjectFromEtidName(string name)
    {
        if (entityLibrary.ContainsKey(name))
        {
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
