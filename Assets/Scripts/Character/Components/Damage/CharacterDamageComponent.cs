using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageComponent : IDamageComponent
{
    public float Damage => 10;

    public void MakeDamage(Character characterTarger)
    {
        if (characterTarger.LiveComponent != null)
        characterTarger.LiveComponent.SetDamage(Damage);
    }
}
