using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using POC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC
{
    class DeleteProduct : IProductDelete

    {
        private readonly IAmazonDynamoDB dynamoDB;

        public DeleteProduct(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;
        }

        /// <summary>
        /// DeleteProductAsync.
        /// For testing the Delete method
        /// {"queryStringParameters": {"ProductID": "2"}}
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductAsync(string ProductID)
        {
            var request = new DeleteItemRequest
            {
                TableName = "dat_Product",
                Key = new Dictionary<string, AttributeValue>()
                {
                    { "ProductID", new AttributeValue { S = ProductID } }
                }
            };

            var response = await dynamoDB.DeleteItemAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;

        }
    }
}
