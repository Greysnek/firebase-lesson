using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace igrohub.Example1
{
  public class VoteSectionView : MonoBehaviour
  {
    [SerializeField] private string _name;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private TMP_InputField _input;
    [SerializeField] private TMP_Text _output;
    [SerializeField] private Button _voteButton;

    private VoteSection _section;

    private void Awake()
    {
      _header.text = _name;
      _section = new VoteSection(_name, Data.Saver);
      _section.OnValueChange += OnValueChange;
      _output.text = _section.Value.ToString();
      _voteButton.onClick.AddListener(OnVoteClick);
    }

    private void OnDestroy()
    {
      _section.OnValueChange -= OnValueChange;
      _voteButton.onClick.RemoveListener(OnVoteClick);
    }

    private void OnValueChange(int value)
    {
      _output.text = value.ToString();
    }

    private void OnVoteClick()
    {
      _section.Vote(int.Parse(_input.text));
    }
  }
}
