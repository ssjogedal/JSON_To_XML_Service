using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using System.Net;



namespace WcfService
{
    
    [ServiceContract]
    public interface IService
    {
       
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        XElement GetTestData();

        [OperationContract]
        XElement GetAllInterchanges();

        [OperationContract]
        XElement FilterByInterchangeID(int id);

        [OperationContract]
        XElement FilterByInterchangeNode(string node);

        [OperationContract]
        XElement FilterByInterchangeIDAndNode(int id, string node);

        [OperationContract]
        XElement FilterByInterchangeNodeAndValue(string node, string value);

    }
        // Remove this?
        [DataContract]
        public class CompositeType 
        {
            bool boolValue = true;
            string stringValue = "Hello ";

            [DataMember]
            public bool BoolValue
            {
                get { return boolValue; }
                set { boolValue = value; }
            }

            [DataMember]
            public string StringValue
            {
                get { return stringValue; }
                set { stringValue = value; }
            }
        }
    }
