using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public List<AudioClip> audioClipsSFX;
	public List<AudioClip> audioClipsMusic;
	public List<AudioSource> audioSources;

	private void Start()
	{
		StartCoroutine(RandomMusic());
	}

	IEnumerator RandomMusic()
	{
		while(true)
		{
			if (!audioSources[0].isPlaying)
			{
				yield return new WaitForSecondsRealtime(3f);
				int randomNum = Random.Range(0,audioClipsMusic.Count);
				playMusicClip(randomNum);
			}else
			{
				yield return new WaitForSecondsRealtime(60);
			}
		}
	}

	void playMusicClip(int clip)
	{
		audioSources[0].clip = audioClipsMusic[clip];
		audioSources[0].Play();
	}
	public void playSFXclip(int clip)
	{
		audioSources[1].clip = audioClipsSFX[clip];
		audioSources[1].Play();
	}
}
