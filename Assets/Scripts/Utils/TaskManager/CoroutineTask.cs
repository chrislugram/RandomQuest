using System;
using System.Collections;

public class CoroutineTask
{
	public enum EState
	{
		RUN,
		PAUSE,
		STOP
	}

	public event Action OnTaskStarted = delegate {};
	public event Action OnTaskFinished = delegate {};
	
	private IEnumerator coroutine;
	private EState 		state;

	private TaskManager	taskManager;
	
	public CoroutineTask(IEnumerator coroutine){
		this.coroutine = coroutine;
		this.taskManager = TaskManager.Instance;
		this.taskManager.Add(this);
	}

	public void Start(){
		state = EState.RUN;
		taskManager.StartCoroutine(Run());
	}
	
	public void Pause(){
		state = EState.PAUSE;
	}
	
	public void Unpause(){
		state = EState.RUN;
	}
	
	public void Stop(){
		state = EState.STOP;
	}
	
	public IEnumerator StartAndWait()
	{
		state = EState.RUN;
		yield return taskManager.StartCoroutine(Run());
	}

	/// <summary>
	/// Run process of the task.
	/// There are two events, OnTaskStarted and OnTaskFinished.
	/// 
	/// The life cycle is as follows:
	/// OnTaskStarted - pass a frame - runs the coroutine - pass a frame - OnTaskFinished
	/// </summary>
	private IEnumerator Run(){
		OnTaskStarted();

		yield return null;

		while(state != EState.STOP) {
			if(state == EState.PAUSE){
				yield return null;
			}else if(coroutine != null && coroutine.MoveNext()){
				yield return coroutine.Current;
			}else {
				Stop ();
				yield return null;
			}
		}

		OnTaskFinished();

		taskManager.Remove(this);
	}
}