using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAzure.Shared.Abstractions.Errors
{
    public class InternalError : Error
    {
        public InternalError(string message) : base(message, ErrorType.InternalServerError) { }
    }
}
