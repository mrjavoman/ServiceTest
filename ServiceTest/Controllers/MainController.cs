using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;
using BusinessLogic;
using System.Collections;
using System.Data;

namespace ServiceTest.Controllers
{
    public class MainController : ApiController
    {
        public Dictionary<string, string> Get()
        {
            Stack<DFS_Node> Graph = new Stack<DFS_Node>();
            Dictionary<string, string> list;

            

            //var xml = new XmlDocument();
            //var xmlText = "<wd:person><wd:name>John</wd:name></wd:person>";
            var xmlText = "<person><firstname>John</firstname><lastname>smith</lastname><props><address>123 street</address></props></person>";

            //xmlText = xmlText.Replace("wd:", "");
            //xml.LoadXml(xmlText);

            //var obj = JsonConvert.SerializeObject(xml);
            //var test = JsonConvert.DeserializeObject(obj);

            // Getting xml output
            var xmlObj = XmlFactory.sample(xmlText);

            // Adds first element to queue
            Graph.Push(new DFS_Node(xmlObj.XElement));

            list = DFS_Iter(Graph);


            return list;
        }

        private Dictionary<string,string> DFS_Iter(Stack<DFS_Node> Graph)
        {
            Dictionary<string, string> propList = new Dictionary<string, string>();

            while (Graph.Count > 0)
            {
                DFS_Node node = Graph.Pop();
                if (!node.visited)
                {
                    node.visited = true;

                    if (node.node.Descendants().Count() > 0)
                    {
                        foreach (XElement adjNode in node.node.Nodes())
                        {
                            Graph.Push(new DFS_Node(adjNode));
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Name: {node.node.Name.LocalName}, Value = {node.node.Value}");
                        propList.Add(node.node.Name.LocalName, node.node.Value);
                    }
                }
            }

            return propList;
        }

        private Dictionary<string, string> DFS(DFS_Node el, Dictionary<string, string> list)
        {
            el.visited = true;
            if(el.node.Descendants().Count() > 0)
            {
                foreach(XElement adjNode in el.node.Nodes())
                {
                    if (!el.visited)
                    {
                        list = DFS(el, list);
                    }
                }
                
            }
            else
            {
                list.Add(el.node.Name.LocalName, el.node.Value);
            }

            return list;
        }
    }

    class DFS_Node
    {
        public XElement node { get; set; }
        public bool visited { get; set; }

        public DFS_Node(XElement n, bool v = false)
        {
            node = n;
            visited = v;
        }
    }
}
