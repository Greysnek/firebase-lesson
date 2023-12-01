using UnityEngine;

namespace igrohub.Example1
{
  public struct VoteData
  {
    public string Name;
    public int Amount;

    public string ToJson() => JsonUtility.ToJson(this, false);
    public static VoteData FromJson(string json) => JsonUtility.FromJson<VoteData>(json);
  }
}
