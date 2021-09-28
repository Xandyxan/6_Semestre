using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestBT : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private GameObject lamparina;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) lamparina.SetActive(!lamparina.activeInHierarchy);
    }
    public void TakeDamage(int damage)
    {
        lives -= damage;

        if(lives <= 0)
        {
            Destroy(gameObject);
        }
    }
}
