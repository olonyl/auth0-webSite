﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth0WebSite.ViewModels
{
    public class Comment
    {

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
