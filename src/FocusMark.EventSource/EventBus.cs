using Amazon;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FocusMark.EventSource
{
    public class EventBus
    {
        private readonly IAmazonKinesis client;

        public EventBus()
        {
            var config = new AmazonKinesisConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1,
            };

             client = new AmazonKinesisClient(config);
        }

        public async Task PublishMessage(EventRecord message)
        {
            byte[] buffer = JsonSerializer.SerializeToUtf8Bytes(message);
            using (var memoryStream = new MemoryStream(buffer))
            {
                var request = new PutRecordRequest
                {
                    Data = memoryStream,
                    PartitionKey = message.Data.GetType().FullName,
                    StreamName = message.StreamName,
                };

                PutRecordResponse response = await client.PutRecordAsync(request);
            }
        }
    }
}
