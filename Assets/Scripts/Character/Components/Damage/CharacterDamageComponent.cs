using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageComponent : IDamageComponent
{
    private Character selfCharacter;
    public float Damage => selfCharacter != null ? selfCharacter.CharacterData.WeaponDamage : 0f;

    public void Initialize(Character character)
    {
        selfCharacter = character;
    }

    public void MakeDamage(Character characterTarget)
    {
        if (characterTarget != null && characterTarget.LiveComponent != null)
            characterTarget.LiveComponent.SetDamage(Damage);
    }
}
