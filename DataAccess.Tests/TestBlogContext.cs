using System.Diagnostics;
using DataAccess.Context;

namespace DataAccess.Tests
{
    public class TestBlogContext : BlogContext
    {
        public TestBlogContext()
        {
            Debug.Write(Database.Connection.ConnectionString);
        }
    }
}
