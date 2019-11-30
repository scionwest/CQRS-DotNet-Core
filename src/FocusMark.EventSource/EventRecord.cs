namespace FocusMark.EventSource
{
    public class EventRecord
    {
        public EventRecord(int version, object data, string streamName)
        {
            this.Version = version;
            this.Data = data;
            this.StreamName = streamName;
        }

        public int Version { get; }
        public object Data { get; }

        public string StreamName { get; }
    }
}
