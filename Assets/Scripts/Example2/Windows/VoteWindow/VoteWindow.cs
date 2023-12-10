using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
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
    [SerializeField] private VoteSection _sectionPrefab;

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
        _sections[sectionName] = Instantiate(_sectionPrefab, _sectionsHolder);
        _sections[sectionName].Init(sectionName, Vote);
        _databaseReference.Child(sectionName).ChildAdded += (_, args) => UpdateData(sectionName, args.Snapshot);
      }
    }
    
    protected override void OnShow()
    {
      _user = FirebaseAuth.DefaultInstance.CurrentUser;
      _currentUserName.text = string.IsNullOrEmpty(_user.DisplayName) ? _user.Email : _user.DisplayName;
      UpdateAllData();
    }

    private void UpdateAllData()
    {
      _databaseReference.GetValueAsync().ContinueWithOnMainThread(
      task => {
        var data = task.Result;

        _voted = data.Children.Any(c => c.HasChild(_user.UserId));
        
        foreach (var section in _sections.Keys)
        {
          var sectionSnapShot = data.Child(section);
          UpdateData(section, sectionSnapShot);
        }
      });
    }

    private void UpdateData(string section, DataSnapshot snapShot)
    {
      _sections[section].UpdateData(_voted, snapShot.ChildrenCount);
    }

    private static void Exit()
    {
      FirebaseAuth.DefaultInstance.SignOut();
      WindowsController.Show<EntryWindow>();
    }

    private void Vote(string section)
    {
      _voted = true;
      _databaseReference.Child(section).Child(_user.UserId).SetValueAsync("").ContinueWithOnMainThread(_ => UpdateAllData());
    }
  }
}
