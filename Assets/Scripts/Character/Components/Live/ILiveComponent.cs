using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveComponent
{
    float MaxHealth { get; }

    float Health { get; }

    public void SetDamage(float damage);
}
