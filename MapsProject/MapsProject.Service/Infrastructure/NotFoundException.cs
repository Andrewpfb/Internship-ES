using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsProject.Service.Infrastructure
{
    public class NotFoundException : AbstractException
    {
        public NotFoundException(string message, string property) : base(message, property) { }
    }
}
