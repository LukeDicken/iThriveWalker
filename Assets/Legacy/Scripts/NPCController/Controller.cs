using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public GameObject[] enemies;
    private int tick;
    public int rate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void takeTurn()
    {
        foreach (GameObject e in enemies)
        {
            RandomWalk rw = e.GetComponent<RandomWalk>();
            if (rw != null)
            {
                rw.takeTurn();
            }
        }
    }
}
