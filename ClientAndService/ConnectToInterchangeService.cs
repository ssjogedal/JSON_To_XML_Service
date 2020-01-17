using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WcfService;

namespace Client
{
    public class ConnectToInterchangeService
    {
        Service service = new Service();

        private XElement _Result;
        public XElement Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
                _Result.Add(new XAttribute("DatumOchTid", DateTime.Now));
                
            }
        }

        public void GetAll()
        {
            Result = service.GetAllInterchanges();
        }
        public void GetTestData()
        {
            Result = service.GetTestData();
        }
        public void GetFilteredByID(int id)
        {
            Result = service.FilterByInterchangeID(id);
        }
        public void GetFilteredByNode(string node)
        {
            Result = service.FilterByInterchangeNode(node);
        }
        public void GetFilteredByIDAndNode(int id, string node)
        {
            Result = service.FilterByInterchangeIDAndNode(id, node);
        }
        public void GetFilteredByNodeValue(string node, string nodeValue)
        {
            Result = service.FilterByInterchangeNodeAndValue(node, nodeValue);
        }




    }
}
