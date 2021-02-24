using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : OffensiveSkill
{
    public GameObject attackObject;
    public float resetDuration = 0.3f; // duration to reset stance
    public float windUpDuration = 0.3f;

    private void Start()
    {
        damage = 10;

        hitBox = GetComponent<Collider>();
        hitBox.enabled = false;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public override IEnumerator Use()
    {
        if (cooldown > 0.0f)
        {
            yield return null;
        }

        yield return StartCoroutine(WindUp());
    }

    public IEnumerator WindUp()
    {
        isInUse = true;

        yield return new WaitForSeconds(windUpDuration);
        meshRenderer.enabled = true;

        hitBox.enabled = true;

        StartCoroutine(Strike());
    }

    public IEnumerator Strike()
    {
        yield return new WaitForSeconds(attackDuration);
        hitBox.enabled = false;
        meshRenderer.enabled = false;

        StartCoroutine(Finish());
    }

    public IEnumerator Finish()
    {
        // Reset stance - mostly for AI so that they don't immediately turn after attacking
        yield return new WaitForSeconds(resetDuration);

        isInUse = false;
        cooldownTimer = cooldown;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTimer);
        cooldownTimer = -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(target))
        {
            Debug.Log(damage + " damage");
        }
    }
}
