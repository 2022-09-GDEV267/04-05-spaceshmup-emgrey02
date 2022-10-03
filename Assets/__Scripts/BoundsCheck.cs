using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a GameObject on screen.
/// Note that this ONLY works for an orthographic Main Camera at [0,0,0].
/// </summary>
public class BoundsCheck : MonoBehaviour {
    [Header("Set in Inspector")]
    bool placeholder1; // here to keep VS from freaking out - DELETE IT

    [Header("Set Dynamically")]
    bool placeholder2; // here to keep VS from freaking out - DELETE IT

    void Awake()
    {

    }

    void LateUpdate()
    {


    }

    // Draw the bounds in the Scene pane using OnDrawGizmos()
    
}
