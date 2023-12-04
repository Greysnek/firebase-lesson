using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace igrohub.Example2
{
  public class VoteSectionView : MonoBehaviour
  {
    [SerializeField] private string _name;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private Slider _output;
    [SerializeField] private Button _voteButton;

    private VoteSection _section;

    private void Awake()
    {
      _header.text = _name;
      _section = new VoteSection(_name, Data.Saver);

      Data.Saver.OnDataChange += OnDataChange;
      _voteButton.onClick.AddListener(OnVoteClick);
      
      OnDataChange();
    }

    private void OnDestroy()
    {
      Data.Saver.OnDataChange -= OnDataChange;
      _voteButton.onClick.RemoveListener(OnVoteClick);
    }

    private void OnDataChange(string _ = null, VoteData __ = null)
    {
      if (GlobalVoteController.Instance.GlobalVoteCount <= 0)
      {
        _output.value = 0;
        return;
      }
      
      _output.value = (float)_section.VoteData.Voters.Count / GlobalVoteController.Instance.GlobalVoteCount;
    }

    private void OnVoteClick()
    {
      _section.Vote(UserRegistry.Instance.CurrentUserId);
    }
  }
}
