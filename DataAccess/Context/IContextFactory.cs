namespace DataAccess.Context
{
    public interface IContextFactory
    {
        BlogContext CreateContext();
    }
}