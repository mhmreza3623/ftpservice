﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ftp.Domain.Models
{
    public class SftpServiceConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RemoteDirectory { get; set; }
    }
}
