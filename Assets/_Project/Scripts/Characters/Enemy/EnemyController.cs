using UnityEngine;

public class EnemyController : CharacterBase
{
    protected override void OnDamaged()
    {
        Debug.Log("Enemy receive damage!");
    }
    public override void Attack(CharacterBase target)
    {
        Debug.Log("Enemy attacks player!");
        target.TakeDamage(10); 
    }
    protected override void Die()
    {
        base.Die();
        Debug.Log("Enemy Defeated!");
        
    }
    
    
    

}
