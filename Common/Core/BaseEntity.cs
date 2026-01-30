using System.ComponentModel.DataAnnotations.Schema;

namespace MoMo.Common.Core;

public abstract class BaseEntity 
{
    public Guid Id { get; set; }
    
}

