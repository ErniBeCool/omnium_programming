using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Character target;

    public void Update()
    {
        if (target != null) transform.position = target.transform.position + new Vector3(5, 20, -20);
        else
        {
            target = GameManager.Instance.CharacterFactory.ActiveCharacters.Find(character => character.Type == CharacterType.Player);
        }
    }
}
