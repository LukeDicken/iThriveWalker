using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuriosityPOI : MonoBehaviour {

    public int radius;
    public string dummystring;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setradius(System.Object value)
    {
        int v = (int)(long)value;
        this.radius = v;
    }

    public void setdummystring(System.Object value)
    {
        string v = value as string;
        this.dummystring = v;
    }
}
