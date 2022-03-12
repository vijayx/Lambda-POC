using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using POC.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POC
{
    class productProvider : IProductGet
    {
        private readonly IAmazonDynamoDB dynamoDB;
        public productProvider(IAmazonDynamoDB dynamoDB)
        {
            this.dynamoDB = dynamoDB;

        }

        /// <summary>
        /// GetAllProductsAsync.
        /// </summary>
        /// <returns></returns>
        public async Task<Product[]> GetAllProductsAsync()
        {
            var result = await dynamoDB.ScanAsync(new ScanRequest
            {
                TableName = "dat_Product",

            });

            var Products = new List<Product>();

            if (result != null && result.Items != null)
            {
                foreach (var item in result.Items)
                {
                    item.TryGetValue("ProductID", out var ProductID);
                    item.TryGetValue("ProductName", out var ProductName);
                    item.TryGetValue("ProductDescription", out var ProductDescription);

                    Products.Add(new Product
                    {
                        ProductID = ProductID?.S,
                        ProductName = ProductName?.S,
                        ProductDescription = ProductDescription?.S

                    }); ;
                }
                return Products.ToArray();
            }
            return Array.Empty<Product>();
        }
    }
}
