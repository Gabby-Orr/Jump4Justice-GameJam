using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject initPlatform;
    public List<GameObject> platformPrefabs;
    public GameObject specialPlatform;
    public GameObject victim;
    public GameObject enemy;
    public GameObject boss;
    private List<GameObject> instantiatedPlatforms;
    private List<GameObject> instantiatedEnemies;
    private float startX;
    private float startY;
    private float gap = 2f;
    private float scale = 5f;
    public float xSpread = 30f;
    private float freq;
    private static int RIGHT = 1;
    private static int LEFT = -1;
    void Awake()
    {
        startX = initPlatform.gameObject.transform.position.x;
        startY = initPlatform.gameObject.transform.position.y;
        instantiatedPlatforms = new List<GameObject>();
        instantiatedEnemies = new List<GameObject>();
        freq = xSpread / 8f;

        // Generate down-right paths
        GenerateSpecialPath(RIGHT, -0.5f, Random.value * scale);
        GeneratePath(RIGHT, -0.25f, Random.value * scale);
        GeneratePath(RIGHT, -0.8f, Random.value * scale);

        // Generate up-left paths
        GenerateSpecialPath(LEFT, -0.5f, Random.value * scale);
        GeneratePath(LEFT, -0.25f, Random.value * scale);
        GeneratePath(LEFT, -0.8f, Random.value * scale);
    }

    // generate a random path up to 'xSpread' horizontal units away from the origin
    // in the given direction with the given average slope. the seed is used to pick
    // the spot in the perlin noise map.
    private Vector3 GeneratePath(int direction, float slope, float seed)
    {
        // use this counter to avoid generating the first platform
        int loopCount = 0;

        Vector3 platformVector = new Vector3(startX, startY, 0);
        for (float x = startX; (x < xSpread) && (x > -xSpread);)
        {
            float width;
            // skip the first platform, and other passes have a 75% chance of generating a platform
            if (loopCount > 0 && Random.value < 0.75f)
            {
                int ind = Random.Range(0, platformPrefabs.Count);
                width = platformPrefabs[ind].GetComponent<SpriteRenderer>().bounds.size.x;
                float y = Mathf.PerlinNoise(x / freq, seed);
                float vecX = x + (width / 2.0f);
                float vecY = startY + (y * scale) + slope*x;
                platformVector = new Vector3(vecX, vecY, 0);

                Quaternion rotation = Quaternion.identity;
                // rotate the boxes by a random amount for variety
                if (platformPrefabs[ind].tag.Equals("Box"))
                    rotation = Quaternion.Euler(rotation.x, rotation.y, Random.Range(0f, 89f));
                // normal platforms have a chance of generating with an enemy
                else if (Random.value < 0.5f)
                    instantiatedEnemies.Add(Instantiate(enemy, new Vector3(platformVector.x, platformVector.y + 0.35f, platformVector.z), Quaternion.identity));

                instantiatedPlatforms.Add(Instantiate(platformPrefabs[ind], platformVector, rotation));
            }
            else
                width = initPlatform.GetComponent<SpriteRenderer>().bounds.size.x;
            x += direction * (width + gap);
            loopCount++;
        }

        // return the position of the final platform so we can make other things there if we want
        return platformVector;
    }

    // generate a path and put a special platform at the end
    private void GenerateSpecialPath(int direction, float slope, float seed)
    {
        Vector3 endVector = GeneratePath(direction, slope, seed);
        instantiatedPlatforms.Add(Instantiate(specialPlatform, new Vector3(endVector.x, endVector.y - scale, endVector.z), Quaternion.identity));

        // add a victim to the platform at the left end, add a boss to the platform at the right end
        if (direction == LEFT)
            Instantiate(victim, new Vector3(endVector.x, endVector.y - scale, endVector.z), Quaternion.identity);
        else
            Instantiate(boss, new Vector3(endVector.x, endVector.y - scale, endVector.z), Quaternion.identity);
    }
}
