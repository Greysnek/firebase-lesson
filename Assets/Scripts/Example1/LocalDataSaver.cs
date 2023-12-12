using System;
using System.Threading.Tasks;
using UnityEngine;
namespace igrohub.Example1
{
  public class LocalDataSaver : IDataSaver
  {
    public Task Save(string key, VoteData data)
    {
      PlayerPrefs.SetString(key, data.ToJson());
      return Task.CompletedTask;
    }
    public async Task<VoteData> Load(string key)
    {
      if (!PlayerPrefs.HasKey(key))
        return new VoteData();

      await Task.CompletedTask;
      return VoteData.FromJson(PlayerPrefs.GetString(key));
    }
  }
}
