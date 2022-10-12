using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour {

    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;
    public float showDamageDuration = 0.1f; //# seconds to show damage
    public float powerUpDropChance = 1f; //change to drop a powerup

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials; //all the materials of this & its children
    public bool showingDamage = false;
    public float damageDoneTime; //time to stop showing damage
    public bool notifiedOfDestruction = false; 

    protected BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();

        //get materials and colors for this gameObject and its children
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    // This is a property: A method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Update()
    {
        Move();

        if (showingDamage && Time.time > damageDoneTime)
        {
            UnShowDamage();
        }

        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }

    // moves enemy downward Y direction
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero":
                Projectile p = otherGO.GetComponent<Projectile>();

                //if this enemy is off screen, don't damage it
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }

                //hurt this enemy
                ShowDamage();
                //get damage amount from Main WEAP_DICT
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                {
                    //tell the main singleton that this ship was destroyed
                    if (!notifiedOfDestruction)
                    {
                        Main.S.shipDestroyed(this);
                    }
                    notifiedOfDestruction = true;

                    //destroy this enemy
                    Destroy(this.gameObject);
                }
                Destroy(otherGO);
                break;

            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name);
                break;
        }
    }

    void ShowDamage()
    {
        foreach(Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}
