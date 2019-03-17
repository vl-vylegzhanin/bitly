﻿using System.Collections.Generic;
using bitlyTest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bitlyTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IUrlsRepository _urlsRepository;

        public LinksController(IUrlsRepository urlsRepository)
        {
            _urlsRepository = urlsRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_urlsRepository.GetTransformedData());
        }
    }
}