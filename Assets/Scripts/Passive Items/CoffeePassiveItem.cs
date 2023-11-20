using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CoffeePassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMoveSpeed *= 1 + passiveItemData.Multiplier / 100f;
    }
}
