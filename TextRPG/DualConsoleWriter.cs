using System;
using System.IO;
using System.Text;

public class DualConsoleWriter : TextWriter
{
    private readonly TextWriter originalConsoleOut;
    private readonly StringWriter logWriter;

    public DualConsoleWriter(TextWriter originalConsoleOut, StringWriter logWriter)
    {
        this.originalConsoleOut = originalConsoleOut;
        this.logWriter = logWriter;
    }

    public override void Write(char value)
    {
        originalConsoleOut.Write(value);
        logWriter.Write(value);
    }

    public override void Write(string value)
    {
        originalConsoleOut.Write(value);
        logWriter.Write(value);
    }

    public override void WriteLine(string value)
    {
        originalConsoleOut.WriteLine(value);
        logWriter.WriteLine(value);
    }

    public override Encoding Encoding => originalConsoleOut.Encoding;
}