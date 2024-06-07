using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureTest;

public class TestHelper
{
    private readonly Context _context;
    public TestHelper()
    {
        var builder = new DbContextOptionsBuilder<Context>();
        builder.UseInMemoryDatabase(databaseName: "TestDB");

        var dbContextOptions = builder.Options;
        _context = new Context(dbContextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public UnitOfWork UnitOfWork
    {
        get { return new UnitOfWork(_context, new UserRepository(_context)); }
    }
}