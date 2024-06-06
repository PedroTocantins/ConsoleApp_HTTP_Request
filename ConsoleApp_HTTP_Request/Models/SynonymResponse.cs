using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HTTP_Request.Models;
public class SynonymResponse
{
    public string Word { get; set; } = string.Empty;
    public int Score { get; set; }
}
