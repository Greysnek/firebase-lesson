using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace igrohub.Example2.Windows
{
  public class VoteWindow : WindowBehaviour
  {
    [SerializeField] private string[] _sectionsNames;
    [Space]
    [SerializeField] private TMP_Text _currentUserName;
    [SerializeField] private Button _exit;
    [SerializeField] private Transform _sectionsHolder;
    [FormerlySerializedAs("_sectionPrefab")]
    [SerializeField] private VoteSectionView _sectionViewPrefab;

    private FirebaseUser _user;
    private DatabaseReference _databaseReference;
    private readonly Dictionary<string, VoteSection> _sections = new();
    private bool _voted;

    private void Awake()
    {
      _exit.onClick.AddListener(Exit);
      _databaseReference = FirebaseDatabase.DefaultInstance.RootReference.Root.Child("Example2").Child("Votes");
      
      foreach (string sectionName in _sectionsNames)
      {
        var view = Instantiate(_sectionViewPrefab, _sectionsHolder);
        _sections[sectionName] = new VoteSection(sectionName, view, _databaseReference);
        _sections[sectionName].Vote += MarkAllVoted;
      }
    }
    
    protected override void OnShow()
    {
      _user = FirebaseAuth.DefaultInstance.CurrentUser;
      _currentUserName.text = string.IsNullOrEmpty(_user.DisplayName) ? _user.Email : _user.DisplayName;
      RefreshVoteState();
    }

    private void RefreshVoteState()
    {
      _databaseReference.GetValueAsync().ContinueWithOnMainThread(
      task => {
        var data = task.Result;
        _voted = data.Children.Any(c => c.HasChild(_user.UserId));
        MarkAllVoted(_voted);
      });
    }

    private void MarkAllVoted() => MarkAllVoted(true);
    private void MarkAllVoted(bool value)
    {
      foreach (KeyValuePair<string, VoteSection> section in _sections)
      {
        section.Value.MarkVoted(value);
      }
    }
    
    private static void Exit()
    {
      FirebaseAuth.DefaultInstance.SignOut();
      WindowsController.Show<EntryWindow>();
    }
  }
}
