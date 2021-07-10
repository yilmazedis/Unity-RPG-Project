using UnityEngine;

public class PlayerStat : CharacterStat
{

    private LifeCycleManager playerLife;

    public ExperienceBar experiencebar; 
    int targetExperience { get { return stats.level.GetValue() * 100; } }
    int experience = 0;

    public StatsPanel statsPanel;

    private void Start()
    {
        StartCoroutine(HealthRegeneration());  //TODO: later open it
        levelText.text = "1"; // TODO: connect with database
        statsPanel.updateStats(stats);
    }

    public override void TakeDamage(ref int damage, Transform attacker)
    {

        base.TakeDamage(ref damage, attacker);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void gainedExperience(int exp)
    {
        experience += exp;
            
        if (experience >= targetExperience)
        {
            
            experience = experience - targetExperience;
            UpdateExperienceBar();
            stats.level.IncreaseValue(1);
            stats.damage.IncreaseValue(2);
            stats.armor.IncreaseValue(1);
            stats.attackSpeed.IncreaseValue(1);
            
            Debug.Log(gameObject.tag + " level: " + stats.level.GetValue());

            levelText.text = stats.level.GetValue().ToString();

            statsPanel.updateStats(stats);
        } else
        {
            UpdateExperienceBar();
        }

        
        Debug.Log(gameObject.tag + " exp: " + experience);
    }

    void UpdateExperienceBar()
    {
        experiencebar.UpdateExperienceBar((float)experience / (float)targetExperience);
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
        playerLife = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LifeCycleManager>();

        playerLife.RespawnPlayer();

    }
}
