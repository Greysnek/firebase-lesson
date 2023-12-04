using System.Linq;

namespace igrohub.Example2
{
  public class GlobalVoteController : Singleton<GlobalVoteController>
  {
    public int GlobalVoteCount { get; private set; }
    
    private void Awake()
    {
      Data.Saver.OnDataChange += OnDataChange;
      OnDataChange();
    }

    private void OnDestroy()
    {
      Data.Saver.OnDataChange -= OnDataChange;
    }

    private void OnDataChange(string _ = null, VoteData __ = null)
    {
      Data.Saver.LoadAll().ContinueWith(
      task => 
      {
        GlobalVoteCount = task.Result.Values.Sum(v => v.Voters.Count);
      });
    }
  }
}
