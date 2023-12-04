using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace igrohub.Example2
{
  public interface IDataSaver
  {
    event Action<string, VoteData> OnDataChange;
    Task Save(string key, VoteData data);
    Task<VoteData> Load(string key);

    Task<Dictionary<string, VoteData>> LoadAll();
  }
}
