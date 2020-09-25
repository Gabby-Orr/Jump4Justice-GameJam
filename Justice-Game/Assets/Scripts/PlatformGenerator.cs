using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject initPlatform;
    public List<GameObject> platformPrefabs;
    private List<GameObject> instantiatedPlatforms;
    private float startX;
    private float startY;
    private float gap = 2f;
    private float scale = 5f;
    private float freq = 4f;
    private static int RIGHT = 1;
    private static int LEFT = -1;
    void Awake()
    {
        startX = initPlatform.gameObject.transform.position.x;
        startY = initPlatform.gameObject.transform.position.y;
        Debug.Log("start values: (" + startX + ", " + startY + ")");
        instantiatedPlatforms = new List<GameObject>();

        // Generate down-right paths
        GeneratePath(RIGHT, -0.5f, Random.value * scale);
        GeneratePath(RIGHT, -0.25f, Random.value * scale);
        GeneratePath(RIGHT, -0.8f, Random.value * scale);

        // Generate up-left paths
        GeneratePath(LEFT, 0.5f, Random.value * scale);
        GeneratePath(LEFT, 0.25f, Random.value * scale);
        GeneratePath(LEFT, 0.8f, Random.value * scale);
    }

    void GeneratePath(int direction, float slope, float seed)
    {
        int loopCount = 0;
        for (float x = startX; x < 30;)
        {
            float width;
            if (loopCount > 0 && Random.value < 0.75f)
            {
                int ind = Random.Range(0, platformPrefabs.Count);
                // Debug.Log("platforms = " + platformPrefabs.Count);
                // Debug.Log("ind = " + ind);
                width = platformPrefabs[ind].GetComponent<SpriteRenderer>().bounds.size.x;
                // Debug.Log("width = " + width);
                float y = Mathf.PerlinNoise(x / freq, seed);
                float vecX = direction * (x + (width / 2.0f));
                float vecY = startY + (y * scale) + slope*x;
                Debug.Log("vectr values: (" + vecX + ", " + vecY + ")");
                Vector3 platformVector = new Vector3(vecX, vecY, 0);
                Quaternion rotation = Quaternion.identity;
                if (platformPrefabs[ind].tag.Equals("Box"))
                    rotation = Quaternion.Euler(rotation.x, rotation.y, Random.Range(0f, 89f));
                instantiatedPlatforms.Add(Instantiate(platformPrefabs[ind], platformVector, rotation));
            }
            else
                width = initPlatform.GetComponent<SpriteRenderer>().bounds.size.x;
            x += (width + gap);
            loopCount++;
        }
    }
}
