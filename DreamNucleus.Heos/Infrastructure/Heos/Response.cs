namespace DreamNucleus.Heos.Infrastructure.Heos
{
    public class HeosResponse
    {
        public Heos Heos { get; set; }
    }
    public class HeosResponse<T>
    {
        public Heos Heos { get; set; }
        public T Payload { get; set; }
    }
    public class Response
    {
        public bool Received { get; }

        public int Sequence { get; }
        public bool Success { get; }

        public HeosResponse HeosResponse { get; }
        public string Message { get; set; }

        public Response(int sequence)
        {
            Sequence = sequence;
            Received = false;
        }

        public Response(int sequence, bool success, HeosResponse heosResponse, string message)
        {
            Sequence = sequence;
            Success = success;
            HeosResponse = heosResponse;
            Message = message;
            Received = true;
        }

    }

    public static class ResultParser
    {
        public static T Parse<T>(string message) where T : new()
        {
            return new T();
        }
    }

    public class Response<T>
    {
        /// <summary>
        /// If a response was received
        /// </summary>
        public bool Received { get; }

        public int Sequence { get; }

        /// <summary>
        /// If the response was received and a success
        /// </summary>
        public bool Success { get; }

        public T Payload { get; }

        public Response(Response response, T payload)
        {
            Payload = payload;
            Received = response.Received;
            Sequence = response.Sequence;
            Success = response.Success;
        }
    }

    public class Heos
    {
        public string Command { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
    }
}
