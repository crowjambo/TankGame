using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCustomization : MonoBehaviour {

    // list of all available prefab body parts for each category
    [SerializeField] private GameObject[] tankBodies;
    [SerializeField] private GameObject[] tankTurrets;
    [SerializeField] private GameObject[] tankTracks;
    [SerializeField] private GameObject[] tankEngines;

    // an easier way to reference body parts in all functions
    private GameObject tankBody;
    private GameObject tankTurret;
    private GameObject tankTrack;
    private GameObject tankEngine;

    private int bodySelection;
    private int turretSelection;
    private int trackSelection;
    private int engineSelection;

    /*
        - Make all parts a child of one main part. 
        - Make all of this information sendable through non destructable game object to another scene
        - Spawn player in another scene with customizations set up + with functional code added to each part
        - Or spawn with all changes/parts/scripts inside customization menu as well, but put it in a small room, it becomes like a testing ground
        - Design more different and interesting parts, just to test how it all connects. Smooth connectivity is important!!
        - See how different functionality on different parts will work. If thats trash, just manage it all in one place
    */

    private void Start()
    {
        // spawn the first default tank , with default part prefabs
        tankBody = Instantiate(tankBodies[bodySelection], transform.position, Quaternion.identity);
        tankTurret = Instantiate(tankTurrets[turretSelection], tankBody.transform.GetChild(0).position, Quaternion.identity);
        tankTrack = Instantiate(tankTracks[trackSelection], transform.position, Quaternion.identity);
        tankEngine = Instantiate(tankEngines[engineSelection], transform.position, Quaternion.identity);
    }


    public void NextBody()
    {
        //destroy current body and instantiate a new one in correct position!
        Destroy(tankBody);
        if(bodySelection >= tankBodies.Length-1)
        {
            bodySelection = 0;
        }
        else
        {
            bodySelection += 1;
        }    
        tankBody = Instantiate(tankBodies[bodySelection], transform.position, Quaternion.identity);
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
    }

    public void NextTrack()
    {
        Destroy(tankTrack);
        if(trackSelection >= tankTracks.Length - 1)
        {
            trackSelection = 0;
        }
        else
        {
            trackSelection += 1;
        }
        tankTrack = Instantiate(tankTracks[trackSelection], transform.position, Quaternion.identity);

    }

    public void NextEngine()
    {
        Destroy(tankEngine);
        if(engineSelection >= tankEngines.Length - 1)
        {
            engineSelection = 0;
        }
        else
        {
            engineSelection += 1;
        }
        tankEngine = Instantiate(tankEngines[engineSelection], transform.position, Quaternion.identity);
    }
}
