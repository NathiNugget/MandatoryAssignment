// See https://aka.ms/new-console-template for more information
using MandatoryAssignment;
using MandatoryAssignment.TracingClasses;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Xml;

Console.WriteLine("Hello, World!");



XmlDocument doc = new XmlDocument();
doc.Load("C:\\Users\\natha\\source\\repos\\MandatoryAssignment\\TestFramework\\config.xml");


List<string> vals = new List<string>();
XmlNode root = doc.DocumentElement;
foreach (XmlNode item in root)
{
    vals.Add(item.Name);
}



Dictionary<string, string> configProperties = new();



foreach (var item in vals)
{
    configProperties.Add(item, doc.SelectSingleNode("xml/" + item).InnerText); 
}

LoggerHelper logger = LoggerHelper.GetInstance(configProperties["loggerName"] ?? "<logger>");
//logger.SetFilter(SourceLevels.Information);
logger.SetFilter(SourceLevels.Verbose);
logger.Add(new CustomTraceListener());
logger.Add(new JsonTraceListener());
logger.TraceEvent(TraceEventType.Information, 1, "Applikation starter");

World world = new(20, 20, new(), new());








