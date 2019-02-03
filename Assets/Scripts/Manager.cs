using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    [Header ("Prefabs")]
    public GameObject playerPrefab;
    public GameObject playerCamera;

    [Header("Spawn Locations")]
    public Transform[] spawnLocations;

                                                                        // TEST CODE BELOW


    // test manager for data transfer between scenes
    private Test_manager testManager;

    // list of all available prefab body parts for each category
    [Header("Test code CUSTOMIZATION spawning")]
    [SerializeField] private GameObject[] tankBodies;
    [SerializeField] private GameObject[] tankTurrets;
    [SerializeField] private GameObject[] tankTracks;
    [SerializeField] private GameObject[] tankEngines;

    void Start () {
        /*  
         GameObject player = Instantiate(playerPrefab, spawnLocations[0].position, Quaternion.identity);
         GameObject camera = Instantiate(playerCamera, spawnLocations[0].position, Quaternion.identity);
        */

        // find it and assign it automatically
        testManager = FindObjectOfType<Test_manager>();
        // spawn player with all of the settings sent over by testManager
        SpawnPlayer(testManager.bodyValue,testManager.turretValue,testManager.engineValue,testManager.trackValue);
        // spawn a camera targeted at objects with tag Player
        GameObject camera = Instantiate(playerCamera, spawnLocations[0].position, Quaternion.identity);
    }

    private void ParentOptions(GameObject futureChild, GameObject futureParent)
    {
        futureChild.transform.parent = futureParent.transform;
    }

    void SpawnPlayer(int body,int turret, int engine, int tracks)
    {
        // spawn at spawn point location!!! ( the body )
        var tankBody = Instantiate(tankBodies[body], spawnLocations[0].position, Quaternion.identity);
        var tankTurret = Instantiate(tankTurrets[turret], tankBody.transform.GetChild(0).position, Quaternion.identity);
        var tankEngine = Instantiate(tankEngines[engine], tankBody.transform.GetChild(1).position, Quaternion.identity);
        var tankTrackLeft = Instantiate(tankTracks[tracks], tankBody.transform.GetChild(2).position, Quaternion.identity);
        var tankTrackRight = Instantiate(tankTracks[tracks], tankBody.transform.GetChild(3).position, Quaternion.identity);

        // set all parts as child of Body
        ParentOptions(tankTurret, tankBody);
        ParentOptions(tankEngine, tankBody);
        ParentOptions(tankTrackLeft, tankBody);
        ParentOptions(tankTrackRight, tankBody);

        // activate controls of selected body
        var pController = tankBody.GetComponent<PlayerController>();
        pController.enabled = true;
        // set up turret transform for rotations
        pController.turret = GameObject.FindGameObjectWithTag("Turret").transform;
        // inside of turret, set up barrel for spawning bullets
        pController.barrel = pController.turret.GetChild(0).transform;

    }

}
