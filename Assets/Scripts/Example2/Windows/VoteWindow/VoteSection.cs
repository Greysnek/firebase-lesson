using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace igrohub.Example2.Windows
{
  public class VoteSection : MonoBehaviour
  {
    [SerializeField] private Button _voteButton;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private TMP_Text _output;

    private string _name;
    private Action<string> _onVote; 
      
    public void Init(string sectionName, Action<string> onVote)
    {
      _onVote = onVote;
      _voteButton.onClick.AddListener(OnButtonClick);
      _name = sectionName;
      _header.text = sectionName;
    }

    public void UpdateData(bool voted, long voteCount)
    {
      _output.text = voteCount.ToString();
      _voteButton.gameObject.SetActive(!voted);
      _output.gameObject.SetActive(voted);
    }

    private void OnButtonClick()
    {
      _onVote?.Invoke(_name);
    }
  }
}
