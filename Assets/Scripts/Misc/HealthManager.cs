using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public bool destroyNoLife = true;

    [SerializeField]
    private int maxHealth;
    private int _health;
    private GameUiManager uiManager;

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    public int Health
    {
        get
        {
            return _health;
        }
        private set
        {
            _health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    void Start()
    {
        Health = maxHealth;
        if (gameObject.CompareTag("Player"))
        {
            uiManager = FindObjectOfType<GameUiManager>();

            if (uiManager == null)
                Debug.LogWarning("HealthManager needs a GameUiManager");
        }
    }

    public bool hit(int damage)
    {
        if (damage <= 0)
            return false;

        Health -= damage;

        if (gameObject.CompareTag("Player"))
            uiManager.updateHealth(Health);

        if (Health <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                GameManager.SharedInstance.endGame();
            }

            if (gameObject.CompareTag("Objetive"))
            {
                GameManager.SharedInstance.endGame();
            }

            Loot loot = GetComponent<Loot>();
            loot?.loot();

            if (destroyNoLife)
                Destroy(gameObject);

            return true;
        }

        return false;
    }

    public void heal(int amount)
    {
        if (amount <= 0)
            return;

        Health += amount;
        uiManager.updateHealth(Health);
    }

    public void changeMaxHealth(int newMaxHealth) {
        if (newMaxHealth == 0)
            return;

        maxHealth = newMaxHealth;
        _health = newMaxHealth;
    }
}
