using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy_4 will start offscreen and then pick a random point on screen to
/// move to. Once it has arrived, it will pick another random point and
/// continue until the player has shot it down.
/// </summary>
public class Enemy_4 : Enemy
{
    private Vector3 p0, p1; //the two points to interpolate
    private float timeStart; //birth time for this enemy
    private float duration = 4; //duration of movement

    void Start()
    {
        //there's already an initial position chosen by Main.SpawnEnemy()
        //so add it to points as the initial p0 & p1
        p0 = p1 = pos;
        InitMovement();
    }

    void InitMovement()
    {
        p0 = p1; //set p0 to old p1

        //assign a new on-screen location to p1
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;
        p1.x = Random.Range(-widMinRad, widMinRad);
        p1.y = Random.Range(-hgtMinRad, hgtMinRad);

        //reset the time
        timeStart = Time.time;
    }

    public override void Move()
    {
        //this completely overrides Enemy.Move() with a linear interp
        float u = (Time.time - timeStart) / duration;

        if (u >= 1)
        {
            InitMovement();
            u = 0;
        }

        u = 1 - Mathf.Pow(1 - u, 2); //apply ease out easing to u
        pos = (1 - u) * p0 + u * p1; //simple linear interpolation
    }
}
