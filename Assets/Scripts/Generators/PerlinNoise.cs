using UnityEngine;
using System.Collections;

public class PerlinNoise : MonoBehaviour {

    public int sizeX, sizeY;
    public int tileSize;
    public GameObject[] tiles;
    private int par1, par2, par3, par4;

    private int[] param1 = { 15413, 15649, 15887, 16111, 15427, 15661, 15889, 16127, 15439, 15667, 15901, 16139, 15443, 15671, 15907, 16141, 15451, 15679, 15913, 16183, 15461, 15683, 15919, 16187, 15467, 15727, 15923, 16189, 15473, 15731, 15937, 16193, 15493, 15733, 15959, 16217, 15497, 15737, 15971, 16223, 15511, 15739, 15973, 16229, 15527, 15749, 15991, 16231, 15541, 15761, 16001, 16249, 15551, 15767, 16007, 16253, 15559, 15773, 16033, 16267, 15569, 15787, 16057, 16273, 15581, 15791, 16061, 16301, 15583, 15797, 16063, 16319, 15601, 15803, 16067, 16333, 15607, 15809, 16069, 16339, 15619, 15817, 16073, 16349, 15629, 15823, 16087, 16361, 15641, 15859, 16091, 16363, 15643, 15877, 16097, 16369, 15647, 15881, 16103, 16381 };
    private int[] param2 = { 788497, 788869, 789137, 789571, 788521, 788873, 789149, 789577, 788527, 788891, 789169, 789587, 788531, 788897, 789181, 789589, 788537, 788903, 789221, 789611, 788549, 788927, 789227, 789623, 788561, 788933, 789251, 789631, 788563, 788941, 789311, 789653, 788569, 788947, 789323, 789671, 788603, 788959, 789331, 789673, 788621, 788971, 789343, 789683, 788651, 788993, 789367, 789689, 788659, 788999, 789377, 789709, 788677, 789001, 789389, 789713, 788687, 789017, 789391, 789721, 788701, 789029, 789407, 789731, 788719, 789031, 789419, 789739, 788761, 789067, 789443, 789749, 788779, 789077, 789473, 789793, 788789, 789091, 789491, 789823, 788813, 789097, 789493, 789829, 788819, 789101, 789511, 789847, 788849, 789109, 789527, 789851, 788863, 789121, 789533, 789857, 788867, 789133, 789557, 789883 };
    private int[] param3 = { 179424691, 179425033, 179425601, 179426083, 179424697, 179425063, 179425619, 179426089, 179424719, 179425069, 179425637, 179426111, 179424731, 179425097, 179425657, 179426123, 179424743, 179425133, 179425661, 179426129, 179424779, 179425153, 179425693, 179426141, 179424787, 179425171, 179425699, 179426167, 179424793, 179425177, 179425709, 179426173, 179424797, 179425237, 179425711, 179426183, 179424799, 179425261, 179425777, 179426231, 179424821, 179425319, 179425811, 179426239, 179424871, 179425331, 179425817, 179426263, 179424887, 179425357, 179425819, 179426321, 179424893, 179425373, 179425823, 179426323, 179424899, 179425399, 179425849, 179426333, 179424907, 179425403, 179425859, 179426339, 179424911, 179425423, 179425867, 179426341, 179424929, 179425447, 179425879, 179426353, 179424937, 179425453, 179425889, 179426363, 179424941, 179425457, 179425907, 179426369, 179424977, 179425517, 179425943, 179426407, 179424989, 179425529, 179425993, 179426447, 179425003, 179425537, 179426003, 179426453, 179425019, 179425559, 179426029, 179426491, 179425027, 179425579, 179426081, 179426549 };

    private int[,] map;

	// Use this for initialization

	void Start () {
        
        int i1 = Random.Range(0, param1.Length);
        par1 = param1[i1];
        int i2 = Random.Range(0, param2.Length);
        par2 = param2[i2];
        int i3 = Random.Range(0, param3.Length);
        par3 = param3[i3];
        int[,] landscape = new int[sizeY, sizeX];
        map = new int[sizeY, sizeX];

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                landscape[y, x] = (int) calcPerlinNoise(x, y);
                map[y, x] = -1;
            }
        }

        // find the max value
        int maxValue = int.MinValue;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if (landscape[y, x] > maxValue)
                {
                    maxValue = landscape[y, x];
                }
            }
        }

        // find the min value
        int minValue = int.MaxValue;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                if (landscape[y, x] < minValue)
                {
                    minValue = landscape[y, x];
                }
            }
        }
        double length = maxValue - minValue;
        double size = length / tiles.Length;
        string output = "";
        Transform parent = GameObject.FindGameObjectWithTag("TileHolder").transform;
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                int index = (int)Mathf.Floor((float) (landscape[y, x] - minValue) / (float)size);
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
        Debug.Log(output);
	}

    // Update is called once per frame
    void Update()
    {
	
	}

    private double generateNoise(int x, int y)
    {
        int n = x + y * 57;
        n = (n<<13) ^ n;
        return ( 1.0 - ( (n * (n * n * par1 + par2) + par3) & 0x7fffffff) / 1073741824.0);   
    }

    private double smoothNoise(int x, int y)
    {
        double corners = (generateNoise(x - 1, y - 1) + generateNoise(x - 1, y + 1) + generateNoise(x + 1, y - 1) + generateNoise(x + 1, y + 1)) / 16;
        double sides = (generateNoise(x - 1, y) + generateNoise(x + 1, y) + generateNoise(x, y - 1) + generateNoise(x, y + 1)) / 8;
        double center = generateNoise(x, y) / 4;
        return corners + sides + center;
    }

    private double interpolateNoise(double x, double y)
    {
        int intX = (int)x;
        double fracX = x - intX;

        int intY = (int)y;
        double fracY = y - intY;

        double v1 = smoothNoise(intX, intY);
        double v2 = smoothNoise(intX + 1, intY);
        double v3 = smoothNoise(intX, intY + 1);
        double v4 = smoothNoise(intX + 1, intY + 1);

        double i1 = interpolate(v1, v2, fracX);
        double i2 = interpolate(v3, v4, fracX);

        return interpolate(i1, i2, fracY);
    }

    private double interpolate(double a, double b, double frac)
    {
        // cosine interpolation
        float ft = (float) (frac * Mathf.PI);
        double f = (1 - Mathf.Cos(ft)) * 0.5;
        return a * (1 - f) + b * f;
    }

    private double calcPerlinNoise(int x, int y)
    {
        double total = 0.0;
        double per = 0.25;
        int octaves = 10;

        for (int i = 0; i < octaves; i++)
        {
            double freq = Mathf.Pow(2, i);
            double amp = Mathf.Pow((float)per, i);
            total += interpolateNoise((double)(x * freq), (double)(y * freq)) * amp;
        }

        return total * 100;
    }

    public Vector2 transform2grid(Vector3 trans)
    {
        return new Vector2(trans.x / 10, trans.y / 10);
    }

    public bool isWalkable(int x, int y)
    {
        if (map[y, x] == 0 || map[y, x] == 4)
        {
            return false;
        }
        return true;
    }
}
