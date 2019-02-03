using UnityEngine;
using UnityEngine.SceneManagement;

public class TankCustomization : MonoBehaviour {

    // list of all available prefab body parts for each category
    [SerializeField] private GameObject[] tankBodies;
    [SerializeField] private GameObject[] tankTurrets;
    [SerializeField] private GameObject[] tankTracks;
    [SerializeField] private GameObject[] tankEngines;

    // an easier way to reference body parts in all functions
    private GameObject tankBody;
    private GameObject tankTurret;
    private GameObject tankTrackLeft;
    private GameObject tankTrackRight;
    private GameObject tankEngine;

    private int bodySelection;
    private int turretSelection;
    private int trackSelection;
    private int engineSelection;

    //buttons to disable in test run mode
    [SerializeField] private GameObject[] UIbuttons;

    //Manager to which we will send values to carry on another scene
    private Test_manager testManager;

    /*    
        - Make all of this information sendable through non destructable game object to another scene
        - Spawn player in another scene with customizations set up + with functional code added to each part
        - See how different functionality on different parts will work. If thats trash, just manage it all in one place
    */

    private void Start()
    {
        // spawn the first default tank , with default part prefabs
        tankBody = Instantiate(tankBodies[bodySelection], transform.position, Quaternion.identity);
        tankTurret = Instantiate(tankTurrets[turretSelection], tankBody.transform.GetChild(0).position, Quaternion.identity);
        tankEngine = Instantiate(tankEngines[engineSelection], tankBody.transform.GetChild(1).position, Quaternion.identity);
        tankTrackLeft = Instantiate(tankTracks[trackSelection], tankBody.transform.GetChild(2).position, Quaternion.identity);
        tankTrackRight = Instantiate(tankTracks[trackSelection], tankBody.transform.GetChild(3).position, Quaternion.identity);

        // set all parts as child of Body
        ParentOptions(tankTurret, tankBody);
        ParentOptions(tankEngine, tankBody);
        ParentOptions(tankTrackLeft, tankBody);
        ParentOptions(tankTrackRight, tankBody);

        // get the test manager component for later use
        testManager = GameObject.Find("Manager_TEST").GetComponent<Test_manager>();
    }

    private void ParentOptions(GameObject futureChild, GameObject futureParent)
    {
        futureChild.transform.parent = futureParent.transform;
    }


    public void NextBody()
    {
        //destroy all parts and body, reinstantiate the body, and assign all the parts to correct places (deriving from body)
        Destroy(tankBody);
        //reset all other parts
        Destroy(tankTurret);
        Destroy(tankTrackLeft);
        Destroy(tankTrackRight);
        Destroy(tankEngine);

        if (bodySelection >= tankBodies.Length-1)
        {
            bodySelection = 0;
        }
        else
        {
            bodySelection += 1;
        }    
        tankBody = Instantiate(tankBodies[bodySelection], transform.position, Quaternion.identity);
        // respawn previous parts on new body
        tankTurret = Instantiate(tankTurrets[turretSelection], tankBody.transform.GetChild(0).position, Quaternion.identity);
        tankEngine = Instantiate(tankEngines[engineSelection], tankBody.transform.GetChild(1).position, Quaternion.identity);
        tankTrackLeft = Instantiate(tankTracks[trackSelection], tankBody.transform.GetChild(2).position, Quaternion.identity);
        tankTrackRight = Instantiate(tankTracks[trackSelection], tankBody.transform.GetChild(3).position, Quaternion.identity);
        

        // make children again
        ParentOptions( tankTurret,  tankBody);
        ParentOptions( tankEngine,  tankBody);
        ParentOptions( tankTrackLeft,  tankBody);
        ParentOptions( tankTrackRight,  tankBody);
    }

    public void NextTurret()
    {
        Destroy(tankTurret);
        if(turretSelection >= tankTurrets.Length - 1)
        {
            turretSelection = 0;
        }
        else
        {
            turretSelection += 1;
        }
       // get transform of turret location placer nr. 1 on tank body, and place turret in that location
        tankTurret = Instantiate(tankTurrets[turretSelection], tankBody.transform.GetChild(0).position,Quaternion.identity);

        ParentOptions( tankTurret,  tankBody);
    }

    public void NextEngine()
    {
        Destroy(tankEngine);
        if (engineSelection >= tankEngines.Length - 1)
        {
            engineSelection = 0;
        }
        else
        {
            engineSelection += 1;
        }
        tankEngine = Instantiate(tankEngines[engineSelection], tankBody.transform.GetChild(1).position, Quaternion.identity);

        ParentOptions(tankEngine, tankBody);
    }

    public void NextTrack()
    {
        Destroy(tankTrackLeft);
        Destroy(tankTrackRight);
        if(trackSelection >= tankTracks.Length - 1)
        {
            trackSelection = 0;
        }
        else
        {
            trackSelection += 1;
        }
        tankTrackLeft = Instantiate(tankTracks[trackSelection], tankBody.transform.GetChild(2).position, Quaternion.identity);
        tankTrackRight = Instantiate(tankTracks[trackSelection], tankBody.transform.GetChild(3).position, Quaternion.identity);

        ParentOptions( tankTrackLeft,  tankBody);
        ParentOptions( tankTrackRight,  tankBody);
    }


    public void TestRun()
    {
        // deactive UI for editing tank
        int i;
        for (i = 0; i < UIbuttons.Length; i++)
        {
            UIbuttons[i].SetActive(false);
        }

        // activate controls of selected body
        var pController = tankBody.GetComponent<PlayerController>();
        pController.enabled = true;
        // set up turret transform for rotations
        pController.turret = GameObject.FindGameObjectWithTag("Turret").transform;
        // inside of turret, set up barrel for spawning bullets
        pController.barrel = pController.turret.GetChild(0).transform;
       
    }

    // for testing purposes
    public void LoadGame()
    {
        if (testManager == null)
        {
            print("manager wasnt found");
        }
        else
        {
            // send values to test manager to hold
            testManager.acquireValues(bodySelection,turretSelection,trackSelection,engineSelection);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
            //SceneManager.LoadScene("1");
        }
        
    }

}
