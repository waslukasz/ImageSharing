﻿using Application_Core.Model;
using Microsoft.Build.Framework;

namespace WebAPI.Request
{
    public class AddCommentRequest
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string Text { get; set; }

    }
}
