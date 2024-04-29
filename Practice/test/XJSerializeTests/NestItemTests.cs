using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XJSerialize;

namespace XJSerializeTests
{
    public class NestItemTests
    {
        [Fact]
        public void NestItemsTest()
        {
            string xml = @"<Root>
                            <Vehicle>
                                <MemberID>26</MemberID>
                            </Vehicle>
                            <Vehicle>
                                <MemberID>27</MemberID>
                            </Vehicle>
                        </Root>";

            using (StringReader stringReader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader))
                {
                    VehicleAuctionXmlReader reader = new VehicleAuctionXmlReader();
                    VehicleAuctionTest auction = reader.Deserialize(xmlReader);

                    foreach (Vehicle vehicle in auction.Vehicles)
                    {
                        Console.WriteLine("Member ID: " + vehicle.MemberID);
                    }
                }
            }
        }
    }

    public class VehicleAuctionXmlReader
    {
        public VehicleAuctionTest Deserialize(XmlReader reader)
        {
            VehicleAuctionTest auction = new VehicleAuctionTest();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Root")
                {
                    
                    reader.ReadToFollowing("Vehicle");
                    var vehicle = new Vehicle();
                    // Ensure the reader is positioned at the start of the element
                    if (reader.IsStartElement())
                    {
                        // Read the next sub-element inside <Vehicle>
                        reader.ReadToFollowing("MemberID");

                        // Assuming MemberID is always present and is an integer
                        if (!reader.IsEmptyElement)
                        {
                            vehicle.MemberID = reader.ReadElementContentAsInt();
                            auction.Vehicles.Add(vehicle);
                        }

                        // The ReadElementContentAsInt method advances the reader to the next node
                        // No need to call ReadEndElement here because ReadElementContentAsInt handles it
                    }
                }
            }

            return auction;
        }
    }

    public class Vehicle
    {
        public int MemberID { get; set; }
    }

    public class VehicleAuctionTest
    {
        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
