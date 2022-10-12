# 2022-05-SpaceSHMUP
 2022-09 version of SpaceShmup - Starter in 2021.3 LTS
 
 We will use this starter to go through Chapter 30
 
 You will import a package with portions of Chapter 31 already implemented after this exercise is completed

# Gibson-Bond - second edition
## Features ch.30
- collision detection with nested colliders
- old input system, GetAxis()
- reusable component script to set bounds
- spawn prefabs
- destroy GameObjects
- set layers and control how layers interact
- rotate ship on movement to make it juicy
- public property to filter/protect private field (shieldLevel)

## Features added ch.31
- parallax bkgd scroll
- enemy-specific features using subclass overrides
- firing system using function delegate
- global WeaponType enum and WeaponDefinition class
- global Part class for enemy_4
- remove individual components in enemy_4
- changing weapon type using public property (get/set) to get protected field and set calls a function
- bezier curve to interpolate btwn 2 or more points
- powerup system
 - cube rotates and drifts and is attached to a WeaponType so that hero can get specific benefits
 - uses WeaponType enum and WeaponDefinition class to set color, text
 - has specific time to live and then fades out
- manage race conditions by changing Script Execution Order
