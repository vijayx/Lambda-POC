
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace POC
{
    public class Function
    {
        private ServiceCollection _serviceCollection;
        public Function()
        {
            ConfigureServices();

        }

        /// <summary>
        /// FunctionGetProducts.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<APIGatewayProxyResponse> FunctionGetProducts(APIGatewayProxyRequest request, ILambdaContext context)
        {
            using ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();

            // entry to run app.
            return serviceProvider.GetService<Application>().FunctionGetHandlerAsync(request, context);
        }

        /// <summary>
        /// FunctionAddProduct.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionAddProduct(APIGatewayProxyRequest request, ILambdaContext context)
        {
            using ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();

            // entry to run app.
            return await serviceProvider.GetService<Application>().FunctionAddHandlerAsync(request, context);
        }

        /// <summary>
        /// FunctionDeleteProduct.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionDeleteProduct(APIGatewayProxyRequest request, ILambdaContext context)
        {
            using ServiceProvider serviceProvider = _serviceCollection.BuildServiceProvider();

            // entry to run app.
            return await serviceProvider.GetService<Application>().FunctionDeleteHandlerAsync(request, context);
        }

        /// <summary>
        /// ConfigureServices.
        /// </summary>
        /// <returns></returns>
        private void ConfigureServices()
        {
            // add dependencies here
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddTransient<Application>();
        }
        
    }
}
