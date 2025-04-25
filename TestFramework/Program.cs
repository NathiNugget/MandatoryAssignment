// See https://aka.ms/new-console-template for more information
using MandatoryAssignment;
using MandatoryAssignment.Interfaces;
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

World world = new(int.Parse(configProperties["maxX"]), int.Parse(configProperties["maxY"]), new(), new());
Creature creature1 = new("Creature1", 100, world);
WorldObject wo1 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword1", 3, 2), 

});
WorldObject wo2 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword2", 3, 2),

});
WorldObject wo3 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword3", 3, 2),

});
WorldObject wo4 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword4", 3, 2),

});
WorldObject wo5 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword5", 3, 2),

});
WorldObject wo6 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword6", 3, 2),

});
WorldObject wo7 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword7", 3, 2),

});
WorldObject wo8 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword8", 3, 2),

});
WorldObject wo9 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword9", 3, 2),

});
WorldObject wo10 = new("AttackChest", true, 1, 1, new()
{
    new AttackItem("Sword10", 3, 2),

});

world.Creatures.Add(creature1); 
world.WorldObjects.Add(wo1);
world.WorldObjects.Add(wo2);
world.WorldObjects.Add(wo3);
world.WorldObjects.Add(wo4);
world.WorldObjects.Add(wo5);
world.WorldObjects.Add(wo6);
world.WorldObjects.Add(wo7);
world.WorldObjects.Add(wo8);
world.WorldObjects.Add(wo9);
world.WorldObjects.Add(wo10);


List<Creature> list = new List<Creature>();

while (world.Creatures.Count != 0)
{
    foreach (Creature c in world.Creatures)
    {
        if (!c.IsDead)
        {
            c.ReceiveHit(2);
            WorldObject? item = world.WorldObjects.LastOrDefault();
            if (item != null)
            {
                c.Loot(item);
                world.WorldObjects.Remove(item);
            }
        }
        else
        {
            list.Add(c);
        }
        

    }
    world.NextRound();
    foreach (Creature c in list)
    {
        world.Creatures.Remove(c);
    }
    list.Clear();
}







