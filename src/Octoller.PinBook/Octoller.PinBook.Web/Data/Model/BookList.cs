using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;
using System.Collections.Generic;

namespace Octoller.PinBook.Web.Data.Model
{
    public class BookList : IIdentity, IAuditable
    {
        ///<inheritdoc />
        public string Id { get; set; }

        ///<inheritdoc />
        public DateTime CreatedAt { get; set; }

        ///<inheritdoc />
        public string CreatedBy { get; set; }

        ///<inheritdoc />
        public DateTime UpdatedAt { get; set; }

        ///<inheritdoc />
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Название списка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Книги
        /// </summary>
        public IEnumerable<Book> Books { get; set; }
    }
}
