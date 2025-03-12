using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsService : MonoBehaviour
{
    [SerializeField] private GameObject experienceItemPrefab; // Префаб предмета опыта
    private Queue<Item> itemPool = new Queue<Item>(); // Пул неактивных предметов
    private List<Item> activeItems = new List<Item>(); // Список активных предметов

    // Инициализация сервиса
    public void Initialize()
    {
        // Можно заранее создать несколько предметов в пуле, но для простоты оставим пустым
    }

    // Получение или создание предмета
    public Item SpawnItem(Vector3 position)
    {
        Item item;

        // Если в пуле есть предмет, берём его
        if (itemPool.Count > 0)
        {
            item = itemPool.Dequeue();
        }
        else
        {
            // Иначе создаём новый
            GameObject itemObj = Instantiate(experienceItemPrefab);
            item = itemObj.GetComponent<Item>();
        }

        // Инициализируем предмет
        item.Initialize(position, GameManager.Instance.CharacterFactory.Player.transform);
        activeItems.Add(item);
        return item;
    }

    // Возвращение предмета в пул
    public void ReturnItem(Item item)
    {
        activeItems.Remove(item);
        itemPool.Enqueue(item);
    }

    // Обновление всех активных предметов
    private void Update()
    {
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            Item item = activeItems[i];
            item.UpdateItem();

            // Если предмет неактивен (подобран), возвращаем его в пул
            if (!item.gameObject.activeSelf)
            {
                ReturnItem(item);
            }
        }
    }
}
