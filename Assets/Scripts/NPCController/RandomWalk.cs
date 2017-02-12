using UnityEngine;
using System.Collections;

public class RandomWalk : MonoBehaviour {


    enum dirs { up, down, left, right }
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void takeTurn()
    {
        int i = Random.Range(0, 4);
        Vector3 trans;
        switch (i)
        {
            case (int)dirs.up:
                trans = new Vector3(0, 0, 10);
                break;
            case (int)dirs.down:
                trans = new Vector3(0, 0, -10);
                break;
            case (int)dirs.left:
                trans = new Vector3(-10, 0, 0);
                break;
            case (int)dirs.right:
                trans = new Vector3(10, 0, 0);
                break;
            default:
                trans = new Vector3(0, 0, 0);
                break;
        }
        this.transform.Translate(trans);
    }
}
