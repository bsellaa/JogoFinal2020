using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavGame.Core
{
    public class InstantAttackerGameObject : AttackGameObject
    {
        protected override void Attack(DamageableGameObject target)
        {
            target.TakeDamage(offenseStats.damage);

            if (onAttackStrike != null)
            {
                onAttackStrike(target.damageTransform.position);
            }
        }
    }
}
