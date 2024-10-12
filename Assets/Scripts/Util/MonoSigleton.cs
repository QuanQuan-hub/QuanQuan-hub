using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSigleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance = null;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject go = new GameObject(typeof(T).ToString());

				_instance = go.AddComponent<T>();
			}

			return _instance;
		}
	}

	protected virtual void Awake()
	{
		DontDestroyOnLoad(gameObject);

		_instance = this as T;
	}

	public virtual void OnCreate()
    {

    }

	protected virtual void OnDestroy()
	{
		Destroy(gameObject);
		_instance = null;
	}

	public virtual void Release()
	{

	}
}
