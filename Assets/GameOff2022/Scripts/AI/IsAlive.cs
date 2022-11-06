using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Opsive.UltimateCharacterController.Traits;

public class IsAlive : Conditional {
	[SerializeField] protected SharedGameObject Target;
	[SerializeField] protected bool Invert;
	public override TaskStatus OnUpdate() {
		bool result = false;
		if (!Target.Value) result = false;
		var healthComp = Target.Value.GetComponent<CharacterHealth>();
		if (healthComp && healthComp.IsAlive()) result = true;
		if (Invert) result = !result;
		Debug.Log(result);
		return result ? TaskStatus.Success : TaskStatus.Failure;
	}
}