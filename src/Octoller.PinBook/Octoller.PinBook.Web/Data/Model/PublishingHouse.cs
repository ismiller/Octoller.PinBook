using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Octoller.PinBook.Web.Data.Model
{
    public class PublishingHouse : IIdEntity, IAuditable, IEquatable<PublishingHouse>
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
        /// Название
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
            this.Equals(obj as PublishingHouse);

        ///<inheritdoc/>
        public bool Equals(PublishingHouse publishingHouse) =>
            publishingHouse != null
            && this.Id == publishingHouse.Id
            && this.Name == publishingHouse.Name;

        ///<inheritdoc/>
        public override string ToString() =>
            "\nPublishingHouse:" +
            $"\n\tId: {Id}" +
            $"\n\tName: {Name}" +
            $"\n\tNumber of books: {Books?.Count()}";

        ///<inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(this.Id, this.Name); 

        #endregion
    }
}
