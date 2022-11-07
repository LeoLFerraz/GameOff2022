using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
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

    protected GameObject _previousTarget;
    protected IInteractable _target;
    protected GameObject _targetGO;

    public override void InactiveUpdate() {
        base.InactiveUpdate();
        RaycastHit hit;
        GameObject hitGO = null;
        IInteractable interactable = null;
        if (Physics.Raycast(m_CharacterLocomotion.transform.position + RaycastOriginOffset,
                            m_CharacterLocomotion.transform.forward, out hit, RaycastDistance,
                                RaycastLayerMask, QueryTriggerInteraction.Collide)) {
            hitGO = hit.collider.gameObject;
            interactable = hitGO.GetComponent<IInteractable>();
            if(interactable == null) interactable = hitGO.transform.parent.GetComponent<IInteractable>();
            if(interactable == null) interactable = hitGO.GetComponentInChildren<IInteractable>();
        }
        if(_previousTarget != null && _previousTarget != _targetGO) {
            var previousHighlight = _previousTarget.GetComponent<HighlightPlusExtension>();
            if (previousHighlight) {
                previousHighlight.highlighted = false;
                previousHighlight.TextBillboardEnabled = false;
            }
        }
            
        _previousTarget = _targetGO;
            
        _targetGO = hitGO;
        _target = interactable;

        if (_target != null) {
            var newHighlight = _targetGO.GetComponent<HighlightPlusExtension>();
            if (newHighlight) {
                newHighlight.highlighted = true;
                newHighlight.TextBillboardEnabled = true;
            }
        }
    }

    protected override void AbilityStarted() {
        var hits = Physics.RaycastAll(m_CharacterLocomotion.transform.position + RaycastOriginOffset, m_CharacterLocomotion.transform.forward, RaycastDistance,
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
