using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Entity playerPrefab = null;
    public GameObject spawnPointPlayer = null;

    private void Awake()
    {
        if (playerPrefab != null)
        {
            if (spawnPointPlayer)
            {
                GameObject go = playerPrefab.gameObject;
                Instantiate(go, spawnPointPlayer.transform.position, spawnPointPlayer.transform.rotation);
                Debug.Log("Player spawned");
            }
            else
                Debug.LogError("You need to add a Player Spawn Point!");
        }
        else
            Debug.LogError("You need to add a Player Prefab!");   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
