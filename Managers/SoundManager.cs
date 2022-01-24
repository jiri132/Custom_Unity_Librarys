using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoundManager : MonoBehaviour
{

	//variables world wide
	public float s = 0;
	public List<AudioClip> audioClipsSFX;
	public List<AudioClip> audioClipsMusic;
	public List<AudioSource> audioSources;


	public enum ManagerType { Random, Continues };
	public ManagerType managerType;


	//Variables for Random music
	public int WaitingTimeSeconds = 0;
	public AudioClip RandomClip;

	//variables for continues


	private void Start()
	{
		StartCoroutine(Music());
	}

	IEnumerator Music()
	{
		while(true)
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
	public void playSFXclip(int clip, out float s)
	{
		audioSources[1].clip = audioClipsSFX[clip];
		s = audioSources[1].clip.length;
		audioSources[1].Play();
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
				soundManager.WaitingTimeSeconds = EditorGUILayout.IntField("Waiting Seconds", soundManager.WaitingTimeSeconds);
				soundManager.RandomClip = (AudioClip)EditorGUILayout.ObjectField("Random Clip",soundManager.RandomClip, typeof(AudioClip),true);
				break;	


			case SoundManager.ManagerType.Continues:


				break;


			default:
				break;
		}

	}
}//end inspectorclass

#endif
