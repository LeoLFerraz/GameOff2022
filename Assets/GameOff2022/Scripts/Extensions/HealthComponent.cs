using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tactical;
using Opsive.Shared.Events;
using Opsive.UltimateCharacterController.Traits;
using Opsive.UltimateCharacterController.Traits.Damage;
using UnityEngine;

public class HealthComponent : CharacterHealth, IDamageable {
    public override void OnDamage(DamageData damageData) {
        base.OnDamage(damageData);
        EventHandler.ExecuteEvent(m_GameObject, "OnDamage", damageData);
    }
}
