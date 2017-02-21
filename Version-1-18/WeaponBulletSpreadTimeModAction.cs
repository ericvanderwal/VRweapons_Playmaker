using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("VRweapons")]
	[Tooltip("Adjust bullet spread over time modifier for the VRweapons script on an object")]

	// the class must match the name of the action
	// if the action is named missleAction then that should be the name of the class
	public class WeaponBulletSpreadTimeModAction : FsmStateAction
	{
		[RequiredField]
		// add the name of your script inside of typeof("yourScriptName"))
		[CheckForComponent(typeof(Weapon))]    

		// this is the game object the script is on
		public FsmOwnerDefault gameObject;

		[Tooltip("Set the bullet spread over time modifier. Max 0.2")]
		[HasFloatSliderAttribute(0, 0.2f)]
		public FsmFloat bulletSpreadTime;

		// you can usually leave this alone
		public FsmBool everyFrame;

		// you are making a custom variable with the scripts type
		Weapon theScript;

		public override void Reset()
		{
			//its good practice to set your var to null at start
			gameObject = null;
			bulletSpreadTime = 0;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			// you are grabbing the script from the game object and storing it in your custom variable type
			theScript = go.GetComponent<Weapon>();

			if (!everyFrame.Value)
			{
				MakeItSo();
				Finish();
			}

		}

		public override void OnUpdate()
		{
			if (everyFrame.Value)
			{
				MakeItSo();
			}
		}

		//Name your method here
		void MakeItSo()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			//Playmaker variable to Script

			theScript.spreadOverTimeModifier = bulletSpreadTime.Value;

			//Note! Playmaker var's need .Value after them or they won't work in some cases


		}

	}
}
