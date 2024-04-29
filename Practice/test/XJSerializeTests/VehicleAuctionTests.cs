using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using XJSerialize;

namespace XJSerializeTests
{
    public class VehicleAuctionTests
    {
        [Fact]
        public void Can_VehicleAuction_Be_Deseralized()
        {
            var doc = """
                        <Root>
                        <Vehicle>
                        <MemberID>26</MemberID>
                        <ExportID>5463433</ExportID>
                        <AuctionID>499278</AuctionID>
                        <VehicleID>3562988</VehicleID>
                        <Ref>LF58EDV</Ref>
                        <SiteID>4</SiteID>
                        <Manufacturer>YAMAHA</Manufacturer>
                        <Model>CJ</Model>
                        <RegNo>LF58EDV</RegNo>
                        <RegYear>2008</RegYear>
                        <Colour>SILVER</Colour>
                        <Fuel>PETROL</Fuel>
                        <Damage>sgfsdafsda</Damage>
                        <Doors>0</Doors>
                        <Body>SCOOTER</Body>
                        <CC>80</CC>
                        <Speedo/>
                        <TransSpeed/>
                        <TransType/>
                        <TrimLevel>80 / CHAMP</TrimLevel>
                        <Engine>NATURALLY ASPIRATED</Engine>
                        <Cat>B</Cat>
                        <ReservePrice>600.0000</ReservePrice>
                        <StartPrice>200.0000</StartPrice>
                        <HasVAT>False</HasVAT>
                        <Keys>False</Keys>
                        <Starts>2</Starts>
                        <Drives>2</Drives>
                        <Stereo>False</Stereo>
                        <VINPlate>True</VINPlate>
                        <VINNumber>JYAVM01E48A124245</VINNumber>
                        <LogBook>False</LogBook>
                        <CanBeViewed>YES</CanBeViewed>
                        <FeaturedVehicle>False</FeaturedVehicle>
                        <InsCoName>Insurance Company 106</InsCoName>
                        <BranchCode>106</BranchCode>
                        <ServiceHistory>False</ServiceHistory>
                        <NSAOwnVehicle>False</NSAOwnVehicle>
                        <CarWebID>0</CarWebID>
                        <CarWebMake>YAMAHA</CarWebMake>
                        <CarWebModel>CJ</CarWebModel>
                        <CarWebModelSeries/>
                        <AdminFeeExVAT>0.0000</AdminFeeExVAT>
                        <EngineCode/>
                        <AuctionFeeVAT>0.0000</AuctionFeeVAT>
                        <AdditionalFee>0.00</AdditionalFee>
                        <BodyTypeID>0</BodyTypeID>
                        <DateFirstReg>01/09/2021</DateFirstReg>
                        <Bundle>False</Bundle>
                        <CAP_Average>0.0000</CAP_Average>
                        <CostOfRepair>0.00</CostOfRepair>
                        <InsuranceBranchName>Insurance Branch 3175</InsuranceBranchName>
                        <Images>
                        <Image_1>https://s3.eu-west-2.amazonaws.com/dhsystems-image-drive/Vehicle/2024/April/3f6af017-db11-4c4c-9296-9c12364a4009_29680071.jpg</Image_1>
                        <Image_2>https://s3.eu-west-2.amazonaws.com/dhsystems-image-drive/Vehicle/2024/April/41234621-b959-4f1c-8cfd-086f37837d89_29680072.jpg</Image_2>
                        <Image_3>https://s3.eu-west-2.amazonaws.com/dhsystems-image-drive/Vehicle/2024/April/2f444bc1-fea2-4ba0-8853-15c199f4c972_29680073.jpg</Image_3>
                        <Image_4>https://s3.eu-west-2.amazonaws.com/dhsystems-image-drive/Vehicle/2024/April/c861996b-ef26-4ae7-b0b3-f32cc0f13f8e_29680074.jpg</Image_4>
                        <Image_5>https://s3.eu-west-2.amazonaws.com/dhsystems-image-drive/Vehicle/2024/April/c0e20507-f272-4e57-861f-b7ae6a0494ef_29680075.jpg</Image_5>
                        <Image_6>https://s3.eu-west-2.amazonaws.com/dhsystems-image-drive/Vehicle/2024/April/a67b64c2-6837-4bba-be92-9ad5eac11d08_29680076.jpg</Image_6>
                        <Image_7>https://s3.eu-west-2.amazonaws.com/dhsystems-image-drive/Vehicle/2024/April/9e7f115c-ad0f-4884-a298-251cb56f03d0_29680077.jpg</Image_7>
                        <Image_8>https://s3.eu-west-2.amazonaws.com/dhsystems-image-drive/Vehicle/2024/April/2e4b480a-61f7-4ce7-acdd-3e7c34852af4_29680078.jpg</Image_8>
                        </Images>
                        <Videos/>
                        </Vehicle>
                        </Root>
                        """;
            doc = doc.Replace("\r", "").Replace("\n", "");
            VehicleAuction vehicleAuction;
            var vehicleAuctionXmlSerializer = new VehicleAuctionXmlSerializer();
            using (StringReader stringReader = new StringReader(doc))
            using (XmlReader xmlReader = XmlReader.Create(stringReader))
            {
                 vehicleAuction = vehicleAuctionXmlSerializer.Deserialize(xmlReader);
                //while (xmlReader.Read())
                //{
                //    Console.WriteLine($"Type: {xmlReader.NodeType}, Name: {xmlReader.Name}, Value: {xmlReader.Value}");
                //}
            }

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true, // Prevents the XML declaration from being written.
                Encoding = Encoding.UTF8, // Specify your desired encoding, if UTF-8 or another.
                Indent = false // Optional: Set to true if you want the output to be indented.
            };
            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                vehicleAuctionXmlSerializer.Serialize(xmlWriter, vehicleAuction);
            }



            var pp = stringBuilder.ToString();

            Assert.NotNull(pp);
            Assert.True(pp==doc);
            //var vehicleAuctionXmlSerializer = new VehicleAuctionXmlSerializer();

            //using (var xmlReader = XmlReader.Create(new StringReader(doc)))
            //{
            //    while (xmlReader.Read())
            //    {
            //        Console.WriteLine($"Type: {xmlReader.NodeType}, Name: {xmlReader.Name}, Value: {xmlReader.Value}");
            //    }

            //    // how to check if the deserialization is successful?
            //    //var vehicleAuction = vehicleAuctionXmlSerializer.Deserialize(xmlReader);

            //    //Assert.NotNull(vehicleAuction);
            //}





            //XmlWriterSettings settings = new XmlWriterSettings
            //{
            //    Indent = true,  // Enables indentation for the output XML for better readability
            //    NewLineOnAttributes = false, // Put new lines on attributes
            //    Encoding = Encoding.UTF8, // Sets the character encoding to UTF-8
            //    OmitXmlDeclaration = false // Include XML declaration
            //};
            //string filePath = "output.xml"; // Define the path to the output file

            //using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            //{
            //    // Start the document
            //    writer.WriteStartDocument();
            //    writer.WriteStartElement("greetings"); // Root element

            //    // Write some content
            //    writer.WriteElementString("greeting", "Hello, world!");

            //    // End the root element
            //    writer.WriteEndElement();

            //    // End the document
            //    writer.WriteEndDocument();
            //}
        }
    }
}
