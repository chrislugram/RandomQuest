using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour{
	#region STATIC_ENUM_CONSTANTS
	private static TaskManager	taskManagerInstance;
	private static GameObject	taskManagerGO;
	#endregion
	
	#region FIELDS
	private List<CoroutineTask> tasksPool;
	#endregion
	
	#region ACCESSORS
	public static TaskManager	Instance{ 
		get { return taskManagerInstance; } 
	}
	#endregion

	#region CONSTRUCTORS
	#endregion

	#region OVERRIDED_METHODS
	#endregion

	#region METHODS
	public static void Init(){
		if(taskManagerInstance == null){
			taskManagerGO = new GameObject("TaskManager");
			taskManagerInstance = taskManagerGO.AddComponent<TaskManager>();
			taskManagerInstance.CreateTaskList();
			DontDestroyOnLoad(taskManagerGO);
		}
	}

	public static void End(){
		if(taskManagerInstance != null){
			Destroy(taskManagerGO);
		}
	}

	public static CoroutineTask Create(IEnumerator coroutine){
		if(taskManagerInstance != null){
			return new CoroutineTask(coroutine);
		}
		return null;
	}

	public static CoroutineTask Launch(IEnumerator coroutine){
		CoroutineTask task = Create(coroutine);
		if(task != null){
			task.Start();
		}
		return task;
	}
	
	public void Add(CoroutineTask task){
		if(task != null){
			tasksPool.Add(task);
		}
	}
	
	public void Remove(CoroutineTask task){
		if(task != null){
			tasksPool.Remove(task);
		}
	}

	private void CreateTaskList(){
		tasksPool = new List<CoroutineTask>();
	}

	private void OnDestroy(){
		tasksPool.Clear();
		tasksPool = null;

		taskManagerInstance = null;
	}
	#endregion
}
