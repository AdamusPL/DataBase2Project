using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.LecturerInformations.Infrastructure.Models
{
    internal readonly record struct LecturerData(

        string Name, 
        string Surname, 
        string Email, 
        string Phone

        );
}
