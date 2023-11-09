namespace neatbook.Application.FunctionalTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new SqlServerTestDatabase();

        await database.InitialiseAsync();

        return database;
    }
}
