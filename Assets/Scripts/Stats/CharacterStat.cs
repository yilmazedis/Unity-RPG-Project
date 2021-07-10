using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStat : MonoBehaviour
{
    public int currentHealth { get; private set; }

    public Stats stats;

    private float attackCooldown = 0f;
    public HealthBar healthBar;

    public Text levelText;

    protected ParticleSystem particles;

    private void Awake()
    {
        currentHealth = stats.maxHealth.GetValue();
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
    }

    private void Start()
    {
        StartCoroutine(HealthRegeneration());  //TODO: later open it
        levelText.text = "1"; // TODO: connect with database
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

        damage -= stats.armor.GetValue();
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
            int damageVal = stats.damage.GetValue();
            targetStat.TakeDamage(ref damageVal, transform);
            attackCooldown = 1f / stats.attackSpeed.GetValue();
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
        healthBar.UpdateHealth((float)currentHealth / (float)stats.maxHealth.GetValue());
    }

    IEnumerator Respawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        //transform = initialPosition;
        //gameObject.SetActive(true);
    }

    protected IEnumerator HealthRegeneration()
    {
        while (true)
        {
            if (currentHealth < stats.maxHealth.GetValue())
            {
                currentHealth += stats.healtRegen.GetValue();

                if (currentHealth > stats.maxHealth.GetValue())
                {
                    currentHealth = stats.maxHealth.GetValue();
                }
                UpdateHealtBar();
                //Debug.Log("Health Regen");
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
