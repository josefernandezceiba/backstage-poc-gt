namespace MoMo.Common.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiEndpointAttribute(string module) : Attribute
    {
        public string ModuleName => module;

    }
}
