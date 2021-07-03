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

    protected ParticleSystem particles;

    private void Awake()
    {
        currentHealth = maxHealth;
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
    }

    private void Start()
    {
        //StartCoroutine(HealthRegeneration());  // later open it
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    TakeDamage(10);
        //}

        attackCooldown -= Time.deltaTime;
    }

    public virtual void TakeDamage (ref int damage, Transform attacker)
    {

        if (currentHealth <= 0)
            return;

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        if (currentHealth < damage)
            damage = currentHealth;

        currentHealth -= damage;

        UpdateHealtBar();
        //Debug.Log(transform.name + " take: " + damage + " damage.");

        

        if (particles.isStopped)
            particles.Play(); //TODO: make it more efficient
    }

    public void Attack(CharacterStat targetStat)
    {
        if (attackCooldown <= 0f) 
        {
            int damageVal = damage.GetValue();
            targetStat.TakeDamage(ref damageVal, transform);
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
                //Debug.Log("Health Regen");
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
