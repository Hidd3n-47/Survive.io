using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerSurvival : MonoBehaviour
{
    public static GameManagerSurvival Instance { get; private set; }

    [SerializeField]
    private Transform m_healthPickupPrefab;

    private GameObject m_player;
    private Player m_playerClass;

    private int m_abilityWaveIndex = 0;

    private int m_numEnemiesRequired = 0;
    private int m_numEnemiesKilled = 0;

    private float m_damageMultiplier = 1.0f;
    private float m_healthDropPercentage = 0.0f;
    private float m_speedBoost = 1.0f;
    private float m_defense = 1.0f;

    public float DamageMultiplier { get { return m_damageMultiplier; } set { m_damageMultiplier = value; } }
    public float HealthDropPercentage {  get { return m_healthDropPercentage; } set { m_healthDropPercentage = value; } }
    public float SpeedBoost {  get { return m_speedBoost; } set { m_speedBoost = value; } }
    public float Defense {  get { return m_defense; } set { m_defense = value; } }

    public UnityEvent PlayerHeal;

    public void SubscribeToOnDeathEvent(IEnemy enemy) 
    { 
        enemy.OnDeath.AddListener(IncrementNumEnemiesKilled);
        enemy.OnDeathWithPosition.AddListener(CalculateHealthPickup);
    }

    public void HealPlayer(float amount)
    {
        m_playerClass.Heal(amount);
        PlayerHeal?.Invoke();
    }

    private void Awake()
    {
        Instance = this;

        m_player = GameObject.Find("Player");
        m_playerClass = m_player.GetComponent<Player>();

        Transform gun = Instantiate(GunManager.Instance.GetPrefab(PlayerStats.Instance.GetEquipped()), Vector3.zero, Quaternion.identity);
        gun.parent = m_player.transform.GetChild(0);
        gun.localPosition = Vector3.zero;

        CalculateEnemiesForAbility();
    }

    public void IncrementNumEnemiesKilled()
    {
        if(++m_numEnemiesKilled >= m_numEnemiesRequired)
        {
            CalculateEnemiesForAbility();

            AbilityManager.Instance.GivePlayerAbilityChoice();
        }
    }

    private void CalculateHealthPickup(Vector2 position)
    {
        float rand = Random.Range(0.0f, 1.0f);

        if(rand < m_healthDropPercentage)
        {
            Instantiate(m_healthPickupPrefab, (Vector3)position, Quaternion.identity);
        }
    }

    private void CalculateEnemiesForAbility()
    {
        m_numEnemiesRequired = (int)(30 * Mathf.Log(++m_abilityWaveIndex) + 15);
        m_numEnemiesKilled = 0;
    }
}
