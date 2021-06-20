using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;
    public Stat attackRange;
    public Stat attackSpeed;
    public Stat HealtRegen;

    private float attackCooldown = 0f;
    public HealthBar healthBar;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        StartCoroutine(HealthRegeneration());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }

        attackCooldown -= Time.deltaTime;
    }

    public void TakeDamage (int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;

        UpdateHealtBar();
        Debug.Log(transform.name + " take" + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack(CharacterStat targetStat)
    {
        if (attackCooldown <= 0f) 
        {
            targetStat.TakeDamage(damage.GetValue());
            attackCooldown = 1f / attackSpeed.GetValue();
        }
    }

    public virtual void Die()
    {
        // Die in some way

        Debug.Log(transform.name + " died.");
        //StartCoroutine("Respawn", 2f);
    }

    void UpdateHealtBar()
    {
        healthBar.UpdateHealth((float)currentHealth / (float)maxHealth);
    }

    IEnumerator Respawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        //transform = initialPosition;
        //gameObject.SetActive(true);
    }

    IEnumerator HealthRegeneration()
    {
        while (true)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += HealtRegen.GetValue();

                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                UpdateHealtBar();
                Debug.Log("Health Regen");
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
