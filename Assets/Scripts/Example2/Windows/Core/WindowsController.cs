using System;
using System.Collections.Generic;
using UnityEngine;

namespace igrohub.Example2.Windows
{
  public class WindowsController : Singleton<WindowsController>
  {
    private readonly Dictionary<Type, IWindow> _windows = new();

    private IWindow _activeWindow;
    
    private void Awake()
    {
      foreach (var window in GetComponentsInChildren<IWindow>(true))
      {
        _windows[window.GetType()] = window;
        window.Hide();
      }
      
      Show<EntryWindow>();
    }

    public static void Show<T>() where T : IWindow
    {
      Instance._activeWindow?.Hide();
      Instance._activeWindow = Instance._windows[typeof(T)];
      Instance._activeWindow.Show();
    }
    
    public static void HideActive()
    {
      Instance._activeWindow.Hide();
      Instance._activeWindow = null;
    }
  }
}
