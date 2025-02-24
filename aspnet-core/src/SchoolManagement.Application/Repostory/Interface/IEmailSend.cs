using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Repostory.Interface
{
    public interface IEmailSend
    {
        Task<bool> EmailSendAsync(string email, string Subject, string message); 
    }
}
