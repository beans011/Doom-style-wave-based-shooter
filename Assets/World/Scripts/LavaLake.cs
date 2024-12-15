using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaLake : MonoBehaviour
{
    public PlayerHP playerHP;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            playerHP.TakeDamage(1000);
        }
    }
}
