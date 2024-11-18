using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalLiveComponent : ILiveComponent
{
    public float MaxHealth => 1;
    public float Health => 1; 

    public event Action<Character> OnCharacterDeath;

    public void Initialize(Character selfCharacter)
    {
        // throw new NotImplementedException();
    }

    public void SetDamage(float damage)
    {
        Debug.Log("I am immortal");
    }
}
