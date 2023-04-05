using Mapster;
using MapsterMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(DurableFunctionsStudyCase.Startup))]
namespace DurableFunctionsStudyCase
{
    
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new TypeAdapterConfig();
            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
