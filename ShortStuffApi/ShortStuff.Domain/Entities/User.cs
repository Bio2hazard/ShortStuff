// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The user business model which is equivalent to the user data entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The user business model which is equivalent to the user data entity.
    /// </summary>
    public class User : EntityBase<decimal>
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the unique tag of the user.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the URI pointing to the picture of the user.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the list of public messages created by the user.
        /// </summary>
        public IEnumerable<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets the list of echoes created by the user.
        /// </summary>
        public IEnumerable<Echo> Echoes { get; set; }

        /// <summary>
        /// Gets or sets the list of users who follow this user.
        /// </summary>
        public IEnumerable<User> Followers { get; set; }

        /// <summary>
        /// Gets or sets the list of users followed by this user.
        /// </summary>
        public IEnumerable<User> Following { get; set; }

        /// <summary>
        /// Gets or sets the list of public messages this user has tagged as favorites.
        /// </summary>
        public IEnumerable<Message> Favorites { get; set; }

        /// <summary>
        /// Gets or sets the list of notifications belonging to this user.
        /// </summary>
        public IEnumerable<Notification> Notifications { get; set; }

        /// <summary>
        /// Gets or sets the list of topics this user is subscribed to.
        /// </summary>
        public IEnumerable<Topic> SubscribedTopics { get; set; }

        /// <summary>
        /// Validates the user for creation.
        /// </summary>
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
                  (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
            {
                // Or the link is not of type Http(s)
                AddBrokenRule(new ValidationRule("Picture", "Picture_InvalidURI"));
            }
        }

        /// <summary>
        /// Validates the user for update.
        /// </summary>
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
                  (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
            {
                // Or the link is not of type Http(s)
                AddBrokenRule(new ValidationRule("Picture", "Picture_InvalidURI"));
            }
        }
    }
}