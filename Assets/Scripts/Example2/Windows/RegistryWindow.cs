using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace igrohub.Example2.Windows
{
  public class RegistryWindow : WindowBehaviour
  {
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _applyButton;
    
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;

    [SerializeField] private TMP_Text _errorMessage;

    private Task _activeTask;
    
    
    private void Awake()
    {
      _backButton.onClick.AddListener(WindowsController.Show<EntryWindow>);
      _applyButton.onClick.AddListener(OnApply);
    }

    private void OnApply()
    {
      if(_activeTask != null && _activeTask.Status == TaskStatus.Running)
        return;
      
      _activeTask = FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(_emailInput.text, _passwordInput.text);
      _activeTask.ContinueWithOnMainThread(
      task => 
      {
        if (task.IsCanceled) {
          Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
          return;
        }
        if (task.IsFaulted)
        {
          Debug.LogException(task.Exception);
          _errorMessage.text = task.Exception.Message;
          return;
        }
        
        _activeTask = FirebaseAuth.DefaultInstance.CurrentUser.UpdateUserProfileAsync(new UserProfile
          {
            DisplayName = _nameInput.text
          });
        
        _activeTask.ContinueWithOnMainThread(
        _ => 
        {
          WindowsController.Show<VoteWindow>();
        });
      }
      );
    }
  }
}
