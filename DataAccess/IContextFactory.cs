namespace DataAccess
{
    public interface IContextFactory
    {
        BlogContext CreateContext();
    }
}