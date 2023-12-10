using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

namespace igrohub.Example2.Windows
{
  public class EntryWindow : WindowBehaviour
  {
    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _registryButton;

    private void Awake()
    {
      _signInButton.onClick.AddListener(WindowsController.Show<SignInWindow>);
      _registryButton.onClick.AddListener(WindowsController.Show<RegistryWindow>); 
    }

    protected override void OnShow()
    {
      if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        WindowsController.Show<VoteWindow>();
    }
  }
}
