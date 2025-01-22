using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject gasItemPrefab;
    public float roadHeight = 10f;
    public int maxRoadsAhead = 5;
    public float gasSpawnInterval = 2f;
    
    private Transform player;
    private float spawnY = 0f;
    private float nextGasSpawnTime = 0f;
    
    private void Start()
    {
        player = PlayerController.Instance.transform;
        InitializeRoads();
    }
    
    private void Update()
    {
        if (!GameManager.Instance.isGameActive) return;
        
        if (player.position.y + roadHeight > spawnY)
        {
            SpawnRoad();
        }
        
        if (Time.time > nextGasSpawnTime)
        {
            SpawnGasItem();
            nextGasSpawnTime = Time.time + gasSpawnInterval;
        }

        CleanupObjects();
    }
    
    public void ResetRoads()
    {
       
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        spawnY = 0f;
        
        InitializeRoads();
        
        nextGasSpawnTime = Time.time + gasSpawnInterval;
    }
    
    private void InitializeRoads()
    {
        spawnY = player.position.y - roadHeight;
        
        for (int i = 0; i < maxRoadsAhead; i++)
        {
            SpawnRoad();
        }
    }
    
    private void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, new Vector2(0, spawnY), Quaternion.identity);
        road.transform.SetParent(transform);
        spawnY += roadHeight;
    }
    
    private void SpawnGasItem()
    {
        float[] xPositions = { -2f, 0f, 2f };
        float randomX = xPositions[Random.Range(0, xPositions.Length)];
        
        float spawnYPosition = player.position.y + roadHeight;
        
        Vector2 itemPosition = new Vector2(randomX, spawnYPosition);
        GameObject gasItem = Instantiate(gasItemPrefab, itemPosition, Quaternion.identity);
        gasItem.transform.SetParent(transform);
    }
    
    private void CleanupObjects()
    {
        float cleanupY = player.position.y - roadHeight * 2;
        
        foreach (Transform child in transform)
        {
            if (child.position.y < cleanupY)
            {
                Destroy(child.gameObject);
            }
        }
    }
}