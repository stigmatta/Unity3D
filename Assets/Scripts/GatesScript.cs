using UnityEngine;

public class GatesScript : MonoBehaviour
{
    [SerializeField] Vector3 openDirection;
    [SerializeField] private int keyNumber;
    [SerializeField] private float size = 0.68f;
    private float openTime;
    private float openTime1 = 3.0f;
    private float openTime2 = 10.0f;
    private bool isKeyInserted;
    private bool isKeyCollected;
    private bool isKeyInTime;
    private int hitCount;

    void Start()
    {
        isKeyInserted = false;
        isKeyCollected = false;
        isKeyInTime = true;
        hitCount = 0;
        GameEventSystem.Subscribe(OnGameEvent);
    }
    void Update()
    {
        if (isKeyInserted && transform.localPosition.magnitude < size)
        {
            Debug.Log(size);
            transform.Translate(size * Time.deltaTime / openTime * openDirection);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (isKeyCollected)
            {
                isKeyInserted = true;
                openTime = isKeyInTime ? openTime1 : openTime2;
            }
            else
            {
                hitCount += 1;
                if (hitCount == 1)
                {
                    GameEventSystem.EmitEvent(new GameEvent
                    {
                        type = "PlayerHitGates",
                        toast = $"Знайдіть ключ {keyNumber} для відчинення"
                    });
                }
                else
                {
                    GameEventSystem.EmitEvent(new GameEvent
                    {
                        type = "PlayerHitGates",
                        payload = hitCount,
                        toast = $"Я кажу {hitCount}й раз, знайдіть ключ {keyNumber} для відчинення"
                    });
                }
            }
        }

        Debug.Log(collision.gameObject.name);
    }

    private void OnGameEvent(GameEvent gameEvent) {
        if(gameEvent.type == $"isKey{keyNumber}Collected")
        {
            isKeyCollected = true;
            isKeyInTime = (bool)gameEvent.payload;
        }
    }
    private void OnDestroy()
    {
        GameEventSystem.Unsubscribe(OnGameEvent);
    }
}
