using System;
using System.Threading.Tasks;

namespace igrohub.Example2.Windows
{
  public static class Authentification
  {
    public static string UserId => throw new NotImplementedException();
    public static string UserName => throw new NotImplementedException();
    public static bool UserEntered => throw new NotImplementedException();
    
    public static Task RegisterUser(string email, string password, string name = "")
    {
      throw new NotImplementedException();
    }
    
    public static Task SignIn(string email, string password)
    {
      throw new NotImplementedException();
    }

    public static void SignOut()
    {
      throw new NotImplementedException();
    }
  }
}