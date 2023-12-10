using UnityEngine;

namespace igrohub
{
  public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T _instance;

    public static T Instance
    {
      get
      {
        if (_instance != null)
          return _instance;

        var fundedInstance = (T)FindObjectOfType(typeof(T));

        _instance = fundedInstance ? fundedInstance : null;

        return _instance;
      }
    }
  }
}
