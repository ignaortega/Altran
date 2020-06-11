namespace AltranExercise.Service.DTOs
{
    public class ClientDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ClientDto))
                return false;
            else
                return this.Id == ((ClientDto)obj).Id &&
                    this.Name == ((ClientDto)obj).Name &&
                    this.Email == ((ClientDto)obj).Email &&
                    this.Role == ((ClientDto)obj).Role;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
