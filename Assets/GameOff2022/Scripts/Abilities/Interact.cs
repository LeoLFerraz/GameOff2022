using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character.Abilities;
using UnityEngine;

[DefaultStartType(AbilityStartType.ButtonDown)]
[DefaultStopType(AbilityStopType.Automatic)]
[DefaultInputName("Use")]
public class Interact : Ability {
    [SerializeField] protected Vector3 RaycastOriginOffset;
    [SerializeField] protected LayerMask RaycastLayerMask;
    [SerializeField] protected float RaycastDistance;
    [SerializeField] protected int MaxTriggers = 1;
    [SerializeField] protected bool LogHits;
    protected override void AbilityStarted() {
        var hits = Physics.RaycastAll(m_CharacterLocomotion.transform.position + RaycastOriginOffset, m_CharacterLocomotion.transform.forward, 100f,
            RaycastLayerMask, QueryTriggerInteraction.Collide);
        int triggers = 0;
        foreach (var hit in hits) {
            if (triggers >= MaxTriggers) return;
            var interactable = hit.collider.GetComponent<IInteractable>();
            if(interactable == null) interactable = hit.collider.transform.parent.GetComponent<IInteractable>();
            if(interactable == null) interactable = hit.collider.GetComponentInChildren<IInteractable>();
            if (interactable != null) {
                interactable.OnInteract(m_CharacterLocomotion);
                triggers++;
            }
            if (LogHits) {
                Debug.Log(hit.collider.name);
            }
        }
    }

    public override void OnDrawGizmosSelected() {
        if(m_CharacterLocomotion) Debug.DrawRay(m_CharacterLocomotion.transform.position + RaycastOriginOffset, m_CharacterLocomotion.transform.forward, Color.red);
    }
}
