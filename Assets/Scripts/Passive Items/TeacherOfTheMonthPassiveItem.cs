using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherOfTheMonthPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentBooksRatio = (int)passiveItemData.Multiplier;
    }
}
