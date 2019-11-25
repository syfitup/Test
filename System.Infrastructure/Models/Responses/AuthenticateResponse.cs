using System;
using System.Collections.Generic;
using System.Text;

namespace SYF.Infrastructure.Models.Responses
{
    public class AuthenticateResponse
    {
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public Guid SiteId { get; set; }
        public Guid? PersonPositionId { get; set; }
        public Guid? LanguageId { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string PersonPositionName { get; set; }
        public int RarSections { get; set; }
        public bool SupportWidget { get; set; }
        public string Token { get; set; }
        public string[] Roles { get; set; }
        public string[] Permissions { get; set; }
        public string[] Features { get; set; }
        public int? TimeZoneId { get; set; }
    }
}


