using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stat Modifiers/Health Modifier" , fileName = "New Health")]
public class CharacterHealthModifier : CharacterStatModifier
{
    public override void AffectCharacter(GameObject charcter, float val)
    {
        PlayerHealth health = charcter.GetComponent<PlayerHealth>();
            if (health != null)
                health.AddHealth((int)val);
    }
}
