using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octoller.PinBook.Web.Data.Model
{
    public class Book : IIdEntity, IAuditable, IEquatable<Book>
    {
        #region SystemProperties

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

        #endregion

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

        ///<inheritdoc/>
        public override bool Equals(object obj) => 
            this.Equals(obj as Book);

        #region Override object methods

        ///<inheritdoc/>
        public bool Equals(Book book) =>
            book != null
            && this.Id == book.Id
            && this.Name == book.Name
            && this.ISBN == book.ISBN;

        ///<inheritdoc/>
        public override string ToString() =>
            "\nBook:" +
            $"\n\tId: {Id}" +
            $"\n\tName: {Name}" +
            $"\n\tISBN: {ISBN}" +
            $"\n\tAuthor: {string.Join(",", Authors?.Select(author => author.Name)).ToArray()}" +
            $"\n\tPublishing house: {PublishingHouse}" +
            $"\n\tCategories: {string.Join(",", Categories?.Select(categories => categories.Name).ToArray())}" +
            $"\n\tNumber of pages: {NumberOfPages}";

        ///<inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(this.Id, this.Name, this.ISBN); 

        #endregion
    }
}
