using System;
using System.Collections.Generic;
using System.Text;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using Product.Application;
using Product.Commands;

namespace Product.Functions
{
    public class FunctionAppConfiguration : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder host)
        {
            host
                .Setup((serviceCollection, commandRegistry) =>
                {
                    serviceCollection.AddProductApplication(commandRegistry);
                })
                .Functions(functions => functions
                    .HttpRoute("/api/v1/product", route => route
                        .HttpFunction<GetProductQuery>()
                    )
                );
        }
    }
}
