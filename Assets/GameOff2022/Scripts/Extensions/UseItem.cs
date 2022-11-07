using BehaviorDesigner.Runtime;
using Opsive.UltimateCharacterController.Character.Abilities;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Utility;
using UnityEngine;

[DefaultStartType(AbilityStartType.ButtonDown)]
[DefaultStopType(AbilityStopType.ButtonUp)]
[DefaultInputName("Fire1")]
[DefaultItemStateIndex(2)]
[DefaultState("Use")]
[AllowDuplicateTypes]
public class UseItem : Use {
    [SerializeField] protected bool SendAITreeEventOnUse;
    [SerializeField] protected float AITreeEventRadius;
    [SerializeField] protected LayerMask AITreeLayer;

    protected override void AbilityStarted() {
        base.AbilityStarted();
        if (SendAITreeEventOnUse) {
            var GOs = Physics.OverlapSphere(m_CharacterLocomotion.transform.position, AITreeEventRadius, AITreeLayer);
            foreach (var go in GOs) {
                var AITree = go.transform.root.GetComponent<BehaviorTree>();
                if (AITree) {
                    AITree.SendEvent("ActorAttacking");
                }
            }
        }
    }
}
