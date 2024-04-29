using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Globalization;

namespace XJSerialize
{
    public class VehicleAuctionXmlSerializer
    {
        public void Serialize(XmlWriter writer, VehicleAuction obj)
        {

            writer.WriteStartElement("Root");  // Start the Root element

            writer.WriteStartElement("Vehicle");  // Start the Vehicle element

            foreach (var property in typeof(VehicleAuction).GetProperties())
            {
                object? value = property.GetValue(obj);
                switch (value)
                {
                    case null:
                        WriteEmptyElement(property.Name);
                        break;

                    case bool boolValue:
                        writer.WriteElementString(property.Name, boolValue ? "True" : "False");
                        break;

                    case DateTime dateValue:
                        writer.WriteElementString(property.Name, dateValue.ToString("dd/MM/yyyy"));
                        break;

                    case string stringValue when property.Name is "Images" or "Videos":
                        WriteMultipleElements(property.Name, stringValue);
                        break;

                    case string stringValue when string.IsNullOrEmpty(stringValue):
                        WriteEmptyElement(property.Name);
                        break;

                    case string stringValue:
                        writer.WriteElementString(property.Name, stringValue);
                        break;

                    default:
                        writer.WriteElementString(property.Name, value.ToString());
                        break;
                }
            }

            writer.WriteEndElement();  // End the Vehicle element

            writer.WriteEndElement();  // End the Root element

            void WriteEmptyElement(string name)
            {
                writer.WriteStartElement(name);
                writer.WriteEndElement();
            }

            void WriteMultipleElements(string type, string concatenatedValues)
            {
                var elements = concatenatedValues.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                writer.WriteStartElement(type);
                for (int i = 0; i < elements.Length; i++)
                {
                    writer.WriteElementString($"{type.Substring(0, type.Length - 1)}_{i + 1}", elements[i]);
                }
                writer.WriteEndElement();
            }
        }


        public VehicleAuction Deserialize(XmlReader reader)
        {
            VehicleAuction obj = new VehicleAuction();

            reader.ReadToFollowing("Vehicle");
            var imagesList = new List<string>();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    var propertyName = reader.Name;
                    if (propertyName.StartsWith("Image_"))
                    {
                        var imageUrl = reader.ReadElementContentAsString();
                        imagesList.Add(imageUrl);
                    }
                    var property = typeof(VehicleAuction).GetProperty(propertyName);

                    if (property != null)
                    {
                        if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?) )
                        {
                            var value = reader.ReadElementContentAsString();
                            property.SetValue(obj, value.Equals("TRUE", StringComparison.OrdinalIgnoreCase));
                        }
                        else if (property.PropertyType == typeof(DateTime))
                        {
                            var dateStr = reader.ReadElementContentAsString();
                            property.SetValue(obj, DateTime.ParseExact(dateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        }
                        else if (propertyName == "Images")
                        {
                        }
                        else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                        {
                            var value = reader.ReadElementContentAsString();
                            property.SetValue(obj, int.Parse(value));
                        }
                        else if (property.PropertyType == typeof(short))
                        {
                            var value = reader.ReadElementContentAsString();
                            property.SetValue(obj, short.Parse(value));
                        }
                        else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                        {
                            var value = reader.ReadElementContentAsString();
                            property.SetValue(obj, decimal.Parse(value));
                        }

                        else if (property.PropertyType == typeof(string) )
                        {
                            var value = reader.ReadElementContentAsString();
                            property.SetValue(obj, value);
                        }
                    }
                }
            }

            var imagesProperty = typeof(VehicleAuction).GetProperty("Images");
            if (imagesProperty != null)
            {
                imagesProperty.SetValue(obj, string.Join(",", imagesList));
            }

            return obj;
        }

    }

}
