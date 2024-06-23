using System.ComponentModel.DataAnnotations;

namespace skillz_backend.models
{
    // Represents the association between a user and a certificate.
    public class CertificatUser
    {
        // Gets or sets the unique identifier for the certificate-user association.
        [Key]
        public int IdCertificatUser { get; set; }

        // Gets or sets the unique identifier for the associated certificate.
        public string CertificateType { get; set; }

        // Gets or sets the unique identifier for the associated user.
        public int IdUser { get; set; }

        // Gets or sets the image associated with the certificate.
        public string CertificateImage { get; set; }

        // Gets or sets the user associated with this certificate-user association.
        public User User { get; set; }
    }
}
