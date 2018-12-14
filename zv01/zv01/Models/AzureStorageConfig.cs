using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zv01.Models
{
    public class AzureStorageConfig
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string QueueName { get; set; }
        public string QRImgContainer { get; set; }
        public string AppImgContainer { get; set; }
        public string UserImgContainer { get; set; }
        public string EventImgContainer { get; set; }
    }
}
