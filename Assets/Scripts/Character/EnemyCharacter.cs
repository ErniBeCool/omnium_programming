using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState currentState;


    private float timeBetweetAttackCounter = 0;


    public override Character CharacterTarget => GameManager.Instance.CharacterFactory.Player;


    public override void Initialize()
    {
        base.Initialize();
        LiveComponent = new CharacterLiveComponent();
        LiveComponent.Initialize(this);
        DamageComponent = new CharacterDamageComponent();
        DamageComponent.Initialize(this); // Добавляем инициализацию
    }

    public override void Update()
    {
        switch (currentState)
        {
            case AiState.MoveToTarget:
                Vector3 targetPos;
                // Если игрок жив и активен, преследуем его
                if (CharacterTarget != null && CharacterTarget.gameObject.activeSelf)
                {
                    targetPos = CharacterTarget.transform.position;
                }
                // Иначе, если база существует и активна, идём к ней
                else if (GameManager.Instance.PlayerBase != null && GameManager.Instance.PlayerBase.gameObject.activeSelf)
                {
                    targetPos = GameManager.Instance.PlayerBase.transform.position;
                }
                else
                {
                    return; // Если ни игрока, ни базы нет, ничего не делаем
                }

                Vector3 direction = targetPos - transform.position;
                direction.Normalize();

                MovementComponent.Move(direction);
                MovementComponent.Rotation(direction);

                if (Vector3.Distance(targetPos, transform.position) < 3 && timeBetweetAttackCounter <= 0)
                {
                    if (CharacterTarget != null && CharacterTarget.gameObject.activeSelf)
                        DamageComponent.MakeDamage(CharacterTarget);
                    else if (GameManager.Instance.PlayerBase != null && GameManager.Instance.PlayerBase.gameObject.activeSelf)
                        GameManager.Instance.PlayerBase.TakeDamage(DamageComponent.Damage);
                    timeBetweetAttackCounter = characterData.TimeBetweenAttacks;
                }

                if (timeBetweetAttackCounter > 0)
                    timeBetweetAttackCounter -= Time.deltaTime;
                break;
        }
    }
}