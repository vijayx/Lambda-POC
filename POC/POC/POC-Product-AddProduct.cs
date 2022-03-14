using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC
{
    class ProductCreator : IProductAdd
    {
        private readonly IAmazonDynamoDB dynamoDB;

        public ProductCreator(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }

        /// <summary>
        /// CreateProductAsync
        /// Json object for testing
        /// {"body":"{\"ProductID\":\"3\",\"ProductName\":\"new product\",\"ProductDescription\":\"sample product description\"}","httpMethod": "POST"}
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> CreateProductAsync(Product product)
        {
            String ProductID = null;
            if (string.IsNullOrEmpty(product.ProductID))
            {
                ProductID = Guid.NewGuid().ToString();

            }
            else
            {
                ProductID = product.ProductID;
            }

            var request = new PutItemRequest
            {
                TableName = "dat_Product",
                Item = new Dictionary<string, AttributeValue>
                {{ "ProductID", new AttributeValue(ProductID) },
                    { "ProductName", new AttributeValue(product.ProductName) },
                    { "ProductDescription", new AttributeValue(product.ProductDescription) },

                }
            };
            var response = await dynamoDB.PutItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;

        }
    }
}
