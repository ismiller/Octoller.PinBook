using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;
using System.Collections.Generic;

namespace Octoller.PinBook.Web.Data.Model
{
    public class Book : IIdentity, IAuditable
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
        /// Название киниги
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Колличество страниц
        /// </summary>
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Номер ISBN
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Авторы
        /// </summary>
        public IEnumerable<Author> Authors { get; set; }

        /// <summary>
        /// Категории
        /// </summary>
        public IEnumerable<CategoryBook> Categories { get; set; }

        /// <summary>
        /// Список книг
        /// </summary>
        public IEnumerable<BookList> BookLists { get; set; }

        /// <summary>
        /// Издательство
        /// </summary>
        public PublishingHouse PublishingHouse { get; set; }

        /// <summary>
        /// Id издательства
        /// </summary>
        public string PublishingHouseId { get; set; }
    }
}
