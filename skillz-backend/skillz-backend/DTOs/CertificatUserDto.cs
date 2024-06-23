using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skillz_backend.DTOs
{
    public class CertificatUserDto
    {
             // Gets or sets the unique identifier for the associated certificate.
        public string CertificateType { get; set; }

        // Gets or sets the unique identifier for the associated user.
        public int IdUser { get; set; }
    }
}