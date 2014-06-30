using System;
using System.Collections.Generic;

namespace ShortStuff.Domain.Entities
{
    public class User : EntityBase<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tag { get; set; }
        public string Picture { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<User> Followers { get; set; }
        public IEnumerable<Message> Favorites { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public IEnumerable<Topic> SubscribedTopics { get; set; }

        protected override void Validate()
        {
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
            System.Uri uriResult;
            if (!string.IsNullOrWhiteSpace(Picture) && 
                Uri.TryCreate(Picture, UriKind.Absolute, out uriResult) && 
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                AddBrokenRule(new ValidationRule("Picture", "Picture_InvalidURI"));
            }
        }
    }
}