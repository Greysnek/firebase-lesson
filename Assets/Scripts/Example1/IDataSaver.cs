using System.Threading.Tasks;
namespace igrohub.Example1
{
  public interface IDataSaver
  {
    Task Save(string key, VoteData data);
    Task<VoteData> Load(string key);
  }
}
