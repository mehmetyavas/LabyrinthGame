using System;
using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public class EnemyManager : MonoBehaviour, IDamageAble, IDieAble
    {
        public float Health { get; set; }


        public void TakeDamage(float damageTaken)
        {
            if (Health - damageTaken <= 0)
            {
                Health = 0;
                Die(1f);
            }

            Health -= damageTaken;


            Debug.Log(
                $"Player hit the target and cause {damageTaken} damage. The target is :{gameObject.name}. Current Health : {Health}");
        }


        public void Die(float waitBeforeDestroy)
        {
            Debug.Log($"Player killed the target. The target is :" + gameObject.name);
            Destroy(gameObject, waitBeforeDestroy);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            if (other.gameObject.TryGetComponent(out IDamageAble hit))
            {
                hit.TakeDamage(5);
            }
        }
    }
}