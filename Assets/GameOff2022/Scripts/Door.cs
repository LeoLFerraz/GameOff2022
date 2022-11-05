using System;
using System.Collections;
using System.Collections.Generic;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Inventory;
using Opsive.UltimateCharacterController.Items;
using UniRx;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
    [SerializeField] public bool StartUnlocked = true;
    [SerializeField] public bool StartOpen;
    [SerializeField] public bool Automatic;
    [SerializeField] public bool OpenWhenUnlocked = true;
    [SerializeField] public Item RequiredKey;
    [SerializeField] public Collider[] AutoCloseColliders;
    [SerializeField] public Collider[] MovementColliders;
    [SerializeField] public Animator Animator;

    protected BoolReactiveProperty _Unlocked = new();
    protected BoolReactiveProperty _Open = new();

    protected List<IDisposable> _Disposables = new();

    private static readonly int OpenAnimatorHash = Animator.StringToHash("Open");

    protected void Start() {
        _Unlocked.Value = StartUnlocked;
        _Open.Value = StartOpen;
        if (!Animator) Animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        _Disposables.Add(_Open.Subscribe(ToggleMovementColliders));
        if (Animator) {
            _Disposables.Add(_Open.Subscribe((x) => {
                Animator.SetBool(OpenAnimatorHash, x);
            }));
        }
    }

    private void OnDisable() {
        foreach (var disposable in _Disposables) {
            disposable.Dispose();
        }
    }

    public void ToggleLock(bool unlocked, UltimateCharacterLocomotion controller) {
        _Unlocked.Value = unlocked;
    }

    public void TryOpen(UltimateCharacterLocomotion controller) {
        if (_Unlocked.Value) {
            Open(controller);
        } else {
            var inventory = controller.GetComponent<Inventory>();
            if (inventory) {
                if (UseKey(inventory)) {
                    Unlock(controller);
                }
            } else {
                Debug.LogWarning("Controller has no inventory");
            }
        }
    }

    public void TryClose(UltimateCharacterLocomotion controller) {
        Close(controller);
    }

    public void TryToggle(UltimateCharacterLocomotion controller) {
        if (_Open.Value) {
            TryClose(controller);
        } else {
            TryOpen(controller);
        }
    }

    protected void Open(UltimateCharacterLocomotion controller) {
        _Open.Value = true;
    }

    protected void Close(UltimateCharacterLocomotion controller) {
        _Open.Value = false;
    }

    protected void Unlock(UltimateCharacterLocomotion controller) {
        _Unlocked.Value = true;
        if (OpenWhenUnlocked) TryOpen(controller);
    }

    protected void Lock(UltimateCharacterLocomotion controller) {
        _Unlocked.Value = false;
    }

    protected void ToggleMovementColliders(bool deactivate) {
        if (MovementColliders == null) return;
        foreach (var collider in MovementColliders) {
            collider.enabled = !deactivate;
        }
    }

    protected bool UseKey(Inventory inventory) {
        if (inventory.HasItem(RequiredKey)) {
            inventory.DropItem(RequiredKey, 1, false, true);
            return true;
        }
        return false;
    }

    public void OnInteract(UltimateCharacterLocomotion controller) {
        TryToggle(controller);
    }
}
