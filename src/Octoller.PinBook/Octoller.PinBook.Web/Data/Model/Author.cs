using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octoller.PinBook.Web.Data.Model
{
    public class Author : IIdEntity, IAuditable, IEquatable<Author>
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
        /// Имя автора
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Книги
        /// </summary>
        public IEnumerable<Book> Books { get; set; }

        #region Override object methods

        ///<inheritdoc/>
        public bool Equals(Author author) =>
            author != null
            && this.Id == author.Id
            && this.Name == author.Name;

        ///<inheritdoc/>
        public override bool Equals(object obj) =>
            this.Equals(obj as Author);

        ///<inheritdoc/>
        public override string ToString() =>
            $"\nAuthor: " +
            $"\n\tId: {Id}; " +
            $"\n\tName: {Name}" +
            $"\n\tNumber of books: {Books?.Count()}";

        ///<inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(this.Id, this.Name); 

        #endregion
    }
}
