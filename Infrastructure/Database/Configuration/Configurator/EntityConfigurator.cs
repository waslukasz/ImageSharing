using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration.Executer;

public class EntityConfigurator
{
    private List<IEntityConfiguration> _configurations;
    private readonly ModelBuilder _builder;

    public EntityConfigurator(ModelBuilder builder)
    {
        this._builder = builder;
        _configurations = new List<IEntityConfiguration>();
    }

    public void AddConfiguration(IEntityConfiguration configuration)
    {
        _configurations.Add(configuration);
    }

    public void Configure()
    {
        foreach (IEntityConfiguration configuration in this._configurations)
        {
            configuration.Configure(this._builder);
        }
    }
    
}