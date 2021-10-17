using System.IO;
using System.Xml.Serialization;

namespace EmailSender
{
  public class Settings
  {
    public Address From { get; set; }

    public string Body { get; set; }

    public string Subject { get; set; }

    public string To { get; set; }

    [XmlAttribute]
    public bool DeliveryConfirmation { get; set; } = true;

    [XmlAttribute]
    public bool OnReadConfirmation { get; set; } = true;

    [XmlAttribute]
    public bool MessageSentConfirmation { get; set; } = true;

    [XmlAttribute]
    public bool Log { get; set; } = true;

    public void save()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new XmlSerializer(typeof (Settings)).Serialize((Stream) memoryStream, (object) this);
        File.WriteAllBytes("Data.xml", memoryStream.ToArray());
      }
    }

    public static Settings Load(string File)
    {
      using (FileStream fileStream = new FileStream(File, FileMode.Open))
        return new XmlSerializer(typeof (Settings)).Deserialize((Stream) fileStream) as Settings;
    }
  }
}
