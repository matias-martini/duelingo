using UnityEngine;

public class EquippedAnimationController : MonoBehaviour
{
    public GameObject weapon; // Reference to the weapon GameObject

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Initially, disable the weapon
        ToggleWeapon(false);
    }

    // Call this function when the player equips a weapon
    public void EquipWeapon()
    {
        animator.SetBool("HasWeapon", true);
        // Enable the weapon
        ToggleWeapon(true);
    }

    // Call this function when the player unequips a weapon
    public void UnequipWeapon()
    {
        animator.SetBool("HasWeapon", false);
        // Disable the weapon
        ToggleWeapon(false);
    }

    // Toggle the visibility of the weapon
    private void ToggleWeapon(bool state)
    {
        if (weapon != null)
        {
            weapon.SetActive(state);
        }
    }
}