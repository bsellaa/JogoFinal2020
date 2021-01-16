using UnityEngine;
using UnityEngine.AI;
using NavGame.Core;

namespace NavGame.Animation
{
    [RequireComponent(typeof(AttackGameObject))]

    public class AttackAnimationController : BasicAnimationController
    {
        protected AttackGameObject attackGameObject;
        protected InteractWithObject interactWithObject;

        protected override void Awake()
        {
            base.Awake();
            attackGameObject = GetComponent<AttackGameObject>();
            interactWithObject = GetComponent<InteractWithObject>();
        }

        protected override void Update()
        {
            base.Update();
            animator.SetBool("isInCombat", attackGameObject.isInCombat);
            animator.SetBool("isPunching", interactWithObject.isPunching);
            animator.SetBool("isRunning", interactWithObject.isRunning);
            animator.SetBool("isCrunching", interactWithObject.isCrunching);
            animator.SetBool("startedInteraction", interactWithObject.startedInteraction);
        }

        void OnEnable()
        {
            attackGameObject.onAttackStart += OnAttackStart;
        }

        void OnAttackStart()
        {
            animator.SetTrigger("attack");
        }
    }
}