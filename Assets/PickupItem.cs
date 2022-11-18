using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Player;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private bool _isKey;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.hasKey = true;
            Destroy(gameObject);
        }
    }
}
