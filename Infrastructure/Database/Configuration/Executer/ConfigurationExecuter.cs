using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration.Executer;

public class ConfigurationExecuter
{
    private List<IEntityConfiguration> _configurations;
    private readonly ModelBuilder _builder;

    public ConfigurationExecuter(ModelBuilder builder)
    {
        this._builder = builder;
        _configurations = new List<IEntityConfiguration>();
    }

    public void AddConfiguration(IEntityConfiguration configuration)
    {
        _configurations.Add(configuration);
    }

    public void Execute()
    {
        _configurations.ForEach(
            c => c.Configure(this._builder)
            );
    }
    
}