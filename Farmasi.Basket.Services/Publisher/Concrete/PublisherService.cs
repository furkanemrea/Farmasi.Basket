using Amazon.SQS;
using Amazon.SQS.Model;
using Farmasi.Basket.Services.Publisher.Abstraction;
using Microsoft.Extensions.Configuration;

namespace Farmasi.Basket.Services.Publisher.Concrete
{
    public class PublisherService:IPublisherService
    {
        private AmazonSQSClient sqsClient;
        public PublisherService(IConfiguration configuration)
        {
            string secretKey = configuration["AWS:SecretKey"].ToString();

            string accessKey = configuration["AWS:AccessKey"].ToString();

            sqsClient = new AmazonSQSClient(secretKey, accessKey, Amazon.RegionEndpoint.EUCentral1);

        }

        public async Task Send(string message)
        {
            await sqsClient.SendMessageAsync(new SendMessageRequest()
            {
                DelaySeconds = 30,
                MessageBody = message,
                QueueUrl = "email-sender"
            });
        }
    }
}
