﻿namespace SkiShop.Core.Models.CommentViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreateOn { get; set; }

        public string User { get; set; }
    }
}