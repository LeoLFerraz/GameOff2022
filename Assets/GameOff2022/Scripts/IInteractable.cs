using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character;
using UnityEngine;

public interface IInteractable {
    public void OnInteract(UltimateCharacterLocomotion controller);
}
