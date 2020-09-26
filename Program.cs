using System;
using CecSharp;

namespace test_cec
{
  class Program
  {
    static void Main(string[] args)
    {
      var cecClient = new CecClient();
      cecClient.Scan();
      Console.WriteLine("Hello World!");
    }
  }
}
