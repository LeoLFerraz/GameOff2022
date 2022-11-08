using System.Collections;
using Opsive.Shared.Events;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Traits;
using Opsive.UltimateCharacterController.Traits.Damage;
using UnityEngine;

public class Stagger : Ability {
	[SerializeField] protected float TimeStaggered = 0.5f;

	protected Coroutine StopCoroutine;

	public override void Start() {
		base.Start();
		EventHandler.RegisterEvent<DamageData>(m_GameObject, "OnDamage", OnDamageTaken);
	}

	protected override void AbilityStarted() {
		base.AbilityStarted();
		StopCoroutine = m_CharacterLocomotion.StartCoroutine(ScheduledStop());
		m_CharacterLocomotion.UpdateAbilityAnimatorParameters();
	}

	protected IEnumerator ScheduledStop() {
		yield return new WaitForSeconds(TimeStaggered);
		StopAbility();
	}

	protected void OnDamageTaken(DamageData dmgData) {
		m_CharacterLocomotion.TryStartAbility(this);
	}

	public override bool ShouldBlockAbilityStart(Ability startingAbility) {
		return true;
	}

	public override bool ShouldStopActiveAbility(Ability activeAbility) {
		return true;
	}
}
