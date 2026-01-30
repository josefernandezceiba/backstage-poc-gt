using MoMo.Common.Core;
using System.ComponentModel.DataAnnotations;

namespace MoMo.Modules.Onboarding.Core;

public class User(string nid, string name, string email) : BaseEntity
{

    public string Nid { get; init; } = nid;
    public string Name { get; init; } = name;
    public string Email { get; init; } = email;
    public string DisplayName => $"{Name} <{Email}>";
}