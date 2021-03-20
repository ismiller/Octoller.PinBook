using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octoller.PinBook.Web.Data.Model
{
    public class CategoryBook : IIdEntity, IAuditable, IEquatable<CategoryBook>
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
        /// Название категории
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
        public override bool Equals(object obj) =>
            this.Equals(obj as CategoryBook);

        ///<inheritdoc/>
        public bool Equals(CategoryBook category) =>
            category != null
            && this.Id == category.Id
            && this.Name == category.Name;

        ///<inheritdoc/>
        public override string ToString() =>
            "\nCategory:" +
            $"\n\tId: {Id}" +
            $"\n\tName: {Name}" +
            $"\n\tNumber of books: {Books?.Count()}";

        ///<inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(this.Id, this.Name); 

        #endregion
    }
}
