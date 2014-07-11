// ShortStuff.Domain
// IValidatable.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Collections.Generic;

namespace ShortStuff.Domain.Entities
{
    internal interface IValidatable
    {
        IEnumerable<ValidationRule> GetBrokenRules();
    }
}
