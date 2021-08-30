using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Block", menuName = "Create Block")]
public class BlockContainer : ScriptableObject
{
    public Block Prefab_Block;
    public RectTransform BlockUI;
}
