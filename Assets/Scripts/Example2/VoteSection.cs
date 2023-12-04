using System;

namespace igrohub.Example2
{
  public class VoteSection
  {
    public readonly string Name;
    private readonly IDataSaver _saver;
    
    public VoteData VoteData { get; private set; }
      
    public VoteSection(string name, IDataSaver saver)
    {
      Name = name;
      _saver = saver;

      Data.Saver.Load(Name).ContinueWith(task => { VoteData = task.Result; });
    }

    public void Vote(string voter)
    {
      if(VoteData.TryAddVoter(voter))
        _saver.Save(Name, VoteData);
    }
  }
}
