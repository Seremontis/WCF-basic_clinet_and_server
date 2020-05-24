using System.Runtime.Serialization;

namespace ProjectSever.Messages
{
    [DataContract]
    public class ReadFileRequest
    {
        [DataMember]
        public string NameFile { get; set; }
    }
}
