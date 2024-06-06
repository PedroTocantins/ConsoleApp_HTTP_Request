using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HTTP_Request.Models;
public class Meanings
{
    public string PartOfSpeech { get; set; } = string.Empty;
    public List<Definitions> Definitions { get; set; } = [];
}
