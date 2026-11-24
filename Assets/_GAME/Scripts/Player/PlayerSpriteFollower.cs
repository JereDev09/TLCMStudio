using UnityEngine;

public class PlayerSpriteFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector2 oldPos;

    private float moveTime;
    private float targetTime;

    private void Start()
    {
        targetTime = Time.fixedDeltaTime;
        oldPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveTime += Time.deltaTime;

        transform.position = Vector2.Lerp(oldPos, target.position, moveTime);

        if (moveTime > targetTime)
        {
            moveTime = 0f;
            oldPos = target.position;
        }
    }
}
