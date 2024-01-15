using System;
using UnityEngine;

public class DenemeAttackRange : MonoBehaviour
{
    public Transform transform;
    public float range;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}