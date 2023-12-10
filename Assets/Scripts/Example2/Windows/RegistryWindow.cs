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

      _activeTask = RegistryNewUser();
      _activeTask.ContinueWithOnMainThread(task => 
      {
        if (task.IsCanceled)
        {
          _errorMessage.text = "Canceled";
          Debug.LogError("Registry is canceled");
          return;
        }

        if (task.IsFaulted)
        {
          _errorMessage.text = task.Exception.Message;
          Debug.LogException(task.Exception);
          return;
        }
        
        WindowsController.Show<VoteWindow>();
      });
    }

    private async Task RegistryNewUser()
    {
      await FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(_emailInput.text, _passwordInput.text);

      var userData = new UserProfile
      {
        DisplayName = _nameInput.text
      };
      
      await FirebaseAuth.DefaultInstance.CurrentUser.UpdateUserProfileAsync(userData);
    }
  }
}
