using System;

namespace igrohub.Example1
{
  public class VoteSection
  {
    public readonly string Name;
    private readonly IDataSaver _saver;
    private int _value;
    public event Action<int> OnValueChange;

    public int Value 
    {
      get => _value;
      set 
      {
        _value = value;
        OnValueChange?.Invoke(_value);
      }
    }

    public VoteSection(string name, IDataSaver saver)
    {
      Name = name;
      _saver = saver;
      
      _saver.Load(Name).ContinueWith(task => 
      {
        Value = task.Result.Amount;
      });
    }

    public void Vote(int value)
    {
      Value = value;
      
      var voteData = new VoteData
      {
        Name = Name,
        Amount = value
      };

      _saver.Save(Name, voteData).ConfigureAwait(false);
    }
  }
}
