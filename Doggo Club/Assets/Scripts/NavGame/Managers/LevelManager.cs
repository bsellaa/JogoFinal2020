using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Core;

namespace NavGame.Managers
{
    public abstract class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public Action[] actions;
        public string errorSound;
        public OnReportableErrorEvent onReportableError;
        public OnWaveUpdateEvent onWaveUpdate;
        public OnWaveCountdownEvent onWaveCountdown;
        public OnDefeatEvent onDefeat;
        public OnVictoryEvent onVictory;

        public bool isPaused { get; private set; } = false;

        protected int selectedAction = -1;

        DamageableGameObject player;

        protected virtual void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            GameObject obj = GameObject.FindWithTag("Player");
            player = obj.GetComponent<DamageableGameObject>();
        }

        void OnEnable()
        {
            player.onDied += EmitDefeatEvent;
        }

        protected virtual void Start()
        {
            StartCoroutine(SpawnBad());
        }

        void EmitDefeatEvent()
        {
            if (onDefeat != null)
            {
                onDefeat();
            }
        }

        protected void EmitVictoryEvent()
        {
            if(onVictory != null)
            {
                onVictory();
            }
        }

        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0;
        }

        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f;
        }

        protected abstract IEnumerator SpawnBad();
    }
}
