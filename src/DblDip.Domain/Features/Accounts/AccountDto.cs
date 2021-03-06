using System;
using System.Collections.Generic;

namespace DblDip.Domain.Features
{
    public class AccountDto
    {
        public Guid? AccountId { get; init; }
        public Guid DefaultProfileId { get; init; }
        public string Name { get; init; }
        public Guid AccountHolderUserId { get; init; }
        public ICollection<Guid> ProfileIds { get; init; }

        public AccountDto(Guid accountId, Guid defaultProfileId, string name, Guid accountHolderUserId, ICollection<Guid> profileIds)
        {
            AccountId = accountId;
            DefaultProfileId = defaultProfileId;
            Name = name;
            AccountHolderUserId = accountHolderUserId;
            ProfileIds = profileIds;
        }
    }
}
