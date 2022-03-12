using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POC
{
    class Application
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="response"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionGetHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var productProvider = new productProvider(new AmazonDynamoDBClient());

            var products = await productProvider.GetAllProductsAsync();
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = JsonConvert.SerializeObject(products)
                };
            }
           
        /// <summary>
        /// </summary>
        /// <param name="response"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionAddHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var product = JsonConvert.DeserializeObject<Product>(request.Body);
            if (product == null) return new APIGatewayProxyResponse { StatusCode = 400 };

            var ProductCreator = new ProductCreator(new AmazonDynamoDBClient());
            if (await ProductCreator.CreateProductAsync(product))
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200
                };
            }
            else
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = 400
                };
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="response"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionDeleteHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string ProductID = null;

            if (request.PathParameters != null && request.PathParameters.ContainsKey("ProductID"))
                ProductID = request.PathParameters["ProductID"];
            else if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey("ProductID"))
                ProductID = request.QueryStringParameters["ProductID"];

            if (ProductID == null) return new APIGatewayProxyResponse { StatusCode = 400 };

            var DeleteProduct = new DeleteProduct(new AmazonDynamoDBClient());
            if (await DeleteProduct.DeleteProductAsync(ProductID))
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200 //return ok
                };
            }
            else
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode = 400 //return bad request
                };
            }
        }


    }
}
