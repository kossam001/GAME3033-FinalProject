using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public int damage = 5;
    public GameObject owner;

    private void OnTriggerEnter(Collider other)
    {
        switch (owner.tag)
        {
            case "Player":
                if (other.CompareTag("Enemy"))
                    other.GetComponent<CharacterData>().UpdateHealth(damage);

                break;
            case "Enemy":
                if (other.CompareTag("Player"))
                    other.GetComponent<CharacterData>().UpdateHealth(damage);

                break;
        }
    }
}
