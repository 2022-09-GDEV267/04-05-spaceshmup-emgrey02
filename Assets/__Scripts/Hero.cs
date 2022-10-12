using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    static public Hero S; // Singleton

    [Header("Set in Inspector")]
    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;

    //holds ref to last triggering GameObject
    private GameObject lastTriggerGo = null;

    //declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();

    //Create a WeaponFireDelegate field named fireDelegate
    public WeaponFireDelegate fireDelegate;

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }

        fireDelegate += TempFire;
    }
	
	// Update is called once per frame
	void Update()
    {
        // pull in info from Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        //rotate ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        //use the fireDelegate to fire Weapons
        //first, make sure the button is pressed: Axis('Jump')
        //then ensure that fireDelegate isn't null to avoid an error
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        Projectile proj = projGO.GetComponent<Projectile>();
        proj.type = WeaponType.spread;
        float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
        rigidB.velocity = Vector3.up * tSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        
        //make sure it's not same triggering go as last time
        if (go == lastTriggerGo)
        {
            return;
        }

        lastTriggerGo = go;

        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        } else
        {
            print("Triggered by non-Enemy " + go.name);
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);

            //if shield is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);

                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

}
