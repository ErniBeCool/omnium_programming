using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private CharacterType characterType;

    [SerializeField]
    protected CharacterData characterData;


    public virtual Character CharacterTarget {  get; }
    public CharacterType CharacterType => characterType;
    public CharacterData CharacterData => characterData;
    public IMovable MovementComponent { get; protected set; }
    public ILiveComponent LiveComponent { get; protected set; } 
    public IDamageComponent DamageComponent { get; protected set; }
    public CharacterType Type { get; set; }

    public virtual void Initialize()
    {
        MovementComponent = new CharacterMovementComponent();
        MovementComponent.Initialize(characterData);

    }

    public abstract void Update();
}
