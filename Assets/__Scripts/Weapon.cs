using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an enum of the various possible weapon types.
/// It also includes a "shield" type to allow a shield power-up.
/// Items marked [NI] below are Not Implemented in the IGDPD book.
/// </summary>

public enum WeaponType
{
    none, //default/ no weapon
    blaster, //simple blaster
    spread, //two shots simultaneously
    phaser, //[NI] shots that move in waves
    missile, //[NI] Homing missiles
    laser, //[NI] Damage over time
    shield //raise shieldLvel
}

/// <summary>
/// The WeaponDefinition class allows you to set the properties
/// of a specific weapon in the Inspector. The Main class has
/// an array of WeaponDefinitions that makes this possible.
/// </summary>

[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter; //letter to show on power-up
    public Color color = Color.white; //color of collar + power-up
    public GameObject projectilePrefab; //prefab for projectiles
    public Color projectileColor = Color.white;
    public float damageOnHit = 0; //amount of damage caused
    public float continuousDamage = 0; //damage per second (laser)
    public float delayBetweenShots = 0;
    public float velocity = 20; //speed of projectiles
}
public class Weapon : MonoBehaviour
{
    
}
