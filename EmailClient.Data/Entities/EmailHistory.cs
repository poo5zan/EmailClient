using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data.Entities
{
    public class EmailHistory
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        // 0 sent / 1 received emails
        public bool isReceived { get; set; }
    }
}
