using UnityEngine;
namespace igrohub.Example2.Windows
{
  public interface IWindow
  {
    void Show();
    void Hide();
  }
  
  public abstract class WindowBehaviour : MonoBehaviour, IWindow
  {
    public void Show()
    {
      gameObject.SetActive(true);
      OnShow();
    }
    
    protected virtual void OnShow(){}
    
    public void Hide()
    {
      gameObject.SetActive(false);
    }
  }
}
