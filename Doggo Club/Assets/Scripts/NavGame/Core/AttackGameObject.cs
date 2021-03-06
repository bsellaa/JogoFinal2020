﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavGame.Managers;

namespace NavGame.Core
{
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class AttackGameObject : TouchableGameObject
	{
		public OffenseStats offenseStats;
		public float attackRange = 4f;
		public float attackDelay = 0.5f;
		public Transform castTransform;
		public string[] enemyLayers;
		public bool isInCombat { get; private set; }

		[SerializeField]
		protected List<DamageableGameObject> enemiesToAttack = new List<DamageableGameObject>();

		DamageableGameObject enemyDamageable;

		protected NavMeshAgent agent;

		float cooldown = 0f;

		LayerMask enemyMask;

		public OnAttackStartEvent onAttackStart;
		public OnAttackCastEvent onAttackCast;
		public OnAttackStrikeEvent onAttackStrike;

		GameObject obj;

		protected virtual void Awake()
		{
			if(gameObject.tag != "Enemy") obj = GameObject.FindWithTag("Enemy");
			else obj = GameObject.FindWithTag("Player");

			if(obj != null) enemyDamageable = obj.GetComponent<DamageableGameObject>();
			agent = GetComponent<NavMeshAgent>();
			enemyMask = LayerMask.GetMask(enemyLayers);

			if(castTransform == null)
			{
				castTransform = transform;
			}
		}

		protected virtual void Update()
		{
			DecreaseAttackCooldown();
			UpdateAttack();
		}

		protected virtual void UpdateAttack()
		{
			if(obj != null){
				if (IsInRange(obj.gameObject.transform.position))
				{
					FaceObjectFrame(obj.gameObject.transform);
					AttackOnCooldown(enemyDamageable);
					isInCombat = true;
					return;
				}
			}
			isInCombat = false;
		}

		public void AttackOnCooldown(DamageableGameObject target)
		{
			if (cooldown <= 0f)
			{
				cooldown = 1f / offenseStats.attackSpeed;

				if(onAttackStart != null)
				{
					onAttackStart();
				}
				StartCoroutine(AttackAfterDelay(target, attackDelay));
			}
		}

		IEnumerator AttackAfterDelay(DamageableGameObject target, float delay)
		{
			yield return new WaitForSeconds(delay);
			if(target != null)
			{
				if(onAttackCast != null)
				{
					onAttackCast(castTransform.position);
				}
				Attack(target);
			}
		}

		void DecreaseAttackCooldown()
		{
			if (cooldown == 0f)
			{
				return;
			}
			cooldown -= Time.deltaTime;
			if (cooldown < 0f)
			{
				cooldown = 0f;
			}
		}

		public bool IsInRange(Vector3 point)
		{
			float distance = Vector3.Distance(transform.position, point);
			return distance <= attackRange;
		}

		protected override void OnDrawGizmosSelected()
		{
			base.OnDrawGizmosSelected();
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, attackRange);
		}

		protected abstract void Attack(DamageableGameObject target);
	}
}
