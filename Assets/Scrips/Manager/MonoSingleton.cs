using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool shuttinDown = false;
    private static T instance = null;
    private static object locker = new object();
    public static T Instance
    {
        get
        {
            if(shuttinDown)
            {
                Debug.LogWarning("[MonoSingleton] Instance" + typeof(T) + "already destroyed. Retuning null");
                return null;
            }
            lock(locker)
            {
            if(instance == null)
                {
                instance = (T)FindObjectOfType(typeof(T));
                if(instance == null)
                    {
                        GameObject temp = new GameObject(typeof(T).ToString());
                        instance = temp.AddComponent<T>();
                        //DontDestroyOnLoad(temp); 이거 쓰셈
                    }
                }
            }
            return instance;
        }
    }
        private void OnApplicationQuit()
        {
            shuttinDown = true;
        } private void OnDestroy()
        {
            shuttinDown = true;
        }
}
