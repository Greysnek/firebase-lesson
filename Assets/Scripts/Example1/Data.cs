namespace igrohub.Example1
{
  public static class Data
  {
    public static IDataSaver Saver => new LocalDataSaver();
  }
}
