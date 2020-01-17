using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfService;
using System.Net;
using System.Xml.Linq;


namespace Client
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConnectToInterchangeService conServ = new ConnectToInterchangeService();

            string SID = "";
            string option = "hi";
            int i = 10;
            while(i != 0){
                Console.WriteLine("Hi, and welcome to this service");
                Console.WriteLine("1. GetAll");
                Console.WriteLine("2. GetTestData");
                Console.WriteLine("3. GetFilteredByID");
                Console.WriteLine("4. GetFilteredByNode");
                Console.WriteLine("5. GetFilteredByIDAndNode");
                Console.WriteLine("6. GetFilteredByNodeValue");
                Console.WriteLine("7. Read new file");
                Console.WriteLine("8. Save Result");
                Console.WriteLine("9. Quit");

                option = Console.ReadLine();
                if(CheckID(option) == true)
                {
                   i= Int32.Parse(option);
                }
             
                
                if(i < 1 || i > 9)
                {
                    Console.WriteLine("Please choose a number from the list");
                }
                switch(i)
                {
                
                    case 1: 
                        conServ.GetAll();
                        Console.WriteLine(conServ.Result);
                        break;
                    case 2:
                        conServ.GetTestData();
                        Console.WriteLine(conServ.Result);
                        break;
                    case 3:
                        Console.WriteLine("Type in the ID you are looking for");
                        SID = Console.ReadLine();

                        if(CheckID(SID) == true)
                        {
                        conServ.GetFilteredByID(Int32.Parse(SID));
                        Console.WriteLine(conServ.Result);
                        }
                        else
                        {
                            Console.WriteLine("Please use a valid ID-format");
                        }
                        
                        break;
                    case 4:
                        Console.WriteLine("Which node are you looking for?");
                        conServ.GetFilteredByNode(Console.ReadLine());
                        Console.WriteLine(conServ.Result);
                        break;
                    case 5:
                        bool correctId = false;
                        
                        while ( correctId == false) {
                            Console.WriteLine("Which ID are you looking for?");
                            SID = Console.ReadLine();
                            correctId = CheckID(SID);
                            if(correctId == false)
                            {
                                Console.WriteLine("Please use a valid ID-format");
                            }
                        }
                        int id = Int32.Parse(SID);
                        Console.WriteLine("Which node are you looking for?");
                        string node = Console.ReadLine();
                        conServ.GetFilteredByIDAndNode(id, node);
                        Console.WriteLine(conServ.Result);
                        break;
                    case 6:
                        Console.WriteLine("Type in the node you are looking for");
                        string node2 = Console.ReadLine();
                        Console.WriteLine("Type in the value you are looking for");
                        string nodeValue = Console.ReadLine();
                        conServ.GetFilteredByNodeValue(node2, nodeValue);
                        Console.WriteLine(conServ.Result);
                        break;
                    case 7:
                        XElement info = XElement.Parse(FileBackup.LoadFile());
                        
                        Console.WriteLine(PrettyInfoPrint(info));
                        break;
                    case 8:
                        if(conServ.Result != null)
                        {
                            FileBackup.SaveToFile(conServ.Result.ToString());
                            Console.WriteLine("Result saved");

                        }
                        else
                        {
                            Console.WriteLine("No result to save");
                        }
                        
                        break;

                    case 10:
                        if (conServ.Result != null)
                        {
                            string s = FileBackup.LoadFile();
                            Console.WriteLine(s);

                        }
                        else
                        {
                            Console.WriteLine("No result to save");
                        }

                        break;
                    case 9:
                        Console.WriteLine("Did you save your result?" + "\n" + "1. Save and quit" + "\n" + "2. Quit");
                        string svar = Console.ReadLine();

                        if (svar == "1")
                        {
                            FileBackup.SaveToFile(conServ.Result.ToString());
                            Console.WriteLine("Saving result and quiting...");
                            i = 0;
                        }
                        else if (svar == "2")
                        {
                            Console.WriteLine("Quiting without saving result...");
                            i = 0;
                        }
                        else
                        {
                        }
                        break;
      
                }

            }
            string PrettyInfoPrint(XElement text)
            {
                string prettyString = "";
                int j = 0;
                foreach (XElement element in text.Descendants("Interchange"))
                {
                    j++;
                    string interchange = "Interchange nmbr: " + j;

                    var patient = from p in element.Descendants("StructuredPersonName")
                                  let namn = p.Element("FirstGivenName").Value + " " + p.Element("FamilyName").Value
                                  select namn;

                    var physician = (from p in element.Descendants("HealthcarePerson")
                                     select p.Element("Name").Value).GroupBy(x => x).Select(x => x.First());

                    var medicine = from m in element.Descendants("ManufacturedProductId")
                                   select m.Element("ProductId").Value;

                    var dosage = from d in element.Descendants("UnstructuredInstructionsForUse")
                                 select d.Element("UnstructuredDosageAdmin").Value;

                    XElement info = new XElement("Info",
                                        new XElement("Patient", patient),
                                        new XElement("Physician", physician),
                                        new XElement("Medicine", medicine),
                                        new XElement("Dosage", dosage));

                    // Alternativ för att returnera string (?)
                    prettyString = prettyString + interchange + "\n" + "Patient: " + info.Element("Patient").Value + "\n" + "Physician: " + info.Element("Physician").Value + "\n" + "Medicine: " + info.Element("Medicine").Value + "\n" + "Dosage: " + info.Element("Dosage").Value + "\n";

                }
                

                return prettyString;






            }
            bool CheckID(string id)
            {
                foreach (char c in id)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
                return true;
            }


        }
    }
}
