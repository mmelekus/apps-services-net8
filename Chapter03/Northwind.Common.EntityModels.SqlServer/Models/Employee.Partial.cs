using System.ComponentModel.DataAnnotations.Schema; // [NotMapped]

namespace Northwind.Common.EntityModels.SqlServer.Models;

public partial class Employee : IHasLastRefreshed
{
    [NotMapped]
    public DateTimeOffset LastRefreshed { get; set; }
}
