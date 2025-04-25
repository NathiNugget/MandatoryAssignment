public class CustomTraceListener : System.Diagnostics.TraceListener
{
    private readonly string _path;
    private StreamWriter sw;

    public CustomTraceListener()
    {
        _path = "log.txt";
        sw = File.AppendText(Directory.GetCurrentDirectory() + "\\anders.txt");
        sw.AutoFlush = true;
    }

    public override void Write(string? message)
    {
        sw.Write(message);
    }

    public override void WriteLine(string? message)
    {
        sw.WriteLine(message);
    }
}
