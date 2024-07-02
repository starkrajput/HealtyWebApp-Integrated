using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS.Models
{
    public class MailAccounts
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DefaultMessageTemplate { get; set; }
        public string DefaultSubject { get; set; }
    }

    }
