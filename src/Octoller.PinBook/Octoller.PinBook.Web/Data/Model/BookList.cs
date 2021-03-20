using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octoller.PinBook.Web.Data.Model
{
    public class BookList : IIdEntity, IAuditable, IEquatable<BookList>
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

        ///<inheritdoc/>
        public override bool Equals(object obj) => 
            this.Equals(obj as BookList);

        #region Override object methods

        ///<inheritdoc/>
        public bool Equals(BookList bookList) =>
            bookList != null
            && this.Id == bookList.Id
            && this.Name == bookList.Name;

        ///<inheritdoc/>
        public override string ToString() =>
            "\nBook list:" +
            $"\n\tId: {Id}" +
            $"\n\tName: {Name}" +
            $"\n\tCreated by: {CreatedBy}" +
            $"\n\tNumber of books: {Books?.Count()}";

        ///<inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(this.Id, this.Name); 

        #endregion
    }
}
