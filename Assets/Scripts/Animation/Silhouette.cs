using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Silhouette : MonoBehaviour
{

    [SerializeField] float timeToWait;
    [SerializeField] private List<Image> spawnPositions;
    [SerializeField] private List<Sprite> silouhettesToSpawn;
    private int randomSilhouette = 0;
    private int randomPosition = 0;
    private bool hasSpawned;

    private void Start() 
    {
        SpawnSilouhette();
    }
    IEnumerator WaitToSwap()
    {
        yield return new WaitForSeconds(timeToWait);
        spawnPositions[randomPosition].sprite = null;
        spawnPositions[randomPosition].enabled = false;
        hasSpawned = false;
        SpawnSilouhette();
    }

    public void SpawnSilouhette()
    {
        if(!hasSpawned)
        {
            randomSilhouette = Random.Range(0,silouhettesToSpawn.Count);
            randomPosition = Random.Range(0,spawnPositions.Count);
            spawnPositions[randomPosition].enabled = true;
            spawnPositions[randomPosition].sprite = silouhettesToSpawn[randomSilhouette];
            hasSpawned = true;
            StartCoroutine(WaitToSwap());
            return;
        }
    }

    

}
