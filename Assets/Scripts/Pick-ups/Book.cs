using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Pickup
{
   public int booksGranted;

   public override void Collect()
   {
      if (hasBeenCollected)
      {
         return;
      }
      else
      {
         base.Collect();
      }
      
      PlayerStats player = FindObjectOfType<PlayerStats>();
      player.IncreaseBooks(booksGranted);
   }
}
