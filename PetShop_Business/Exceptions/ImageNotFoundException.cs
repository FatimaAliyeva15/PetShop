using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop_Business.Exceptions
{
    public class ImageNotFoundException : Exception
    {
        public string PropertyName { get; set; }
        public ImageNotFoundException(string propertyName, string? message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
