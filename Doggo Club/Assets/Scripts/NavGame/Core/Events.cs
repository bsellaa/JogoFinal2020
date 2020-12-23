using UnityEngine;

namespace NavGame.Core
{
    // CHARACTER EVENTS

    public delegate void OnAttackStartEvent();

    public delegate void OnAttackCastEvent(Vector3 castPoint);

    public delegate void OnAttackStrikeEvent(Vector3 strikePoint);

    public delegate void OnDamageTakenEvent(Vector3 strikePoint, int amount);

    public delegate void OnHealthChangedEvent(int maxHealth, int currentHealth);

    public delegate void OnDiedEvent();

    // LEVEL EVENTS

    public delegate void OnReportableErrorEvent(string message);

    public delegate void OnWaveUpdateEvent(int totalWaves, int currentWave);

    public delegate void OnWaveCountdownEvent(float remainingTime);

    public delegate void OnDefeatEvent();

    public delegate void OnVictoryEvent();
}