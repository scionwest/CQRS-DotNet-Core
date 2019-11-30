using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using System.IO;
using System.Text;

namespace FocusMark.Api.Projects.Consumers
{
    public class CreateProjectConsumer
    {
        public void FunctionHandler(KinesisEvent kinesisEvent, ILambdaContext context)
        {
            context.Logger.LogLine($"Beginning to process {kinesisEvent.Records.Count} records...");

            foreach (var record in kinesisEvent.Records)
            {
                context.Logger.LogLine($"Event ID: {record.EventId}");
                context.Logger.LogLine($"Event Name: {record.EventName}");

                string recordData = GetRecordContents(record.Kinesis);
                context.Logger.LogLine($"Record Data:");
                context.Logger.LogLine(recordData);
            }

            context.Logger.LogLine("Stream processing complete.");
        }

        private string GetRecordContents(KinesisEvent.Record streamRecord)
        {
            using (var reader = new StreamReader(streamRecord.Data, Encoding.ASCII))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
