﻿using Microsoft.AspNetCore.Identity;

namespace devshop.api.Features.Auths.Entities;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
}
