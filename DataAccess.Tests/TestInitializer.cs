using System.Data.Entity;

namespace DataAccess.Tests
{
    public class TestInitializer : DropCreateDatabaseAlways<TestBlogContext>
    {
    }
}
