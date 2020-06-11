using System;

namespace AltranExercise.Data.Entities
{
    public class Policy
    {
        public string Id { get; set; }
        public double AmountInsured { get; set; }
        public string Email { get; set; }
        public DateTime InceptionDate { get; set; }
        public bool InstallmentPayment { get; set; }
        public string ClientId { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Policy))
                return false;
            else
                return this.Id == ((Policy)obj).Id &&
                    this.AmountInsured == ((Policy)obj).AmountInsured &&
                    this.Email == ((Policy)obj).Email &&
                    this.InceptionDate == ((Policy)obj).InceptionDate &&
                    this.InstallmentPayment == ((Policy)obj).InstallmentPayment &&
                    this.ClientId == ((Policy)obj).ClientId;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
