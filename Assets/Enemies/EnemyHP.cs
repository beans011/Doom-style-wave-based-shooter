using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float enemyHP;
    public GameObject blood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0)
        {
            blood.SetActive(true);
            Invoke(nameof(DestroyObject), 2.0f);
        }
    }

    public void TakeDamage(int amount)
    {
        Debug.Log(amount);
        enemyHP = enemyHP - amount;
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
