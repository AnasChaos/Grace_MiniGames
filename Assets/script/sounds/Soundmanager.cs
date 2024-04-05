using System;
using UnityEngine;
using UnityEngine.Audio;

public class Soundmanager : MonoBehaviour
{
	public static Soundmanager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;
	float mu;
	int mus;

	void Start()
	{
		if (instance != null)
		{
			// Destroy(gameObject);
			Destroy(this);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
		mu = PlayerPrefs.GetFloat("music");
		mus = PlayerPrefs.GetInt("mute");

		//Play("click");

	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (mu == 0)
		{
			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));

		}
		else
		{
			//s.source.volume = 0;
		}
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Stops(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Stop();
	}

	public void Stopall()
	{
		foreach (Sound s in sounds)
		{
			s.source.Stop();

		}
	}
	public void music(float noice)
	{
		foreach (Sound s in sounds)
		{
			//s.source.Stop();
			s.source.volume = noice * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			//s.source.Play();

		}
	}

	public bool Isplaying(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		return s.source.isPlaying;
	}
}
