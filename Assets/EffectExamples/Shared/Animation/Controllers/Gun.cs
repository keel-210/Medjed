using UnityEngine;

namespace AnimatorParameter
{
	[System.Serializable]
	public class Gun
	{
		public Animator animator;

		protected readonly static int FireHash = -118708495; public void Fire(){ animator.SetTrigger (FireHash); } public void ResetFire() { animator.ResetTrigger (FireHash); }
		public static readonly int Base_Layer_GunFire = 1465097150;
		public static readonly int Base_Layer_GunIdle = -743889052;

	}
}