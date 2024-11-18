using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState currentState;


    private float timeBetweetAttackCounter = 0;


    public override Character CharacterTarget => 
        GameManager.Instance.CharacterFactory.Player;


    public override void Initialize()
    {
        base.Initialize();

        LiveComponent = new CharacterLiveComponent();
        LiveComponent.Initialize(this);

        DamageComponent = new CharacterDamageComponent();
    }

    public override void Update()
    {
        switch (currentState)
        {
            case AiState.None:

                break;

            case AiState.MoveToTarget:
                Vector3 direction = CharacterTarget.transform.position - transform.position;
                direction.Normalize();

                MovementComponent.Move(direction);
                MovementComponent.Rotation(direction);


                if (Vector3.Distance(CharacterTarget.transform.position, transform.position) < 3
                    && timeBetweetAttackCounter <= 0) 
                {
                    DamageComponent.MakeDamage(CharacterTarget);
                    timeBetweetAttackCounter = characterData.TimeBetweenAttacks;
                }
                
                if (timeBetweetAttackCounter > 0)
                    timeBetweetAttackCounter -= Time.deltaTime;

                break;
        }
    }

}
