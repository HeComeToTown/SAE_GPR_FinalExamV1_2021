using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WallSpellDescription : SpellDescription
{
    [Header("Wall")]
    public GameObject WallPrefab;
    public float WallSpawnDelay;
}
