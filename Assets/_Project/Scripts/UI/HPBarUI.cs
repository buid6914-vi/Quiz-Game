using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [Header("References")]
    public Slider slider;         
    private CharacterBase target;  

    public void Setup(CharacterBase character)
    {
        target = character;

        slider.maxValue = character.MaxHP;
        slider.value = character.CurrentHP;
    }

    public void LateUpdate()
    {
        if (target == null) return;

        slider.value = target.CurrentHP;

        if (Camera.main != null)
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}