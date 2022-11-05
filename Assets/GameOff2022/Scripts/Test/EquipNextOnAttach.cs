using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities.Items;
using UnityEngine;

public class EquipNextOnAttach : MonoBehaviour {
    void Start() {
        var controller = GetComponent<UltimateCharacterLocomotion>();
        var equipNext = controller.GetAbility<EquipNext>();
        Debug.Log(controller.TryStartAbility(equipNext));
    }

}
