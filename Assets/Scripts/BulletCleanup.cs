using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCleanup : MonoBehaviour {


    public float timeToDestroy = 3;

	void Start () {
        Destroy(gameObject,timeToDestroy);
        // replace this with a pool of bullets
	}

}
