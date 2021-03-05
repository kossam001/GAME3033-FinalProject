using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private CharacterData character;

    private Interactable interactable;

    private void Awake()
    {
        character = GetComponent<CharacterData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            interactable = other.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            interactable = null;
        }
    }

    public void Use()
    {
        if (interactable != null)
            interactable.Use(character);
    }
}
