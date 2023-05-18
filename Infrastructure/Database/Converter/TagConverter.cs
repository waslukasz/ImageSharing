using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converter;

public class TagConverter : ValueConverter<ICollection<string>, string>
{
    public const char SEPARATOR = '|';

    public TagConverter() : base(
        s => string.Join(SEPARATOR,s),
        s => s.Split(SEPARATOR,StringSplitOptions.RemoveEmptyEntries))
    {
    }
}