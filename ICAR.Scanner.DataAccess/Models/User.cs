﻿using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? IsEmailVerified { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsLocked { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string? ResetToken { get; set; }

    public DateTime? ResetTokenExpiry { get; set; }

    public bool? MfaEnabled { get; set; }

    public string? MfaSecret { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public Guid? AddressId { get; set; }

    public virtual Address? Address { get; set; }
}
