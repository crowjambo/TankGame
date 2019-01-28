using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    [Header ("Prefabs")]
    public GameObject playerPrefab;
    public GameObject playerCamera;

    [Header("Spawn Locations")]
    public Transform[] spawnLocations;

	
	void Start () {
        GameObject player = Instantiate(playerPrefab, spawnLocations[0].position, Quaternion.identity);
        GameObject camera = Instantiate(playerCamera, spawnLocations[0].position, Quaternion.identity);

	}
	

}
