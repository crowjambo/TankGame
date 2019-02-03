using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_manager : MonoBehaviour {

    /*
        - Quick manager to test sending data to another scene. Purpose = load a customized player tank, ready to play.
    */

    public int bodyValue;
    public int turretValue;
    public int trackValue;
    public int engineValue;

    private void Awake()
    {
        // Make sure data travels across scenes
        DontDestroyOnLoad(this);
    }

    // function to be called before sceneLoad, to receive the final customization values
    public void acquireValues(int body, int turret, int track, int engine)
    {
        bodyValue = body;
        turretValue = turret;
        trackValue = track;
        engineValue = engine;
    }

    
}
