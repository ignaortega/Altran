using System;

namespace AltranExercise.Service.DTOs
{
    public class PolicyDto
    {
        public string Id { get; set; }
        public double AmountInsured { get; set; }
        public string Email { get; set; }
        public DateTime InceptionDate { get; set; }
        public string InstallmentPayment { get; set; }
        public string ClientId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PolicyDto))
                return false;
            else
                return this.Id == ((PolicyDto)obj).Id &&
                    this.AmountInsured == ((PolicyDto)obj).AmountInsured &&
                    this.Email == ((PolicyDto)obj).Email &&
                    this.InceptionDate == ((PolicyDto)obj).InceptionDate &&
                    this.InstallmentPayment == ((PolicyDto)obj).InstallmentPayment &&
                    this.ClientId == ((PolicyDto)obj).ClientId;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
