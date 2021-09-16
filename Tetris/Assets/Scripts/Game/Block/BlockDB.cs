using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Create Block DB")]
public class BlockDB : ScriptableObject
{
    public BlockView[] blocksPrefabs;
    public float fallTime; // gravityForce;
}
