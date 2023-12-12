using System.Threading.Tasks;
using Firebase.Database;

namespace igrohub.Example1
{
  public class FirebaseDataSaver : Singleton<FirebaseDataSaver>, IDataSaver
  {
    private const string FOLDER = "Example1";
    
    private DatabaseReference _reference;
      
    private void Awake()
    {
      _reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public Task Save(string key, VoteData data)
    {
      return _reference.Root.Child(FOLDER).Child(key).SetRawJsonValueAsync(data.ToJson());
    }
    public async Task<VoteData> Load(string key)
    {
      var snapshot = await _reference.Root.Child(FOLDER).Child(key).GetValueAsync();

      if (snapshot.Value != null)
        return VoteData.FromJson(snapshot.GetRawJsonValue());
        
      return new VoteData();
    }
  }
}
