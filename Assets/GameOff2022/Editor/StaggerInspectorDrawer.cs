
namespace Opsive.UltimateCharacterController.Editor.Inspectors.Character.Abilities
{
	using Opsive.Shared.Editor.Inspectors;
	using Opsive.UltimateCharacterController.Editor.Utility;
	using UnityEditor;
	using UnityEditor.Animations;
	using UnityEngine;

	/// <summary>
	/// Draws a custom inspector for the Stagger Ability.
	/// </summary>
	[InspectorDrawer(typeof(Stagger))]
	public class StaggerInspectorDrawer : AbilityInspectorDrawer
	{
		// ------------------------------------------- Start Generated Code -------------------------------------------
		// ------- Do NOT make any changes below. Changes will be removed when the animator is generated again. -------
		// ------------------------------------------------------------------------------------------------------------

		/// <summary>
		/// Returns true if the ability can build to the animator.
		/// </summary>
		public override bool CanBuildAnimator { get { return true; } }

		/// <summary>
		/// An editor only method which can add the abilities states/transitions to the animator.
		/// </summary>
		/// <param name="animatorController">The Animator Controller to add the states to.</param>
		/// <param name="firstPersonAnimatorController">The first person Animator Controller to add the states to.</param>
		public override void BuildAnimator(AnimatorController animatorController, AnimatorController firstPersonAnimatorController)
		{
			if (animatorController.layers.Length <= 0) {
				Debug.LogWarning("Warning: The animator controller does not contain the same number of layers as the demo animator. All of the animations cannot be added.");
				return;
			}

			var baseStateMachine1256977630 = animatorController.layers[0].stateMachine;

			// The state machine should start fresh.
			for (int i = 0; i < animatorController.layers.Length; ++i) {
				for (int j = 0; j < baseStateMachine1256977630.stateMachines.Length; ++j) {
					if (baseStateMachine1256977630.stateMachines[j].stateMachine.name == "Flinch/Stagger/Hit") {
						baseStateMachine1256977630.RemoveStateMachine(baseStateMachine1256977630.stateMachines[j].stateMachine);
						break;
					}
				}
			}

			// AnimationClip references.
			var hitFwdAnimationClip37530Path = AssetDatabase.GUIDToAssetPath("bd52cb59f8fa7914ea47259fb3e283dd"); 
			var hitFwdAnimationClip37530 = AnimatorBuilder.GetAnimationClip(hitFwdAnimationClip37530Path, "Hit_Fwd");

			// State Machine.
			var flinchStaggerHitAnimatorStateMachine34298 = baseStateMachine1256977630.AddStateMachine("Flinch/Stagger/Hit", new Vector3(630f, 110f, 0f));

			// States.
			var staggerAnimatorState35100 = flinchStaggerHitAnimatorStateMachine34298.AddState("Stagger", new Vector3(390f, 110f, 0f));
			staggerAnimatorState35100.motion = hitFwdAnimationClip37530;
			staggerAnimatorState35100.cycleOffset = 0f;
			staggerAnimatorState35100.cycleOffsetParameterActive = false;
			staggerAnimatorState35100.iKOnFeet = false;
			staggerAnimatorState35100.mirror = false;
			staggerAnimatorState35100.mirrorParameterActive = false;
			staggerAnimatorState35100.speed = 1f;
			staggerAnimatorState35100.speedParameterActive = false;
			staggerAnimatorState35100.writeDefaultValues = true;

			// State Machine Defaults.
			flinchStaggerHitAnimatorStateMachine34298.anyStatePosition = new Vector3(50f, 20f, 0f);
			flinchStaggerHitAnimatorStateMachine34298.defaultState = staggerAnimatorState35100;
			flinchStaggerHitAnimatorStateMachine34298.entryPosition = new Vector3(50f, 120f, 0f);
			flinchStaggerHitAnimatorStateMachine34298.exitPosition = new Vector3(800f, 120f, 0f);
			flinchStaggerHitAnimatorStateMachine34298.parentStateMachinePosition = new Vector3(800f, 20f, 0f);

			// State Transitions.
			var animatorStateTransition37528 = staggerAnimatorState35100.AddExitTransition();
			animatorStateTransition37528.canTransitionToSelf = true;
			animatorStateTransition37528.duration = 0.25f;
			animatorStateTransition37528.exitTime = 0.75f;
			animatorStateTransition37528.hasExitTime = true;
			animatorStateTransition37528.hasFixedDuration = true;
			animatorStateTransition37528.interruptionSource = TransitionInterruptionSource.None;
			animatorStateTransition37528.offset = 0f;
			animatorStateTransition37528.orderedInterruption = true;
			animatorStateTransition37528.isExit = true;
			animatorStateTransition37528.mute = false;
			animatorStateTransition37528.solo = false;
			animatorStateTransition37528.AddCondition(AnimatorConditionMode.NotEqual, 201f, "AbilityIndex");


			// State Machine Transitions.
			var animatorStateTransition34830 = baseStateMachine1256977630.AddAnyStateTransition(staggerAnimatorState35100);
			animatorStateTransition34830.canTransitionToSelf = false;
			animatorStateTransition34830.duration = 0.25f;
			animatorStateTransition34830.exitTime = 0.75f;
			animatorStateTransition34830.hasExitTime = false;
			animatorStateTransition34830.hasFixedDuration = true;
			animatorStateTransition34830.interruptionSource = TransitionInterruptionSource.None;
			animatorStateTransition34830.offset = 0f;
			animatorStateTransition34830.orderedInterruption = true;
			animatorStateTransition34830.isExit = false;
			animatorStateTransition34830.mute = false;
			animatorStateTransition34830.solo = false;
			animatorStateTransition34830.AddCondition(AnimatorConditionMode.Equals, 201f, "AbilityIndex");
		}
	}
}
