using UnityEngine;

namespace igrohub
{
  //todo Singleton is not best option for educational code
  //For future: DO NOT DO ANYTHING AT NIGHT!!!
  public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T _instance = null;

    public static T Instance
    {
      get
      {
        if (_instance != null)
          return _instance;

        var fundedInstance = (T)FindObjectOfType(typeof(T));

        if (fundedInstance)
        {
          _instance = fundedInstance;
          DontDestroyOnLoad(_instance.gameObject);
        }
        else
        {
          _instance = null;
        }

        return _instance;
      }
    }
  }
}
