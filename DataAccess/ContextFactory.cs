namespace DataAccess
{
    public class ContextFactory : IContextFactory
    {
        public BlogContext CreateContext()
        {
            return new BlogContext();
        }
    }
}