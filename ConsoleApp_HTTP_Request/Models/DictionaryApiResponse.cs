﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_HTTP_Request.Models;
public class DictionaryApiResponse
{
    public string Word { get; set; } = string.Empty;
    public List<Meanings> Meanings { get; set; } = [];


}
