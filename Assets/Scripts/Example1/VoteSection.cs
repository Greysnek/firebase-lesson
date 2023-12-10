using System;
using System.Threading.Tasks;
using UnityEngine;

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
      private set 
      {
        _value = value;
        OnValueChange?.Invoke(_value);
      }
    }

    public VoteSection(string name, IDataSaver saver)
    {
      Name = name;
      _saver = saver;
    }

    public async Task LoadDataAsync()
    {
      var data = await _saver.Load(Name);
      Value = data.Amount;
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
