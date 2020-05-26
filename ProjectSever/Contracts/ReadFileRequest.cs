using System.Runtime.Serialization;

namespace ProjectSever.Contracts
{
    [DataContract]
    public class ReadFileRequest
    {
        [DataMember]
        public string NameFile { get; set; }
    }
}
