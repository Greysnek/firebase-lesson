using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace igrohub.Example2.Windows
{
  public class SignInWindow : WindowBehaviour
  {
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _applyButton;
    
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;

    [SerializeField] private TMP_Text _errorMessage;

    private Task _activeTask;

    private void Awake()
    {
      _backButton.onClick.AddListener(WindowsController.Show<EntryWindow>);
      _applyButton.onClick.AddListener(OnApply);
    }
    
    protected override void OnShow()
    {
      _emailInput.text = "";
      _passwordInput.text = "";
      _errorMessage.text = "";
    }

    private void OnApply()
    {
      if(_activeTask != null && _activeTask.Status == TaskStatus.Running)
        return;
      
      _activeTask = SignIn();
      _activeTask.ContinueWithOnMainThread(
      task => 
      {
        if (task.IsCanceled) {
          Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
          return;
        }
        if (task.IsFaulted) {
          Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
          _errorMessage.text = task.Exception.Message;
          return;
        }
        
        WindowsController.Show<VoteWindow>();
      });
    }

    private async Task SignIn()
    {
      await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(_emailInput.text, _passwordInput.text);
    }
  }
}
