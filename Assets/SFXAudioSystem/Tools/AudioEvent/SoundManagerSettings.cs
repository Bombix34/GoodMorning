using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using Tools.Utils;
#endif
using Tools.Audio;

namespace Tools.Managers
{
	public class SoundManagerSettings : ScriptableObject
	{

		[SerializeField]
		private AudioDatabase m_SoundDataBase;

#if UNITY_EDITOR
		[MenuItem("Assets/Create/Audio/ManagerSettings/SoundManagerSettings", priority = -25)]
		public static void CreateAsset()
		{
		//	ScriptableObjectUtility.CreateAsset<SoundManagerSettings>("SoundManagerSettings", null);
		}
#endif

		public AudioDatabase SoundDatabase { get => m_SoundDataBase; }

	}
}