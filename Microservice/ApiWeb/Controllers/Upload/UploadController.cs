﻿using Microservice.Application.Services.Upload.Contexts;
using Microservice.Application.Services.Upload.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.ApiWeb.Controllers.Upload
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService<MultipartFormData> _uploadMultipartFormDataService;

        public UploadController(IUploadService<MultipartFormData> UploadService) {
            this._uploadMultipartFormDataService = UploadService;
        }

        [HttpPost("multipart-form-data")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload() {
            await this._uploadMultipartFormDataService.UploadHandlerAsync();
            return Ok();
        }
    }
}
