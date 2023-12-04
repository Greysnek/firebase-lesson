using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace igrohub.Example2
{
  public class UserRegistry : Singleton<UserRegistry>
  {
    [SerializeField] private TMP_InputField _input;
    [SerializeField] private Button _registryButton;
    
    public string CurrentUserId { get; private set; }

    private void Awake()
    {
      _registryButton.onClick.AddListener(OnRegistry);
    }

    private void OnDestroy()
    {
      _registryButton.onClick.RemoveListener(OnRegistry);
    }

    private void OnRegistry()
    {
      CurrentUserId = _input.text;
      Hide();
    }

    private void Hide()
    {
      _input.gameObject.SetActive(false);
      _registryButton.gameObject.SetActive(false);
    }
  }
}
