using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.1f;

    [Header("Set Dynamically")]
    public int levelShown = 0;

    Material mat;



    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update()
    {
       //read current shield level from the Hero Singleton
       int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);

       //if it's different from levelShown...
       if (levelShown != currLevel)
       {
            levelShown = currLevel;

            //adjust texture offset to show diff shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
       }

        //rotate shield a bit every frame in time-based way
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
