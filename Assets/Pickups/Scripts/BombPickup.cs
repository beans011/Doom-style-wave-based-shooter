using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    public LayerMask whatIsEnemy;
    public GameObject bombExplosion;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaycastHit hit;

            if (Physics.SphereCast(gameObject.transform.position, 50.0f, transform.forward, out hit, whatIsEnemy))
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<EnemyMovement>().TakeDamage(damage); //change for when enemy implemented
                    Debug.Log("Hit");
                }
            }

            Instantiate(bombExplosion, transform.position, Quaternion.identity);
        }       
    }
}
