namespace AltranExercise.Data.Entities
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Client))
                return false;
            else
                return this.Id == ((Client)obj).Id &&
                    this.Name == ((Client)obj).Name &&
                    this.Email == ((Client)obj).Email &&
                this.Role == ((Client)obj).Role;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
