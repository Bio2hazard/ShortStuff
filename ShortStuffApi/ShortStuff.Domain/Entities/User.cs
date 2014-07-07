using System;
using System.Collections.Generic;

namespace ShortStuff.Domain.Entities
{
    public class User : EntityBase<decimal>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tag { get; set; }
        public string Picture { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<Echo> Echoes { get; set; } 
        public IEnumerable<User> Followers { get; set; }
        public IEnumerable<User> Following { get; set; }
        public IEnumerable<Message> Favorites { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public IEnumerable<Topic> SubscribedTopics { get; set; }

        protected override void Validate()
        {
            if (Id == 0)
            {
                AddBrokenRule(new ValidationRule("Id", "Id_Missing"));
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new ValidationRule("Name", "Name_Missing"));
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                AddBrokenRule(new ValidationRule("Email", "Email_Missing"));
            }
            if (string.IsNullOrWhiteSpace(Tag))
            {
                AddBrokenRule(new ValidationRule("Tag", "Tag_Missing"));
            }
            Uri uriResult;
            if (!string.IsNullOrWhiteSpace(Picture) && // If string is set
                !(Uri.TryCreate(Picture, UriKind.Absolute, out uriResult) && // And either creating the link fails...
                  (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))) // Or the link is not of type Http(s)
            {
                AddBrokenRule(new ValidationRule("Picture", "Picture_InvalidURI"));
            }
        }

        protected override void UpdateValidate()
        {
            if (Name != null && string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new ValidationRule("Name", "Name_Whitespace"));
            }
            if (Email != null && string.IsNullOrWhiteSpace(Email))
            {
                AddBrokenRule(new ValidationRule("Email", "Email_Whitespace"));
            }
            if (Tag != null && string.IsNullOrWhiteSpace(Tag))
            {
                AddBrokenRule(new ValidationRule("Tag", "Tag_Whitespace"));
            }
            Uri uriResult;
            if (!string.IsNullOrWhiteSpace(Picture) && // If string is set
                !(Uri.TryCreate(Picture, UriKind.Absolute, out uriResult) && // And either creating the link fails...
                  (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))) // Or the link is not of type Http(s)
            {
                AddBrokenRule(new ValidationRule("Picture", "Picture_InvalidURI"));
            }
        }
    }
}