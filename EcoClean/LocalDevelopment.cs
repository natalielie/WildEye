using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EcoClean
{
    public static class LocalDevelopment
    {
        public static bool IsLocalDevelopment(this IWebHostEnvironment env)
        {
            return (env.IsEnvironment("LocalDevelopment"));
        }
    }
}
