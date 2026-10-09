using UnityEngine;
using UnityEngine.Events;

//A timer is simething that keeps track of time
//If a timer has >0 time it wiill begin to count down until no more time remains.
//Timers can be paused.
/// A paused timer retains its currecnt time value.
/// A timer can be paused/unpaused by using the SetPause() funtion.
/// A timer will be unpasused if StartTimer() is used.
public class Timer : MonoBehaviour
{
    [Tooltip("The current time on the timer. If the timer has time, it will count down.")]
    [SerializeField][Range(0f, 600f)] private float time;
    private bool is_paused = false;
    private bool has_finished = false;

    //Event to signal when the timer has finished counting down.
    public UnityEvent TimerFinished;

    //Gets the time from the timer.
    public float GetTime()
        {
            return time;
        }
    // Gets the finished state of the timer.
    public bool IsFinsihed()
    {
        return has_finished;
    }
    //Sets the paused status of the timer.
    public void SetPause(bool pause)
    {
        is_paused = pause;
    }
    //Gets the paused status of the timer.
    public bool GetPause()
    {
        return (is_paused);
    }

    //Starts the timer by setting the time to t.
    //Acts as the setter for the time.
    public void StartTimer(float t)
    {
        time = t; // Assign the time passed in.
        is_paused = false;
        has_finished = false;
        
    }

    //Stops the timer by setting it to zero.
    public void StopTimer()
    {
        has_finished = true; // The timer has finished.
        time = 0; //So set its time to zero.
        TimerFinished.Invoke(); //Invoke the event to signal that the timer has finished.

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Start the timer if the user set the time in the inspector.
        if (time > 0)
            StartTimer(time);
    }

    // Update is called once per frame
    void Update()
    {
        //Stop the timer from running if it is paused or has finished.
        if (is_paused || has_finished)
            return;
        if (time > 0) //If the timer has time on it...
            time -= Time.deltaTime; // Countdown.
        else// Otherwise
            StopTimer(); // Stop the timer.


    }
}
