using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace igrohub.Example2.Windows
{
  public class VoteSectionView : MonoBehaviour
  {
    [SerializeField] private Button _voteButton;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private TMP_Text _output;

    private Action _onVote; 
      
    public void Init(string sectionName, Action onVote)
    {
      _onVote = onVote;
      _voteButton.onClick.AddListener(OnButtonClick);
      _header.text = sectionName;
    }

    public void UpdateData(long voteCount)
    {
      _output.text = voteCount.ToString();
    }

    public void MarkVoted(bool voted = true)
    {
      _voteButton.gameObject.SetActive(!voted);
      _output.gameObject.SetActive(voted);
    }

    private void OnButtonClick()
    {
      _onVote?.Invoke();
    }
  }
}
