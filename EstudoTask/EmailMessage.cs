using System.Collections.Generic;

namespace EstudoTask
{
    public class EmailMessage
    {
        public EmailMessage(IList<string> usersTo, string subject, string body)
        {
            UsersTo = usersTo;
            Subject = subject;
            Body = body;

            UsersTo = UsersTo ?? new List<string>();
        }

        public IList<string> UsersTo { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
    }
}