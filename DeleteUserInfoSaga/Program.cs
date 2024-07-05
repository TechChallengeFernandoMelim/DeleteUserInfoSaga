using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using DeleteUserInfoSaga.Logger;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

string accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_DYNAMO");
string secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY_DYNAMO");

AWSCredentials credentials = new BasicAWSCredentials(accessKey, secretKey);

var sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);

builder.Services.AddSingleton(sqsClient);
builder.Services.AddSingleton<SqsLogger>();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapDelete("/DeleteUserData/{cpf}", async (HttpRequest request, string cpf, SqsLogger logger) =>
{
    try
    {
        var accessToken = request.Headers["Authorization"].ToString();

        if (DeleteUserTotemData(cpf, accessToken))
            DeleteUserLoginData(cpf, accessToken);

    }
    catch (Exception ex)
    {
        await logger.Log(ex.StackTrace, ex.Message, ex.ToString());
    }
});

bool DeleteUserTotemData(string cpf, string accessToken)
{
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Clear();
    httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("USER_API_GATEWAY_URL"));
    httpClient.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");

    var path = $"/ApiGatewayStage/v1/Order/DeleteUserData/{cpf}";

    var result = httpClient.DeleteAsync(path).Result;

    if (result.IsSuccessStatusCode)
        return true;

    return false;
}

bool DeleteUserLoginData(string cpf, string accessToken)
{
    var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Clear();
    httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("USER_API_GATEWAY_URL"));
    httpClient.DefaultRequestHeaders.Add("Authorization", $"{accessToken}");

    var path = $"/ApiGatewayStage/User/DeleteUserData/{cpf}";

    var result = httpClient.DeleteAsync(path).Result;

    if (result.IsSuccessStatusCode)
        return true;

    return false;
}

app.Run();
