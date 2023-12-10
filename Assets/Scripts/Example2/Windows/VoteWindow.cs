using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace igrohub.Example2.Windows
{
  public class VoteWindow : WindowBehaviour
  {
    [SerializeField] private TMP_Text _currentUserName;
    [SerializeField] private Button _exit;

    private FirebaseUser _user;

    private void Awake()
    {
      _exit.onClick.AddListener(Exit);
    }

    protected override void OnShow()
    {
      var user = FirebaseAuth.DefaultInstance.CurrentUser;
      _currentUserName.text = string.IsNullOrEmpty(user.DisplayName) ? user.Email : user.DisplayName;
    }

    private void Exit()
    {
      FirebaseAuth.DefaultInstance.SignOut();
      WindowsController.Show<EntryWindow>();
    }
  }
}
