﻿using Microsoft.AspNetCore.Identity;

namespace devshop.api.Auths.Entities;

public sealed class UserToken : IdentityUserToken<Guid>
{
}