using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Character playerCharacterPrefab;
    [SerializeField] private Character enemyCharacterPrefab;

    private Dictionary<CharacterType, Queue<Character>> disabledCharacters
        = new Dictionary<CharacterType, Queue<Character>>();

    private List<Character> activeCharacters = new List<Character>();


    public Character Player
    {
        get; private set;
    }

    public List<Character> ActiveCharacters => activeCharacters;


    public Character GetCharacter(CharacterType type)
    {
        Character character = null;
        if (type == CharacterType.Player && Player != null)
        {
            Debug.Log("Player already exists, returning existing instance.");
            return Player;
        }

        if (disabledCharacters.ContainsKey(type))
        {
            if (disabledCharacters[type].Count > 0)
            {
                character = disabledCharacters[type].Dequeue();
            }
        }
        else
        {
            disabledCharacters.Add(type, new Queue<Character>());
        }

        if (character == null)
        {
            character = InstantiateCharacter(type);
            Debug.Log("New character instantiated: " + character.name + " (Type: " + type + ")");
        }

        activeCharacters.Add(character);
        if (type == CharacterType.Player)
        {
            Player = character; // Убеждаемся, что Player обновляется
        }
        return character;
    }

    public void ReturnCharacter(Character character)
    {
        Queue<Character> characters = disabledCharacters[character.CharacterType];
        characters.Enqueue(character);

        activeCharacters.Remove(character);
    }

    private Character InstantiateCharacter(CharacterType type)
    {
        Character character = null;
        switch (type)
        {
            case CharacterType.Player:
                character = GameObject.Instantiate(playerCharacterPrefab, null);
                Player = character;
                break;

            case CharacterType.DefaultEnemy:
                character = GameObject.Instantiate(enemyCharacterPrefab, null);
                break;

            default:
                Debug.LogError("Unknown character type: " + type);
                break;
        }

        return character;
    }
}
