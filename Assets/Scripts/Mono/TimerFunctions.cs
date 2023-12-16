using System.Collections;
using UnityEngine;

public class TimerFunctions : MonoBehaviour
{
	private bool isTimerActive = false;
	private float countdownDuration = 5f;

	void Start()
	{
		StartCoroutine(StartWatchdogTimer(countdownDuration));
		InvokeRepeating("FeedWatchdogTimer", 2, 2);
	}

	private void Update()
	{
		// Ovo je čisto za testiranje kad stisneš space da se resetira timer ako želiš ovako
		// Ako želiš testirati na ovaj način samo zakomentiraj InvokeRepeating u Startu.
		if (Input.GetKeyDown(KeyCode.Space))
		{
			FeedWatchdogTimer();
		}
	}

	void FeedWatchdogTimer() => isTimerActive = true;

	IEnumerator StartWatchdogTimer(float seconds)
	{
		while (true)
		{
			float timer = seconds;

			while (timer > 0f)
			{
				if (isTimerActive)
				{
					isTimerActive = false;
					timer = seconds;
					Debug.Log("Reset to: " + timer.ToString("F2"));
				}
				else
				{
					Debug.Log(timer.ToString("F2"));
					yield return new WaitForSeconds(0.1f);
					timer -= 0.1f;
				}
			}

			Debug.LogError("Watchdog vrijeme je isteklo! Vjerojatno se radi o grešci.");
			OnErrorEvent();
		}
	}

	void OnErrorEvent()
	{
		// DO SOMETHING
	}
}
