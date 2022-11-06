using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tactical;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using UnityEngine;

public class AttackAgent : MonoBehaviour, IAttackAgent {
    [SerializeField] protected UltimateCharacterLocomotion Controller;
    [SerializeField] protected float MaxAttackDistance = 1f;
    [SerializeField] protected float MaxAttackAngle = 60f;
    
    void Start() {
        Controller = GetComponent<UltimateCharacterLocomotion>();
    }
    
    public float AttackDistance() {
        return MaxAttackDistance;
    }

    public bool CanAttack() {
        var useAbility = Controller.GetAbility<Use>()?.CanStartAbility();
        return useAbility == true;
    }

    public float AttackAngle() {
        return MaxAttackAngle;
    }

    public void Attack(Vector3 targetPosition) {
        var useAbility = Controller.GetAbility<Use>();
        if (useAbility != null) {
            Controller.TryStartAbility(useAbility);
        } else {
            Debug.LogWarning("Couldn't find Use ability");
        }
    }
}
