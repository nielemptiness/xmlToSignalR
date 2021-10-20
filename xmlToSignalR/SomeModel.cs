using System;

namespace xmlToSignalR
{
    public class SomeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Created Created { get; set; }
    }

    public class Created
    {
        public string Creator { get; set; }
        public DateTime Date { get; set; }
    }

}