using ProjectSever.Enums;
using System.Runtime.Serialization;

namespace ProjectSever.Messages
{
    [DataContract]
    public class FileTransferRequest
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public FileType FileType { get; set; } 
    }
}
