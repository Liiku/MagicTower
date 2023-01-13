using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInput playerInput = other.GetComponent<PlayerInput>();
        playerInput.isShopping = true;
    }
}
