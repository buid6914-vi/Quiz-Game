using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected int maxHP = 100;
    protected int currentHP;

    [Header("UI")]
    [SerializeField] private HPBarUI hpBarPrefab;
    [SerializeField] private Transform hpSpawnPoint;
    private HPBarUI hpBarInstance;

    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;

    protected virtual void Awake()
    {
        currentHP = maxHP;
        SpawnHPBar();
    }

    private void SpawnHPBar()
    {
        if (hpBarPrefab == null) return;
        Transform parent = hpSpawnPoint != null ? hpSpawnPoint : transform;
        hpBarInstance = Instantiate(hpBarPrefab, parent);
        hpBarInstance.transform.localPosition = Vector3.zero;
        hpBarInstance.Setup(this);
    }

    public virtual void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        OnDamaged();
        if (currentHP <= 0) Die();
    }

    public virtual void Attack(CharacterBase target) { }

    protected virtual void OnDamaged() { /* Animation/VFX here */ }
    protected virtual void Die() { Debug.Log($"{gameObject.name} Died"); }
    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPBar(); 
    }
    public void ResetHP()
    {
        currentHP = maxHP; 
        UpdateHPBar();
        
    }

public void UpdateHPBar()
{
    if (hpBarInstance != null)
    {
        hpBarInstance.LateUpdate();
    }
}
}