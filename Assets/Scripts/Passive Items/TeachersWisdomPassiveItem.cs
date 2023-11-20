using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachersWisdomPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentExperienceRatio *= 1 + passiveItemData.Multiplier / 100f;
    }
}
