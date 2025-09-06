using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] int startChunksAmount = 12;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;

    List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);

                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        float spawnPostionZ;
        
        if (chunks.Count == 0)
            spawnPostionZ = transform.position.z;
        else
            spawnPostionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;

        Vector3 chunkSpawnPosition = new Vector3(transform.position.x, transform.position.y, spawnPostionZ);
        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPosition, quaternion.identity, chunkParent);

        chunks.Add(newChunk);
    }
}
