using UnityEngine;
using System.Collections;

public class RandomMap : MonoBehaviour {
    public int sizeX, sizeY;
    public int tileSize;
    public GameObject[] tiles;
    private int[,] map;

	// Use this for initialization
	void Start () {
        int[,] landscape = new int[sizeY, sizeX];
        int[,] map = new int[sizeY, sizeX];

        Transform parent = GameObject.FindGameObjectWithTag("TileHolder").transform;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                int index = Random.Range(0, tiles.Length);
                if (index >= tiles.Length)
                {
                    index = tiles.Length - 1;
                }
                //Debug.Log(index);
                GameObject go = GameObject.Instantiate(tiles[index], new Vector3((x * tileSize), 0, (y * tileSize)), new Quaternion()) as GameObject;
                go.transform.parent = parent;// GameObject.FindGameObjectWithTag("TileHolder").transform;
                map[y, x] = index;
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
