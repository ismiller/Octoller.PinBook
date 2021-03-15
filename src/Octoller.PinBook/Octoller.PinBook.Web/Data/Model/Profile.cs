using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;

namespace Octoller.PinBook.Web.Data.Model
{
    public class Profile : IIdentity, IAuditable
    {
        ///<inheritdoc />
        public string Id { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сайт пользователя
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// Местоположение пользователя
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Дополнительная информация о пользователе
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Аватар пользователя 
        /// </summary>
        public byte[] Avatar { get; set; }

        ///<inheritdoc />
        public DateTime CreatedAt { get; set; }

        ///<inheritdoc />
        public string CreatedBy { get; set; }

        ///<inheritdoc />
        public DateTime UpdatedAt { get; set; }

        ///<inheritdoc />
        public string UpdatedBy { get; set; }

    }
}
