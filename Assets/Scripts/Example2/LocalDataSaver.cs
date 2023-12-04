using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace igrohub.Example2
{
  public class LocalDataSaver : IDataSaver
  {
    //todo event inside static field is danger
    //For future: DO NOT DO ANYTHING AT NIGHT!!!
    public event Action<string, VoteData> OnDataChange;
    
    public Task Save(string key, VoteData data)
    {
      OnDataChange?.Invoke(key, data);
      Dictionary<string, VoteData> savedData = LoadData();
      savedData[key] = data;
      SaveData(savedData);
      
      return Task.CompletedTask;
    }
    
    public async Task<VoteData> Load(string key)
    {
      await Task.CompletedTask;

      Dictionary<string, VoteData> savedData = LoadData();

      return !savedData.ContainsKey(key) ? new VoteData(key) : savedData[key];

    }
    public async Task<Dictionary<string, VoteData>> LoadAll()
    {
      await Task.CompletedTask;
      return LoadData();
    }

    private Dictionary<string, VoteData> LoadData()
    {
      if (!PlayerPrefs.HasKey(Data.VOTES_DATA_KEY))
      {
        Debug.LogError("Data didn't exist");
        return new Dictionary<string, VoteData>();
      }

      string json = PlayerPrefs.GetString(Data.VOTES_DATA_KEY);

      return JsonUtility.FromJson<Dictionary<string, VoteData>>(json);
    }

    private void SaveData(Dictionary<string, VoteData> data)
    {
      //todo Dictionary dont serialized to json with simple JsonUtility
      //For future: DO NOT DO ANYTHING AT NIGHT!!!
      PlayerPrefs.SetString(Data.VOTES_DATA_KEY, JsonUtility.ToJson(data));
    }
  }
}
