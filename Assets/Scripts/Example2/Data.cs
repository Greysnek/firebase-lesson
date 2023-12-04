namespace igrohub.Example2
{
  public static class Data
  {
    public const string VOTES_DATA_KEY = "Votes";
    public static IDataSaver Saver => new LocalDataSaver();
  }
}
