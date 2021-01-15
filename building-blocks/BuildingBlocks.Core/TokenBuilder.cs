﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BuildingBlocks.Core
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly ITokenProvider _tokenProivder;
        private string _username;
        private List<Claim> _claims = new List<Claim>();
        public TokenBuilder(ITokenProvider tokenProvider)
        {
            _tokenProivder = tokenProvider;
        }

        public TokenBuilder AddUsername(string username)
        {
            this._username = username;

            return this;
        }

        public TokenBuilder FromClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            _username = claimsPrincipal.Identity.Name;

            _claims = claimsPrincipal.Claims.ToList();

            return this;
        }

        public TokenBuilder RemoveClaim(Claim claim)
        {
            _claims.Remove(_claims.SingleOrDefault(x => x.Type == claim.Type));

            return this;
        }
        public TokenBuilder AddOrUpdateClaim(Claim claim)
        {
            RemoveClaim(claim);

            _claims.Add(claim);

            return this;
        }

        public string Build()
        {
            return _tokenProivder.Get(_username, _claims);
        }
    }
}
