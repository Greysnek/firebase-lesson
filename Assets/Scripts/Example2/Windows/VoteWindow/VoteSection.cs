using System;
namespace igrohub.Example2.Windows
{
  public class VoteSection
  {
    public event Action Vote;
    
    private readonly VoteSectionView _view;
    private readonly DatabaseReference _databaseReference;
    
    public VoteSection(string name, VoteSectionView view, DatabaseReference databaseRoot)
    {
      _view = view;
      _databaseReference = databaseRoot.Child(name);
      _view.Init(name, OnVote);

      _databaseReference.ChildAdded += OnChildAdded;
    }

    public void Refresh()
    {
      _databaseReference.GetValueAsync().ContinueWithOnMainThread(
      task => {
        _view.UpdateData(task.Result.ChildrenCount);
      });
    }

    public void MarkVoted(bool voted = true)
    {
      _view.MarkVoted(voted);
    }

    private void OnChildAdded(object sender, ChildChangedEventArgs args)
    {
      Refresh();
    }

    private void OnVote()
    {
      _databaseReference.Child(Authentification.UserId).SetValueAsync("");
      Vote?.Invoke();
    }
  }
}
