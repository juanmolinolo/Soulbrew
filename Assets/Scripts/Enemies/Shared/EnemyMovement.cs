using Assets.Scripts.Enums;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private float movementSpeed = 5f;

    private Direction moveDirection = Direction.Idle;
    private Direction lastDirectionPressed;

    void Update()
    {
        SetLastDirectionPressed();

        bool leftCurrentlyPressed = Input.GetKey(KeyCode.G);
        bool rightCurrentlyPressed = Input.GetKey(KeyCode.H);

        if (leftCurrentlyPressed && !rightCurrentlyPressed)
        {
            MovePlayer(Direction.Left);
        }
        else if (rightCurrentlyPressed && !leftCurrentlyPressed)
        {
            MovePlayer(Direction.Right);
        }
        else if (rightCurrentlyPressed && leftCurrentlyPressed)
        {
            MovePlayer(lastDirectionPressed);
        }
        else
        {
            moveDirection = Direction.Idle;
        }

        animator.SetInteger("MoveDirection", (int)moveDirection);

    }

    private void SetLastDirectionPressed()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            lastDirectionPressed = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            lastDirectionPressed = Direction.Right;
        }
    }

    private void MovePlayer(Direction direction)
    {
        Vector2 currentPosition = transform.position;

        if (direction == Direction.Left)
        {
            currentPosition.x -= movementSpeed * Time.deltaTime;
        }
        else if (direction == Direction.Right)
        {
            currentPosition.x += movementSpeed * Time.deltaTime;
        }

        transform.position = currentPosition;
        moveDirection = direction;
    }
}
