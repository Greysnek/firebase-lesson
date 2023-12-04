using System.Collections.Generic;

namespace igrohub.Example2
{
  public class VoteData
  {
    public string Name;
    public readonly List<string> Voters = new();
    
    public VoteData(string name)
    {
      Name = name;
    }

    public bool TryAddVoter(string voter)
    {
      if (Voters.Contains(voter))
        return false;
      
      Voters.Add(voter);
      
      return true;
    }
  }
}
