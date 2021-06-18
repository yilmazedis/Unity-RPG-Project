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

    private float attackCooldown = 0f;

    public HealthBar healthBar;

    private void Awake()
    {
        currentHealth = maxHealth;
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

        healthBar.UpdateHealth((float)currentHealth / (float)maxHealth);
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

    IEnumerator Respawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        //transform = initialPosition;
        gameObject.SetActive(true);
    }
}
