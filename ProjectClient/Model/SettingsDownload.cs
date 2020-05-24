using ProjectClient.ServerProject;

namespace ProjectClient.Model
{
    public struct SettingsDownload
    {
        public string Storage { get; set; }
        public string FileNameServer { get; set; }
        public string PathLocal { get; set; }
        public FileType TypeFile { get; set; }
    }
}
