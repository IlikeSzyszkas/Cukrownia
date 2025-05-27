namespace Projekt2.Models
{
    //klasa User
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        private string role;
        public string Role
        {
            get => role;
            set
            {
                var allowedRoles = new[] { "admin", "client", "worker", "volunteer" };
                if (!allowedRoles.Contains(value.ToLower()))
                {
                    throw new ArgumentException("Invalid role. Allowed roles are: admin, client, worker, volunteer");
                }
                role = value;
            }
        }
    }
}
