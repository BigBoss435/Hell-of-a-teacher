using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public CharacterScriptableObject characterData;
    public BossDeathCheck bossDeathCheck;
    RageMeter rageMeter;

    //Current Stats
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;
    float currentExperienceRatio;
    int currentBooksRatio;

    #region Current Stats Properties

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
                }
            }
        }
    }
    
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }
    
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move speed: " + currentMoveSpeed;
                }
            }
        }
    }
    
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }
    
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }
    
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }

    public float CurrentExperienceRatio
    {
        get { return currentExperienceRatio; }
        set
        {
            if (currentExperienceRatio != value)
            {
                currentExperienceRatio = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentExperienceRatioDisplay.text = "Experience Ratio: " + currentExperienceRatio;
                }
            }
        }
    }

    public int CurrentBooksRatio
    {
        get { return currentBooksRatio; }
        set
        {
            if (currentBooksRatio != value)
            {
                currentBooksRatio = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentBookRatioDisplay.text = "Books Ratio: " + currentBooksRatio;
                }
            }
        }
    }

    #endregion

    public ParticleSystem damageEffect;
    

    //Experience and level  of the player
    [Header ("Experience/Level")]
    public int experience = 0;
    public float experienceToGive = 0f;
    public int level = 1;
    public int experienceCap;
    public int booksCollected = 0;


    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    //I-frames
    [Header("I-frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;
    public bool damageSound = false;

    public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")] 
    public Image healthBar;
    public Image expBar;
    public Image rageBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bookText;

    public GameObject secondWeaponTest;
    public GameObject firstPassiveItemTest, secondPassiveItemTest;
    
    [Header("Rage Bar stat increases")]
    bool recoveryChanged = false;

    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
        rageMeter = GetComponent<RageMeter>();
        bossDeathCheck = FindObjectOfType<BossDeathCheck>();
        
        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
        GameManager.instance.currentExperienceRatioDisplay.text = "Experience Ratio: " + currentExperienceRatio;
        GameManager.instance.currentBookRatioDisplay.text = "Books Ratio: " + currentBooksRatio;
        
        UpdateRageBar();
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
        UpdateBookText();
    }

    void Update()
    {
        if(invincibilityTimer > 0)
        {
            SetInvincibilityIfRageBarFull();
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }

        if (bossDeathCheck.hasBeenKilled)
        {
            SetResultsIfBossIsDead();
        }
        Recover();
        UpdateStatsBasedOnRage();
    }

    public void IncreaseExperience (float amount)
    {
        experienceToGive = amount * currentExperienceRatio;
        experience += (int)experienceToGive;
        LevelUpChecker();
        UpdateExpBar();
    }
    
    public void IncreaseRage(int amount)
    {
        rageMeter.AddRage(amount);
        UpdateRageBar();
    }

    public void IncreaseBooks(int amount)
    {
        booksCollected = booksCollected + amount * currentBooksRatio;
        UpdateBookText();
    }
    
    public void IncreaseBooksLevelUp(int amount)
    {
        booksCollected += amount;
        UpdateBookText();
        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void IncreaseBooksWithoutRatio(int amount)
    {
        booksCollected += amount;
        UpdateBookText();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel) 
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
            
            UpdateLevelText();
            
            GameManager.instance.StartLevelUp();
        }
    }

    void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText()
    {
        levelText.text = "LV " + level.ToString();
    }

    void UpdateRageBar()
    {
        rageBar.fillAmount = (float)rageMeter.currentRage / rageMeter.rageCap;
    }

    void UpdateBookText()
    {
        bookText.text = booksCollected.ToString();
    }

    public void UpdateStatsBasedOnRage()
    {
        if (rageMeter.currentRage >= 25 && !recoveryChanged)
        {
            CurrentRecovery += 0.1f;
            recoveryChanged = true;
        }
    }

    void Awake()
    {
        inventory = GetComponent<InventoryManager>();
        
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed= characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;
        CurrentExperienceRatio = characterData.ExperienceGrantedRatio;
        currentBooksRatio = characterData.BooksGrantedRatio;
        
        SpawnWeapon(characterData.StartingWeapon);
        SpawnPassiveItem(firstPassiveItemTest);
        //GameManager.instance.AssignChosenCharacterUI(characterData);
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            CurrentHealth -= dmg;
            damageSound = true;

            if (damageEffect) Instantiate(damageEffect, transform.position, Quaternion.identity);

            invincibilityTimer = invincibilityDuration;
            Debug.Log("Non rage" + invincibilityTimer);
            isInvincible = true;

            if (CurrentHealth <= 0)
            {
                Kill();
            }
            
            UpdateHealthBar();
        }
        else
        {
            damageSound = false;
        }
    }

    void SetInvincibilityIfRageBarFull()
    {
        if (rageMeter.currentRage == rageMeter.oldRageCap)
        {
            isInvincible = true;
            invincibilityTimer = 5f;
            Debug.Log(invincibilityTimer);
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
            GameManager.instance.AssignBooksCollectedUI(booksCollected);
        }
    }

    public void SetResultsIfBossIsDead()
    {
        if (bossDeathCheck.hasBeenKilled)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
            GameManager.instance.AssignBooksCollectedUI(booksCollected);
        }
    }

    public void RestoreHealth(float amount)
    {
        if(CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;
            
            if(CurrentHealth > characterData.MaxHealth) 
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += currentRecovery * Time.deltaTime;

            if(CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        //Checking if the slots are full, and returning if it is
        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots are already full");
            return;
        }
        
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }
    
    
    public void SpawnPassiveItem(GameObject passiveItem)
    {
        //Checking if the slots are full, and returning if it is
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory slots are already full");
            return;
        }
        
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
}
