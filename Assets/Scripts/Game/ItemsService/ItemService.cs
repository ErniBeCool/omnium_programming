using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsService : MonoBehaviour
{
    [SerializeField] private GameObject experienceItemPrefab; // ������ �������� �����
    private Queue<Item> itemPool = new Queue<Item>(); // ��� ���������� ���������
    private List<Item> activeItems = new List<Item>(); // ������ �������� ���������

    // ������������� �������
    public void Initialize()
    {
        // ����� ������� ������� ��������� ��������� � ����, �� ��� �������� ������� ������
    }

    // ��������� ��� �������� ��������
    public Item SpawnItem(Vector3 position)
    {
        Item item;

        // ���� � ���� ���� �������, ���� ���
        if (itemPool.Count > 0)
        {
            item = itemPool.Dequeue();
        }
        else
        {
            // ����� ������ �����
            GameObject itemObj = Instantiate(experienceItemPrefab);
            item = itemObj.GetComponent<Item>();
        }

        // �������������� �������
        item.Initialize(position, GameManager.Instance.CharacterFactory.Player.transform);
        activeItems.Add(item);
        return item;
    }

    // ����������� �������� � ���
    public void ReturnItem(Item item)
    {
        activeItems.Remove(item);
        itemPool.Enqueue(item);
    }

    // ���������� ���� �������� ���������
    private void Update()
    {
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            Item item = activeItems[i];
            item.UpdateItem();

            // ���� ������� ��������� (��������), ���������� ��� � ���
            if (!item.gameObject.activeSelf)
            {
                ReturnItem(item);
            }
        }
    }
}
