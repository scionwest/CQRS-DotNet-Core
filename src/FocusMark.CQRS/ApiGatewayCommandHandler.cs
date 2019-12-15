using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;
using System.Threading.Tasks;

namespace FocusMark.CQRS
{
    public class ApiGatewayCommandHandler : ApiGatewayHandler
    {
        public async Task<APIGatewayProxyResponse> RunHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            base.ProxyRequest = request;
            base.Logger = context.Logger;

            // Run query
            HandlerResponse response = await this.CommandHandler();

            // Response to client
            APIGatewayProxyResponse httpResponse = await this.CreateGatewayResponse(response);
            return httpResponse;
        }

        protected virtual Task<HandlerResponse> CommandHandler()
            => throw new NotImplementedException("This method is not to be invoked on the base class. Override it with an implementation.");

        protected override string GetDefaultContentType() => "application/plaintext";
    }
}
