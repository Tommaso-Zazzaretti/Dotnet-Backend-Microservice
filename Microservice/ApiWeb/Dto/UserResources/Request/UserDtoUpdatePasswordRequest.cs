﻿namespace Microservice.ApiWeb.Dto.UserResources.Request
{
    public class UserDtoUpdatePasswordRequest
    {
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
