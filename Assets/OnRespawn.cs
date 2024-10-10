using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OnRespawn : MonoBehaviour
{
    public static OnRespawn Instance { get; private set; }
    [SerializeField] private GameObject walker;

    [SerializeField] private GameObject spitter;
    [SerializeField] private GameObject bloater;
    private int bloaterCounter;
    private int walkerCounter;
    private int spitterCounter;
    private void Awake()
    {
        Instance = this;
        Instantiate(walker, new Vector2(-96, -5), Quaternion.identity);
        Instantiate(spitter, new Vector2(-114, 6), Quaternion.identity);
        Instantiate(bloater, new Vector2(-157, 2), Quaternion.identity);
    }
    public void Respawn()
    {
        if(!Damageable.Instance.IsAlive)
        {

            SpawnEnemy(new Vector2(-96, -5), walker, walkerCounter);
            walkerCounter++;
            SpawnEnemy(new Vector2(-91, -5), walker, walkerCounter);
            SpawnEnemy(new Vector2(-114, 6), spitter, spitterCounter);
            SpawnEnemy(new Vector2(-157, 2), bloater, bloaterCounter);
        }
    }
    public void SpawnEnemy(Vector2 spawnPosition, GameObject enemyPrefab, int counter)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.name = enemyPrefab.name + "_" + counter;
       
    }

}
