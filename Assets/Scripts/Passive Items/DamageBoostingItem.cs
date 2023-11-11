using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageBoostingItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMight *= 1 + passiveItemData.Multiplier / 100f;
    }
}
