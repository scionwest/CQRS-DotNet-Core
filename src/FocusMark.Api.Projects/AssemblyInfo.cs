// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace FocusMark.Api.Projects
{
    internal class AssemblyInfo
    {
    }
}
