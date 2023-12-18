using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
public class TimerFunctions : MonoBehaviour
{
	public static TimerFunctions instance;
    public void Awake()
    {
        instance = this; 
    }
    public void Start()
    {
       // InitWatchDogTimer(5000);
		//Test();
        //FeedWatchdogTimer();
    }
    public async void Test()
    {
		await Task.Delay(2000);
    }
    public void  InitWatchDogTimer(int miliseconds) => StartCoroutine(StartWatchdogTimer(miliseconds));

	private bool isTimerActive = false;
	private int timer;
	public async Task FeedWatchdogTimer(int value)
	{
		timer += value;
		Debug.Log("TIMERRRR " + timer);
        await Task.Delay(timer);
        Debug.Log("wait");
        isTimerActive = true;
	}
    IEnumerator StartWatchdogTimer(int miliseconds)
	{
		float timer = miliseconds/1000f;

			while (timer > 0f)
			{
				if (isTimerActive)
				{
					isTimerActive = false;
					timer = miliseconds/1000f;
				}
				else
				{
					yield return new WaitForSeconds(1f);
				    Debug.Log((int)timer);
					timer -= 1f;
				}
			}
			throw new System.Exception("Watchdog timer has finished! There must be an error!");		
	}
    public IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);

    }
}

