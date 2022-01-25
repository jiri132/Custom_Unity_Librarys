using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/// <summary>
/// Never refference this into other scripts nothing needed
/// </summary>
public class SoundManager : MonoBehaviour
{

	//variables world wide
	public float s = 0;
	public List<AudioClip> audioClipsSFX;
	public List<AudioClip> audioClipsMusic;
	public List<AudioSource> audioSources;
	//dont mind this audiosources
	static List<AudioSource> StaticAudio;
	public static List<AudioClip> StaticSFX;

	public enum ManagerType { Random, Continues };
	public ManagerType managerType;


	//Variables for Random music
	public int WaitingTimeSeconds = 0;
	public AudioClip RandomClip;

	//variables for continues

	private void Start()
	{
		
		StaticAudio = audioSources;
		StaticSFX = audioClipsSFX;

		StartCoroutine(Music());
	}

	IEnumerator Music()
	{
		while(true && audioSources.Count == 2)
		{
			if (!audioSources[0].isPlaying)
			{
				yield return new WaitForSecondsRealtime(s);
				int randomNum = Random.Range(0,audioClipsMusic.Count);
				RandomClip = audioClipsMusic[randomNum];
				playMusicClip(randomNum ,out s);
				if (managerType == ManagerType.Random) { yield return new WaitForSecondsRealtime(60); }
			}
			else
			{
				yield return new WaitForSecondsRealtime(60);
			}
		}
	}

	void playMusicClip(int clip, out float s)
	{
		audioSources[0].clip = audioClipsMusic[clip];
		s = audioSources[0].clip.length;
		audioSources[0].Play();
	}

	/// <summary>
	/// Play a sound effect from this void
	/// use all the static variables for this
	/// </summary>
	/// <param name="clip">the SFX clip used to play</param>
	public static void playSFXclip(int clip)
	{
		StaticAudio[1].clip = StaticSFX[clip];
		StaticAudio[1].Play();
	}
}

//Custom inspector starts here
#if UNITY_EDITOR

[CustomEditor(typeof(SoundManager))]
public class SoundManagerInspectorEditor : Editor
{
	public override void OnInspectorGUI()
	{

		var soundManager = target as SoundManager;

		soundManager.managerType = (SoundManager.ManagerType)EditorGUILayout.EnumPopup(soundManager.managerType);
		
		switch (soundManager.managerType)
		{
			case SoundManager.ManagerType.Random:
				EditorGUILayout.LabelField("OPTIONAL VARIABLES ", "Effects with chosen method of sound");
				soundManager.WaitingTimeSeconds = EditorGUILayout.IntField("Waiting Seconds", soundManager.WaitingTimeSeconds);
				soundManager.RandomClip = (AudioClip)EditorGUILayout.ObjectField("Random Clip",soundManager.RandomClip, typeof(AudioClip),true);
				break;	

			case SoundManager.ManagerType.Continues:


				break;


			default:
				break;
		}
		EditorGUILayout.LabelField("", "");
		EditorGUILayout.LabelField("GLOBAL VARIABLES", "All lists for functionality");
		var SourceList = serializedObject.FindProperty("audioSources");
		var MusicList = serializedObject.FindProperty("audioClipsMusic");
		var SFXList = serializedObject.FindProperty("audioClipsSFX");
		EditorGUILayout.PropertyField(SourceList, new GUIContent("audioSouces List"), true);
		EditorGUILayout.PropertyField(MusicList, new GUIContent("Music List"), true);
		EditorGUILayout.PropertyField(SFXList, new GUIContent("Sound Effects List"), true);

	}
}//end inspectorclass

#endif
