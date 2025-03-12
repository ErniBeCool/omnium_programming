using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float speed = 5f; 
    private Transform playerTarget; 
    private bool isMovingToPlayer = false; 
    private float pickupDistance = 2f; 


    public virtual void Initialize(Vector3 position, Transform player)
    {
        transform.position = position; 
        playerTarget = player; 
        isMovingToPlayer = false;
        pickupDistance = 2f + GameManager.Instance.SkillService.PickupRangeBonus;
        gameObject.SetActive(true); 
    }

    public void UpdateItem()
    {
        if (playerTarget == null) return;

        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (!isMovingToPlayer && distance <= pickupDistance)
        {
            isMovingToPlayer = true;
        }

        if (isMovingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
            if (distance < 0.1f) 
            {
                OnPickedUp(); 
            }
        }
    }

    protected abstract void OnPickedUp();
}