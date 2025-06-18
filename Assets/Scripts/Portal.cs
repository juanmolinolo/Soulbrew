using Assets.Scripts.Constants;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstants.PLAYER))
        {
            Debug.Log("Player has entered the portal.");
        }
    }
}
