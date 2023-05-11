using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using StackExchange.Redis;
using System.Collections.Generic;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Farmasi.Basket.Lambda.Manager;

public class Function
{
    public Function()
    {

    }
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach(var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);
        }
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processed message {message.Body}");



        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis-13882.c92.us-east-1-3.ec2.cloud.redislabs.com:13882");

        IDatabase db = redis.GetDatabase();

        bool exists = db.HashExists(, "Id");

        await Task.CompletedTask;
    }
}