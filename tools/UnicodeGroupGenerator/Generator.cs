using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Generator
{
  public static void Main(string[] args)
  {
    if (args.Length != 2)
    {
      Console.Out.WriteLine("Usage: generator DerivedGeneralCategory.txt groups.lex");
      return;
    }
    
    Dictionary<string, List<Range>> groups = new Dictionary<string, List<Range>>();
    
    using (TextReader reader = new StreamReader(args[0]))
    {
      string line;
      while ((line = reader.ReadLine()) != null)
      {
        line = line.Trim();
        if (line.Length == 0) continue;
        if (line[0] == '#') continue;
        
        int indx = 0;
        
        string first = ReadHex(line, ref indx);
        string last = null;
        if (Match(line, ref indx, '.'))
        {
          indx += 2;
          last = ReadHex(line, ref indx);
        }
        
        SkipWhitespaces(line, ref indx);
        if (!Match(line, ref indx, ';'))
          throw new InvalidOperationException();
        
        indx ++;
        SkipWhitespaces(line, ref indx);
        
        string group = line.Substring(indx, 2);
        
        if (!groups.ContainsKey(group))
          groups[group] = new List<Range>();
        
        groups[group].Add(new Range(first, last));
      }
    }
    
    using (TextWriter writer = new StreamWriter(args[1]))
    {
      foreach (KeyValuePair<string, List<Range>> pair in groups)
      {
        writer.Write("UNICODE_" + pair.Key.ToUpper() + "=[");
        foreach (Range range in pair.Value)
        {
          if (range.last == null && range.first.Length == 4)
            writer.Write("\\u" + range.first);
          else if (range.first.Length == 4 && range.last.Length == 4)
            writer.Write("\\u" + range.first + "-\\u" + range.last);
        }
        writer.WriteLine("]");
      }
    }
      
  }

  struct Range
  {
    public string first;
    public string last;

    public Range(string first, string last)
    {
      this.first = first;
      this.last = last;
    }
  }
  
  static void SkipWhitespaces(string str, ref int indx)
  {
    while (indx < str.Length)
    {
      if (!char.IsWhiteSpace(str[indx]))
        return;
      
      indx++;
    }
  }
  
  static string ReadHex (string str, ref int indx)
  {
    int len = 0;
    while (indx + len < str.Length && char.IsLetterOrDigit(str[indx+len]))
      len ++;
    
    string hex = str.Substring(indx, len);
    indx += len;
    return hex;
  }
  
  static bool Match (string str, ref int indx, char c)
  {
    SkipWhitespaces(str, ref indx);
    if (indx >= str.Length) return false;
    return str[indx] == c;
  }
  
}