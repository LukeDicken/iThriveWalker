using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 trans;
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            trans = new Vector3(0, 0, 10);
            this.transform.Translate(trans);
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Controller>().takeTurn();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            trans = new Vector3(0, 0, -10);
            this.transform.Translate(trans);
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Controller>().takeTurn();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            trans = new Vector3(-10, 0, 0);
            this.transform.Translate(trans);
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Controller>().takeTurn();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            trans = new Vector3(10, 0, 0);
            this.transform.Translate(trans);
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Controller>().takeTurn();
        }        
	}
}
