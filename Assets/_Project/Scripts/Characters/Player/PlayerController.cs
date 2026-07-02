using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterBase
{
    
    
    protected override void OnDamaged()
    {
        
        Debug.Log("Player receive damage!");
    }
    public override void Attack(CharacterBase target)
    {
        Debug.Log("Player attacks enemy!");
        target.TakeDamage(15); // Sát thương của Player
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Player Lose!");
    }
    
}