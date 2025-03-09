using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageComponent
{
    float Damage { get; }
    void Initialize(Character character); // ��������� ����� Initialize
    void MakeDamage(Character characterTarget);
}
