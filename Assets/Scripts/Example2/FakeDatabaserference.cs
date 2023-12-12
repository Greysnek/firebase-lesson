using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//todo
namespace igrohub.Example2
{
  public class ChildChangedEventArgs
  {
    
  }
  
  public class DatabaseReference
  {
    public List<DatabaseReference> Children;

    public Action<object, ChildChangedEventArgs> ChildAdded;

    public int ChildrenCount;
    
    public DatabaseReference GetValueAsync()
    {
      throw new NotImplementedException();
    }

    public void ContinueWithOnMainThread(Action<Task<DatabaseReference>> task)
    {
      throw new NotImplementedException();
    }

    public bool HasChild(string value)
    {
      throw new NotImplementedException();
    }

    public DatabaseReference Child(string value)
    {
      throw new NotImplementedException();
    }

    public void SetValueAsync(string value)
    {
      
    }
  }
}