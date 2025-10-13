using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Terrain terreno;

    private int[,] gridArray;

    public float tileSize = 1f;
    private int gridX;
    private int gridZ;
    private Vector3 gridOrigin = Vector3.zero;

    void Start()
    {
        gridX = Mathf.FloorToInt(terreno.terrainData.size.x / tileSize);
        gridZ = Mathf.FloorToInt(terreno.terrainData.size.z / tileSize);

        gridArray = new int[gridX, gridZ];

        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                gridArray[x, z] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
