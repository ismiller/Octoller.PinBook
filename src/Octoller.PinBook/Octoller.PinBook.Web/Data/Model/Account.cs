using Octoller.PinBook.Web.Data.Model.Abstraction;
using System;

namespace Octoller.PinBook.Web.Data.Model
{
    /// <summary>
    /// Содержит дополнительную информацию о пользователе
    /// </summary>
    public class Account : IIdentity, IAuditable
    {
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
        /// Id группы на стороне сервиса Vk
        /// </summary>
        public string VkId { get; set; }

        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Аватар пользователя
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Ключ доступа
        /// </summary>
        public string AccessToken { get; set; }

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
