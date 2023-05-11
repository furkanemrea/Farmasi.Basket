using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Farmasi.Basket.Services.Publisher.Concrete.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Farmasi.Basket.Email.Sender;

public class Function
{
    public Function()
    {

    }

    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        try
        {
            var emailMessageModel = JsonConvert.DeserializeObject<EmailMessageModel>(message.Body);
            if (emailMessageModel != null)
            {
                SendEmail(emailMessageModel.Email);
                context.Logger.LogInformation($"Email Sent to {message.Body}");
            }
        }
        catch (Exception ex)
        {
            context.Logger.LogInformation($"{ex.Message} ~~ {message.Body}");
        }


        context.Logger.LogInformation($"Processed message {message.Body}");
        await Task.CompletedTask;
    }

    public void SendEmail(string email)
    {
        // to do : send email
    }
}