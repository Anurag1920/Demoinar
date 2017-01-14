using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess.IModel
{
   public interface IAspNetUser
    {
       string Id { get; set; }
       string UserName { get; set; }
       string PasswordHash { get; set; }
       string SecurityStamp { get; set; }
       string Discriminator { get; set; }
       string Email { get; set; }
       
    }
}
