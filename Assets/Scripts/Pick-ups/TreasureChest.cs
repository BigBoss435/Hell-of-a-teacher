using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TreasureChest : MonoBehaviour
{
    InventoryManager inventory;
    PlayerStats player;

    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        player = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            OpenTreasureChest();
            Destroy(gameObject);
        }
    }

    public void OpenTreasureChest()
    {
        int randAmountOfBooksToGive = Random.Range(5, 30);
        player.IncreaseBooksWithoutRatio(randAmountOfBooksToGive);
        GameManager.GenerateFloatingText(randAmountOfBooksToGive.ToString(), transform);
        
        /*
        if (inventory.GetPossibleEvolutions().Count <= 0)
        {
            Debug.LogWarning("No available evolutions");
            return;
        }
        
        WeaponEvolutionBlueprint toEvolve = inventory.GetPossibleEvolutions()[Random.Range(0, inventory.GetPossibleEvolutions().Count)];
        inventory.EvolveWeapon(toEvolve);
        */
    }
}
