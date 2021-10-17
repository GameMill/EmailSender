using System.Xml.Serialization;

namespace EmailSender
{
  public class Address
  {
    [XmlAttribute]
    public string Host { get; set; } = "";

    [XmlAttribute]
    public int Port { get; set; } = 465;

    [XmlAttribute]
    public bool useSSL { get; set; } = true;

    public string Email { get; set; } = "";

    public string User { get; set; } = "";

    public string Pass { get; set; } = "";
  }
}
